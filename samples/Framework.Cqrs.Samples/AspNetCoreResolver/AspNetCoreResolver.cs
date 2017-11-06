namespace Framework.Cqrs.Samples.AspNetCoreResolver
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using PetProjects.Framework.Cqrs.DependencyResolver;

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