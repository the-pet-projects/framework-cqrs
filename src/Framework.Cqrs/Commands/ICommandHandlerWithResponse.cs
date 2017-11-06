namespace PetProjects.Framework.Cqrs.Commands
{

    public interface ICommandHandlerWithResponse<in TCommand, out TResponse>
        where TCommand : ICommand
    {
        TResponse Handle(TCommand command);
    }
}