namespace PetProjects.Framework.Cqrs.DependencyResolver
{
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;

    public interface IDependencyResolverBuilder
    {
        IDependencyResolverBuilder RegisterQueryHandlerAsync<THandler, TQuery, TResponse>(Lifetime lifetime = Lifetime.Scoped)
            where THandler : class, IQueryHandlerAsync<TQuery, TResponse>
            where TQuery : IQuery<TResponse>;

        IDependencyResolverBuilder RegisterCommandHandlerAsync<THandler, TCommand>(Lifetime lifetime = Lifetime.Scoped)
            where THandler : class, ICommandHandlerAsync<TCommand>
            where TCommand : ICommand;

        IDependencyResolverBuilder RegisterCommandHandlerWithResponseAsync<THandler, TCommand, TResponse>(Lifetime lifetime = Lifetime.Scoped)
            where THandler : class, ICommandHandlerWithResponseAsync<TCommand, TResponse>
            where TCommand : ICommand;
    }
}