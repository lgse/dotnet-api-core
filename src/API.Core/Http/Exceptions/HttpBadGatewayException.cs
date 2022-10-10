#nullable enable

namespace API.Core.Http.Exceptions
{
    public class HttpBadGatewayException : HttpException
    {
        public new int StatusCode = 502;

        public string? GatewayResponse { get; init; }

        public HttpBadGatewayException(string message) : base(message)
        {
        }
    }
}
