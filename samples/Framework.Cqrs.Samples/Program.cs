namespace Framework.Cqrs.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Commands;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Mediator;
    using PetProjects.Framework.Cqrs.Queries;
    using Queries;

    public class Program
    {
        public static void Main(string[] args)
        {
            var users =  new List<User>
            {
                new User("Pedro", 25),
                new User("João", 25)
            };
            var mediator = new Mediator();
            mediator.Register<ICommandHandlerAsync<GreetingCommand>>(c => new GreetingCommandHandlerAsync());
            mediator.Register<IQueryHandlerAsync<GetUserByNameQuery, User>>(c => new GetUserByNameQueryHandlerAsync(users));

            MainAsync(mediator).Wait();

            Console.ReadLine();
        }

        public static async Task MainAsync(IMediator mediator)
        {
            Console.WriteLine("Command Example");
            var greetingCommand = new GreetingCommand("Say hello!");

            await mediator.RunCommandAsync(greetingCommand);

            Console.WriteLine("Query Example");
            var getUserByNameQuery = new GetUserByNameQuery("Pedro");

            var result = await mediator.QueryAsync<GetUserByNameQuery, User>(getUserByNameQuery);
            Console.WriteLine($"Name: {result.Name} | Age: {result.Age}");
        }
    }
}