namespace PetProjects.Framework.Cqrs.Commands.Sync
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}