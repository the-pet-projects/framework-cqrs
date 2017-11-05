namespace PetProjects.Framework.Cqrs.Mediator.V2
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.DependencyResolver;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.Framework.Cqrs.Utils;

    public class Mediator : IMediator
    {
        private readonly IDependencyResolver dependencyResolver;

        public Mediator(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        public TResponse Query<TResponse>(IQuery query)
        {
            var plan = new MediatorPlan<TResponse>(typeof(IQueryHandler<,>), "Handle", query.GetType(), this.dependencyResolver);

            return plan.Invoke(query);
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery query)
        {
            var plan = new MediatorPlan<TResponse>(typeof(IQueryHandlerAsync<,>), "HandleAsync", query.GetType(), this.dependencyResolver);

            return await plan.InvokeAsync(query);
        }

        public void RunCommand(ICommand command)
        {
            throw new NotImplementedException();
        }

        public Task RunCommandAsync(ICommand command)
        {
            throw new NotImplementedException();
        }

        public TResponse RunCommandWithResponse<TResponse>(ICommand command)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> RunCommandWithResponseAsync<TResponse>(ICommand command)
        {
            throw new NotImplementedException();
        }

        private class MediatorPlan<TResult>
        {
            private readonly MethodInfo HandleMethod;
            private readonly Func<object> HandlerInstanceBuilder;

            public MediatorPlan(Type handlerTypeTemplate, string handlerMethodName, Type messageType, IDependencyResolver dependencyResolver)
            {
                var handlerType = handlerTypeTemplate.MakeGenericType(messageType, typeof(TResult));

                HandleMethod = GetHandlerMethod(handlerType, handlerMethodName, messageType);

                HandlerInstanceBuilder = () => dependencyResolver.GetInstance(handlerType);
            }

            private MethodInfo GetHandlerMethod(Type handlerType, string handlerMethodName, Type messageType)
            {
                return handlerType
                    .GetMethod(handlerMethodName,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                        null, CallingConventions.HasThis,
                        new[] { messageType },
                        null);
            }

            public TResult Invoke(object message)
            {
                return (TResult)HandleMethod.Invoke(HandlerInstanceBuilder(), new[] { message });
            }

            public async Task<TResult> InvokeAsync(object message)
            {
                return await (Task<TResult>)HandleMethod.Invoke(HandlerInstanceBuilder(), new[] { message });
            }
        }
    }
}