namespace PetProjects.Framework.Cqrs.Samples.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Queries;

    public class GetUserByNameQueryHandlerAsync : IQueryHandlerAsync<GetUserByNameQuery, User>
    {
        public GetUserByNameQueryHandlerAsync(List<User> users)
        {
            this.Users = users;
        }

        public List<User> Users { get; set; }

        public async Task<User> HandleAsync(GetUserByNameQuery query)
        {
            return await Task.Run(() => this.Users.First(u => u.Name.Equals(query.Name)));
        }
    }
}