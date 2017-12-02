namespace PetProjects.Framework.Cqrs.Extensions.AspNetCore
{
    using Microsoft.Extensions.DependencyInjection;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.DependencyResolver;
    using PetProjects.Framework.Cqrs.Queries;

    internal class AspNetCoreResolverBuilder : IDependencyResolverBuilder
    {
        private readonly IServiceCollection collection;

        public AspNetCoreResolverBuilder(IServiceCollection collection)
        {
            this.collection = collection;
        }

        public void RegisterQueryHandlerAsync<THandler, TQuery, TResponse>()
            where THandler : class, IQueryHandlerAsync<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            this.collection.AddScoped<IQueryHandlerAsync<TQuery, TResponse>, THandler>();
        }

        public void RegisterCommandHandlerAsync<THandler, TCommand>()
            where THandler : class, ICommandHandlerAsync<TCommand>
            where TCommand : ICommand
        {
            this.collection.AddScoped<ICommandHandlerAsync<TCommand>, THandler>();
        }

        public void RegisterCommandHandlerWithResponseAsync<THandler, TCommand, TResponse>()
            where THandler : class, ICommandHandlerWithResponseAsync<TCommand, TResponse>
            where TCommand : ICommand
        {
            this.collection.AddScoped<ICommandHandlerWithResponseAsync<TCommand, TResponse>, THandler>();
        }

        public IServiceCollection GetServiceCollection()
        {
            return this.collection;
        }
    }
}