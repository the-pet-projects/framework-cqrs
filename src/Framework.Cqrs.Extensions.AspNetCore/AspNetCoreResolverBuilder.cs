namespace PetProjects.Framework.Cqrs.Extensions.AspNetCore
{
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.DependencyResolver;
    using PetProjects.Framework.Cqrs.Queries;

    public class AspNetCoreResolverBuilder : IDependencyResolverBuilder
    {
        public static readonly IReadOnlyDictionary<Lifetime, ServiceLifetime> LifetimeMapper =
            new Dictionary<Lifetime, ServiceLifetime>
            {
                { Lifetime.Scoped, ServiceLifetime.Scoped },
                { Lifetime.Transient, ServiceLifetime.Transient },
                { Lifetime.Singleton, ServiceLifetime.Singleton }
            };

        private readonly IServiceCollection collection;

        public AspNetCoreResolverBuilder(IServiceCollection collection)
        {
            this.collection = collection;
        }

        public IDependencyResolverBuilder RegisterQueryHandlerAsync<THandler, TQuery, TResponse>(Lifetime lifetime = Lifetime.Scoped)
            where THandler : class, IQueryHandlerAsync<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            this.collection.Add(new ServiceDescriptor(typeof(IQueryHandlerAsync<TQuery, TResponse>), typeof(THandler), AspNetCoreResolverBuilder.LifetimeMapper[lifetime]));
            return this;
        }

        public IDependencyResolverBuilder RegisterCommandHandlerAsync<THandler, TCommand>(Lifetime lifetime = Lifetime.Scoped)
            where THandler : class, ICommandHandlerAsync<TCommand>
            where TCommand : ICommand
        {
            this.collection.Add(new ServiceDescriptor(typeof(ICommandHandlerAsync<TCommand>), typeof(THandler), AspNetCoreResolverBuilder.LifetimeMapper[lifetime]));
            return this;
        }

        public IDependencyResolverBuilder RegisterCommandHandlerWithResponseAsync<THandler, TCommand, TResponse>(Lifetime lifetime = Lifetime.Scoped)
            where THandler : class, ICommandHandlerWithResponseAsync<TCommand, TResponse>
            where TCommand : ICommand
        {
            this.collection.Add(new ServiceDescriptor(typeof(ICommandHandlerWithResponseAsync<TCommand, TResponse>), typeof(THandler), AspNetCoreResolverBuilder.LifetimeMapper[lifetime]));
            return this;
        }

        public IServiceCollection GetServiceCollection()
        {
            return this.collection;
        }
    }
}