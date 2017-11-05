namespace PetProjects.Framework.Cqrs.Commands
{
    using System.Threading.Tasks;

    public interface ICommandHandlerAsync<in TCommand>
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}