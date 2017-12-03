namespace PetProjects.Framework.Cqrs.Mediator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.DependencyResolver;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.Framework.Cqrs.Utils;

    public class Mediator : IMediator
    {
        private readonly IDependencyResolver dependencyResolver;

        public Mediator(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        public IEnumerable<Response<TResponse>> Query<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var responses = new List<Response<TResponse>>();
            var handlers = this.dependencyResolver.ResolveDependencies<IQueryHandler<TQuery, TResponse>>();

            foreach (var handler in handlers)
            {
                try
                {
                    responses.Add(new Response<TResponse>(handler.Handle(query)));
                }
                catch (Exception ex)
                {
                    responses.Add(new Response<TResponse>(ex));
                }
            }

            return responses;
        }

        public async Task<IEnumerable<Response<TResponse>>> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var responses = new List<Response<TResponse>>();
            var handlers = this.dependencyResolver.ResolveDependencies<IQueryHandlerAsync<TQuery, TResponse>>();
            var tasks = new List<Task<TResponse>>();

            foreach (var handler in handlers)
            {
                try
                {
                    tasks.Add(handler.HandleAsync(query));
                }
                catch (Exception ex)
                {
                    responses.Add(new Response<TResponse>(ex));
                }
            }

            return await this.CollectResultsFromAsyncQueryHandlers(responses, tasks).ConfigureAwait(false);
        }

        public void RunCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlers = this.dependencyResolver.ResolveDependencies<ICommandHandler<TCommand>>();

            foreach (var handler in handlers)
            {
                handler.Handle(command);
            }
        }

        public async Task RunCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlers = this.dependencyResolver.ResolveDependencies<ICommandHandlerAsync<TCommand>>();
            var tasks = new List<Task>();
            var exceptions = new List<Exception>();

            foreach (var handler in handlers)
            {
                try
                {
                    tasks.Add(handler.HandleAsync(command));
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            try
            {
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (Exception)
            {
                exceptions.AddRange(tasks.Where(t => t.Exception != null).Select(t => t.Exception));
            }
        }

        private async Task<ICollection<Response<TResponse>>> CollectResultsFromAsyncQueryHandlers<TResponse>(ICollection<Response<TResponse>> responses, ICollection<Task<TResponse>> tasks)
        {
            try
            {
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch
            {
                // ignored
            }

            foreach (var task in tasks)
            {
                try
                {
                    responses.Add(new Response<TResponse>(task.Result));
                }
                catch (AggregateException ex)
                {
                    responses.Add(new Response<TResponse>(ex.InnerException));
                }
            }

            return responses;
        }
    }
}