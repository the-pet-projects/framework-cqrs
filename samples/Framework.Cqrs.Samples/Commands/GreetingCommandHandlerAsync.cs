namespace Framework.Cqrs.Samples.Commands
{
    using System;
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Commands;
    using Queries;

    public class GreetingCommandHandlerAsync : ICommandHandlerAsync<GreetingCommand>
    {
        public async Task HandleAsync(GreetingCommand command)
        {
            await Task.Run(() => Console.WriteLine(command.Greet));
        }
    }
}