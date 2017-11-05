namespace PetProjects.Framework.Cqrs.Commands.Sync
{

    public interface ICommandHandlerWithResponse<in TCommand, TResponse>
        where TCommand : ICommand
    {
        TResponse Handle(TCommand command);
    }
}