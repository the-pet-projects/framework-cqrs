namespace PetProjects.Framework.Cqrs.Mediator
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;

    public class Mediator : IMediator
    {
        private readonly Dictionary<Type, Creator> typeMapper = new Dictionary<Type, Creator>();

        public delegate object Creator(Mediator container);

        public void Register<T>(Creator creator)
        {
            this.typeMapper.Add(typeof(T), creator);
        }

        public TResponse Query<TQuery, TResponse>(TQuery query)
            where TQuery : IQuery
        {
            var handler = this.Create<IQueryHandler<TQuery, TResponse>>();

            return handler.Handle(query);
        }

        public async Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query)
            where TQuery : IQuery
        {
            var handler = this.Create<IQueryHandlerAsync<TQuery, TResponse>>();

            return await handler.HandleAsync(query);
        }

        public void RunCommand<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handler = this.Create<ICommandHandler<TCommand>>();

            handler.Handle(command);
        }

        public TResponse RunCommandWithResponse<TCommand, TResponse>(TCommand command)
            where TCommand : ICommand
        {
            var handler = this.Create<ICommandHandlerWithResponse<TCommand, TResponse>>();

            return handler.Handle(command);
        }

        public async Task RunCommandAsync<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handler = this.Create<ICommandHandlerAsync<TCommand>>();

            await handler.HandleAsync(command);
        }

        public async Task<TResponse> RunCommandWithResponseAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand
        {
            var handler = this.Create<ICommandHandlerWithResponseAsync<TCommand, TResponse>>();

            return await handler.HandleAsync(command);
        }

        private T Create<T>()
        {
            if (this.typeMapper.ContainsKey(typeof(T)))
            {
                return (T)this.typeMapper[typeof(T)](this);
            }

            return default(T);
        }
    }
}