namespace PetProjects.Framework.Cqrs.DependencyResolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DependencyResolver
    {
        private static IDependencyResolver currentResolver;

        public static void SetResolver(IDependencyResolver resolver)
        {
            currentResolver = resolver;
        }

        public static void SetResolver(Func<Type, object> getInstance, Func<Type, IEnumerable<object>> getInstances)
        {
            currentResolver = new DelegateDependencyResolver(getInstance, getInstances);
        }

        private class DelegateDependencyResolver : IDependencyResolver
        {
            private readonly Func<Type, object> getInstance;
            private readonly Func<Type, IEnumerable<object>> getInstances;

            public DelegateDependencyResolver(Func<Type, object> getInstance, Func<Type, IEnumerable<object>> getInstances)
            {
                this.getInstance = getInstance;
                this.getInstances = getInstances;
            }

            public object GetInstance(Type type) => this.getInstance(type);

            public IEnumerable<T> GetInstance<T>() => Enumerable.OfType<T>(this.getInstances(typeof(T)));
        }
    }
}