namespace PetProjects.Framework.Cqrs.Mediator.V2
{
    using System.Threading.Tasks;

    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.Framework.Cqrs.Utils;

    public interface IMediator
    {
        TResponse Query<TResponse>(IQuery query);

        Task<TResponse> QueryAsync<TResponse>(IQuery query);

        void RunCommand(ICommand command);

        TResponse RunCommandWithResponse<TResponse>(ICommand command);

        Task RunCommandAsync(ICommand command);

        Task<TResponse> RunCommandWithResponseAsync<TResponse>(ICommand command);
    }
}