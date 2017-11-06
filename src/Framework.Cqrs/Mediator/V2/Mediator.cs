namespace PetProjects.Framework.Cqrs.Mediator.V2
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.DependencyResolver;
    using PetProjects.Framework.Cqrs.Queries;

    public class Mediator : IMediator
    {
        private readonly IDependencyResolver dependencyResolver;

        public Mediator(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        public TResponse Query<TResponse>(IQuery<TResponse> query)
        {
            var plan = new MediatorPlan<TResponse>(typeof(IQueryHandler<,>), "Handle", query.GetType(), this.dependencyResolver);

            return plan.Invoke(query);
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query)
        {
            var plan = new MediatorPlan<TResponse>(typeof(IQueryHandlerAsync<,>), "HandleAsync", query.GetType(), this.dependencyResolver);

            return await plan.InvokeAsync(query);
        }

        public void RunCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlers = this.dependencyResolver.GetInstance<ICommandHandler<TCommand>>();

            foreach (var handler in handlers)
            {
                handler.Handle(command);
            }
        }

        public async Task RunCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlers = this.dependencyResolver.GetInstance<ICommandHandlerAsync<TCommand>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(command);
            }
        }

        private class MediatorPlan<TResult>
        {
            private readonly MethodInfo handleMethod;
            private readonly Func<object> handlerInstanceBuilder;

            public MediatorPlan(Type handlerTypeTemplate, string handlerMethodName, Type messageType, IDependencyResolver dependencyResolver)
            {
                var handlerType = handlerTypeTemplate.MakeGenericType(messageType, typeof(TResult));

                this.handleMethod = this.GetHandlerMethod(handlerType, handlerMethodName, messageType);

                this.handlerInstanceBuilder = () => dependencyResolver.GetInstance(handlerType);
            }

            public TResult Invoke(object message)
            {
                return (TResult)this.handleMethod.Invoke(this.handlerInstanceBuilder(), new[] { message });
            }

            public async Task<TResult> InvokeAsync(object message)
            {
                return await(Task<TResult>)this.handleMethod.Invoke(this.handlerInstanceBuilder(), new[] { message });
            }

            private MethodInfo GetHandlerMethod(Type handlerType, string handlerMethodName, Type messageType)
            {
                return handlerType.GetMethod(handlerMethodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, CallingConventions.HasThis, new[] { messageType }, null);
            }
        }
    }
}