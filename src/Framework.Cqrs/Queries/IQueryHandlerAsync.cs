namespace PetProjects.Framework.Cqrs.Queries
{
    using System.Threading.Tasks;

    public interface IQueryHandlerAsync<in TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}