namespace PetProjects.Framework.Cqrs.Commands
{
    using System.Threading.Tasks;

    public interface ICommandHandlerWithResponseAsync<in TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        Task<TResponse> HandleAsync(TCommand command);
    }
}