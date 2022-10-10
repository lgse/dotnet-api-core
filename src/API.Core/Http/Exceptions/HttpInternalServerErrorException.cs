#nullable enable

namespace API.Core.Http.Exceptions
{
    public class HttpInternalServerErrorException : HttpException
    {
        public new int StatusCode = 500;

        public string? GatewayResponse { get; init; }

        public HttpInternalServerErrorException(string message) : base(message)
        {
        }
    }
}
