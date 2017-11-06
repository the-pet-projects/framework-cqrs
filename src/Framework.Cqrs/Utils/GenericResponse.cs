namespace PetProjects.Framework.Cqrs.Utils
{
    using System;

    public class Response<TResponseData>
    {
        public virtual TResponseData Data { get; set; }

        public virtual Exception Exception { get; set; }

        public virtual bool HasException()
        {
            return this.Exception != null;
        }
    }
}