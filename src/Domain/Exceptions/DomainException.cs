namespace Domain.Exceptions
{
    using System;
    using System.Net;

    public class DomainException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public DomainException(
            string message,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}