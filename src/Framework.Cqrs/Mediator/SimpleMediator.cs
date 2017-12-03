namespace PetProjects.Framework.Cqrs.Mediator
{
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.DependencyResolver;
    using PetProjects.Framework.Cqrs.Queries;

    public class SimpleMediator : ISimpleMediator
    {
        private readonly IDependencyResolver dependencyResolver;

        public SimpleMediator(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        public Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var handler = this.dependencyResolver.ResolveFirstDependency<IQueryHandlerAsync<TQuery, TResponse>>();

            return handler.HandleAsync(query);
        }

        public Task RunCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = this.dependencyResolver.ResolveFirstDependency<ICommandHandlerAsync<TCommand>>();

            return handler.HandleAsync(command);
        }
    }
}