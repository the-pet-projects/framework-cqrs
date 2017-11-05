namespace PetProjects.Framework.Cqrs.Mediator
{
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;

    public interface IMediator
    {
        TResponse Query<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;

        void RunCommand<TCommand>(TCommand command) where TCommand : ICommand;

        TResponse RunCommandWithFeedback<TCommand, TResponse>(TCommand command) where TCommand : ICommand;

        Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<Task<TResponse>>;

        Task RunCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;

        Task<TResponse> RunCommandWithFeedbackAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand;
    }
}