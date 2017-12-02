namespace PetProjects.Framework.Cqrs.Extensions.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using PetProjects.Framework.Cqrs.DependencyResolver;

    public static class AspNetCoreExtensions
    {
        public static IServiceCollection AddCqrsDependencyResolver(this IServiceCollection collection, Action<IDependencyResolverBuilder> builderAct, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var builder = new AspNetCoreResolverBuilder(collection);
            builderAct(builder);
            collection = builder.GetServiceCollection();
            collection.Add(new ServiceDescriptor(typeof(IDependencyResolver), typeof(AspNetCoreResolver), lifetime));
            return collection;
        }
    }
}