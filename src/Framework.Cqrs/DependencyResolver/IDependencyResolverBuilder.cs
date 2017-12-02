namespace PetProjects.Framework.Cqrs.DependencyResolver
{
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;

    public interface IDependencyResolverBuilder
    {
        void RegisterQueryHandlerAsync<THandler, TQuery, TResponse>()
            where THandler : class, IQueryHandlerAsync<TQuery, TResponse>
            where TQuery : IQuery<TResponse>;

        void RegisterCommandHandlerAsync<THandler, TCommand>()
            where THandler : class, ICommandHandlerAsync<TCommand>
            where TCommand : ICommand;

        void RegisterCommandHandlerWithResponseAsync<THandler, TCommand, TResponse>()
            where THandler : class, ICommandHandlerWithResponseAsync<TCommand, TResponse>
            where TCommand : ICommand;
    }
}