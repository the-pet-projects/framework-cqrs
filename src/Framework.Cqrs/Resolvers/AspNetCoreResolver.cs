namespace PetProjects.Framework.Cqrs.Resolvers
{
    using System;
    using System.Collections.Generic;
    using DependencyResolver;
    using Microsoft.Extensions.DependencyInjection;

    public class AspNetCoreResolver : IDependencyResolver
    {
        private readonly IServiceProvider serviceProvider;

        public AspNetCoreResolver(IServiceCollection services)
        {
            this.serviceProvider = services.BuildServiceProvider();
        }

        public object GetInstance(Type type)
        {
            return this.serviceProvider.GetService(type);
        }

        public IEnumerable<T> GetInstance<T>()
        {
            return this.serviceProvider.GetServices<T>();
        }
    }
}