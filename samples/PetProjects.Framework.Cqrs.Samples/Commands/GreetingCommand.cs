namespace Framework.Cqrs.Samples.Commands
{
    using PetProjects.Framework.Cqrs.Commands;

    public class GreetingCommand : ICommand
    {
        public GreetingCommand(string greeting)
        {
            this.Greet = greeting;
        }

        public string Greet { get; private set; }
    }
}