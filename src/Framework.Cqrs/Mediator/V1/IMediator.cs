namespace PetProjects.Framework.Cqrs.Mediator.V1
{
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;

    public interface IMediator
    {
        TResponse Query<TQuery, TResponse>(TQuery query) where TQuery : IQuery;

        Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery;

        void RunCommand<TCommand>(TCommand command) where TCommand : ICommand;

        TResponse RunCommandWithResponse<TCommand, TResponse>(TCommand command) where TCommand : ICommand;

        Task RunCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;

        Task<TResponse> RunCommandWithResponseAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand;
    }
}