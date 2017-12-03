namespace PetProjects.Framework.Cqrs.Extensions.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using PetProjects.Framework.Cqrs.DependencyResolver;

    public class AspNetCoreResolver : IDependencyResolver
    {
        private readonly IServiceScope scope;

        public AspNetCoreResolver(IServiceProvider provider)
        {
            this.scope = provider.CreateScope();
        }

        public IEnumerable<T> ResolveDependencies<T>()
        {
            return this.scope.ServiceProvider.GetServices<T>();
        }

        public T ResolveFirstDependency<T>()
        {
            return this.scope.ServiceProvider.GetRequiredService<T>();
        }

        public void Dispose()
        {
            this.scope.Dispose();
        }
    }
}