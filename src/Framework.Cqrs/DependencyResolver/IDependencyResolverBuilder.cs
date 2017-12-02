namespace PetProjects.Framework.Cqrs.DependencyResolver
{
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;

    public interface IDependencyResolverBuilder
    {
        IDependencyResolverBuilder RegisterQueryHandlerAsync<THandler, TQuery, TResponse>()
            where THandler : class, IQueryHandlerAsync<TQuery, TResponse>
            where TQuery : IQuery<TResponse>;

        IDependencyResolverBuilder RegisterCommandHandlerAsync<THandler, TCommand>()
            where THandler : class, ICommandHandlerAsync<TCommand>
            where TCommand : ICommand;

        IDependencyResolverBuilder RegisterCommandHandlerWithResponseAsync<THandler, TCommand, TResponse>()
            where THandler : class, ICommandHandlerWithResponseAsync<TCommand, TResponse>
            where TCommand : ICommand;
    }
}