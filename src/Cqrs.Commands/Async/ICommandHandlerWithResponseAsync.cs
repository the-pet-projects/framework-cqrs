namespace PetProjects.Framework.Cqrs.Commands.Async
{
    using System.Threading.Tasks;

    public interface ICommandHandlerWithResponseAsync<in TCommand, TResponse>
        where TCommand : ICommand
    {
        Task<TResponse> HandleAsync(TCommand command);
    }
}