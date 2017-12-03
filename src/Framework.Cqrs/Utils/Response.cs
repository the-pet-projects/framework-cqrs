namespace PetProjects.Framework.Cqrs.Utils
{
    using System;

    public sealed class Response : Response<UnitType>
    {
        public Response(Exception ex) : base(ex)
        {
        }

        public Response(UnitType data) : base(data)
        {
        }
    }
}