namespace PetProjects.Framework.Cqrs.Mediator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.Framework.Cqrs.Utils;

    public interface IMediator
    {
        IEnumerable<Response<TResponse>> Query<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;

        Task<IEnumerable<Response<TResponse>>> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;

        void RunCommand<TCommand>(TCommand command) where TCommand : ICommand;

        Task RunCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}