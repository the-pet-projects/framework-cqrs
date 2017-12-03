namespace PetProjects.Framework.Cqrs.DependencyResolver
{
    using System;
    using System.Collections.Generic;

    public interface IDependencyResolver : IDisposable
    {
        IEnumerable<T> ResolveDependencies<T>();

        T ResolveFirstDependency<T>();
    }
}