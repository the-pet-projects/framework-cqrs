namespace PetProjects.Framework.Cqrs.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Commands;
    using Cqrs.Commands;
    using Microsoft.Extensions.DependencyInjection;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.Framework.Cqrs.Resolvers;
    using PetProjects.Framework.Cqrs.Samples.Queries;
    using MediatorV1 = PetProjects.Framework.Cqrs.Mediator.V1;
    using MediatorV2 = PetProjects.Framework.Cqrs.Mediator.V2;

    public class Program
    {
        public static void Main(string[] args)
        {
            var users = new List<User>
            {
                new User("Pedro", 25),
                new User("João", 25)
            };

            Console.WriteLine("Mediator V1");
            var mediator = new MediatorV1.Mediator();
            mediator.Register<ICommandHandlerAsync<GreetingCommand>>(c => new GreetingCommandHandlerAsync());
            mediator.Register<IQueryHandlerAsync<GetUserByNameQuery, User>>(c => new GetUserByNameQueryHandlerAsync(users));

            MainAsync(mediator).Wait();

            Console.WriteLine("Mediator V2 - With DI");

            var container = new ServiceCollection();
            container.AddSingleton<ICommandHandlerAsync<GreetingCommand>, GreetingCommandHandlerAsync>();
            container.AddSingleton<IQueryHandlerAsync<GetUserByNameQuery, User>>(new GetUserByNameQueryHandlerAsync(users));
            container.AddSingleton<MediatorV2.IMediator>(new MediatorV2.Mediator(new AspNetCoreResolver(container)));

            MainWithDiAsync(container).Wait();

            Console.ReadLine();
        }

        public static async Task MainAsync(MediatorV1.IMediator mediator)
        {
            Console.WriteLine("Command Example");
            var greetingCommand = new GreetingCommand("Say hello!");

            await mediator.RunCommandAsync(greetingCommand);

            Console.WriteLine("Query Example");
            var getUserByNameQuery = new GetUserByNameQuery("Pedro");

            var result = await mediator.QueryAsync<GetUserByNameQuery, User>(getUserByNameQuery);
            Console.WriteLine($"Name: {result.Name} | Age: {result.Age}");
        }

        public static async Task MainWithDiAsync(IServiceCollection container)
        {
            var sp = container.BuildServiceProvider();
            var mediator = sp.GetService<MediatorV2.IMediator>();
            Console.WriteLine("Command Example");
            var greetingCommand = new GreetingCommand("Say hello!");

            await mediator.RunCommandAsync(greetingCommand);

            Console.WriteLine("Query Example");
            var getUserByNameQuery = new GetUserByNameQuery("Pedro");

            var result = await mediator.QueryAsync(getUserByNameQuery);
            Console.WriteLine($"Name: {result.Name} | Age: {result.Age}");
        }
    }
}