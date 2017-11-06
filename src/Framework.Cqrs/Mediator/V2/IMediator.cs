namespace PetProjects.Framework.Cqrs.Mediator.V2
{
    using System.Threading.Tasks;

    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.Framework.Cqrs.Utils;

    public interface IMediator
    {
        TResponse Query<TResponse>(IQuery<TResponse> query);

        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query);

        void RunCommand<TCommand>(TCommand command) where TCommand : ICommand;

        Task RunCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}