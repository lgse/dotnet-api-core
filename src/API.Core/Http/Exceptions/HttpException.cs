using System;

#nullable enable

namespace API.Core.Http.Exceptions
{
    public abstract class HttpException : Exception
    {
        public int StatusCode { get; set; }

        protected HttpException(string? message) : base(message)
        {
        }
    }
}
