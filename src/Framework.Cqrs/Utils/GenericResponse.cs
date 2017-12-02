namespace PetProjects.Framework.Cqrs.Utils
{
    using System;

    public class Response<TResponseData>
    {
        public Response(Exception ex)
        {
            this.Data = default(TResponseData);
            this.Exception = ex;
        }

        public Response(TResponseData data)
        {
            this.Data = data;
            this.Exception = null;
        }

        public virtual TResponseData Data { get; }

        public virtual Exception Exception { get; }

        public virtual bool HasException()
        {
            return this.Exception != null;
        }

        public virtual bool Success => !this.HasException();
    }
}