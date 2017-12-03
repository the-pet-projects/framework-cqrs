namespace PetProjects.Framework.Cqrs.Mediator
{
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.Framework.Cqrs.Utils;

    /// <summary>
    /// This mediator is different from V2.IMediator because it executes only one handler per query/command.
    /// </summary>
    public interface ISimpleMediator
    {
        Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
        
        Task RunCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}