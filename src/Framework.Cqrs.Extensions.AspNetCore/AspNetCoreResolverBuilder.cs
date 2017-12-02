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

        public IDependencyResolverBuilder RegisterQueryHandlerAsync<THandler, TQuery, TResponse>()
            where THandler : class, IQueryHandlerAsync<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            this.collection.AddScoped<IQueryHandlerAsync<TQuery, TResponse>, THandler>();
            return this;
        }

        public IDependencyResolverBuilder RegisterCommandHandlerAsync<THandler, TCommand>()
            where THandler : class, ICommandHandlerAsync<TCommand>
            where TCommand : ICommand
        {
            this.collection.AddScoped<ICommandHandlerAsync<TCommand>, THandler>();
            return this;
        }

        public IDependencyResolverBuilder RegisterCommandHandlerWithResponseAsync<THandler, TCommand, TResponse>()
            where THandler : class, ICommandHandlerWithResponseAsync<TCommand, TResponse>
            where TCommand : ICommand
        {
            this.collection.AddScoped<ICommandHandlerWithResponseAsync<TCommand, TResponse>, THandler>();
            return this;
        }

        public IServiceCollection GetServiceCollection()
        {
            return this.collection;
        }
    }
}