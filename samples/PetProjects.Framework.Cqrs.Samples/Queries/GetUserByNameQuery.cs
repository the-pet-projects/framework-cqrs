﻿namespace PetProjects.Framework.Cqrs.Samples.Queries
{
    using PetProjects.Framework.Cqrs.Queries;

    public class GetUserByNameQuery : IQuery<User>
    {
        public GetUserByNameQuery(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}