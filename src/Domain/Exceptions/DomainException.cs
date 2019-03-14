namespace Domain.Exceptions
{
    using System;
    using System.Net;

    public class DomainException : Exception
    {
        readonly HttpStatusCode httpStatus;

        public DomainException(
            string message,
            HttpStatusCode httpStatus = HttpStatusCode.BadRequest)
            : base(message)
        {
            this.httpStatus = httpStatus;
        }
    }
}