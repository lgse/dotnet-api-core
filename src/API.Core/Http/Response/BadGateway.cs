using Microsoft.AspNetCore.Http;

#nullable enable

namespace API.Core.Http.Response
{
    public class BadGateway : ErrorResponse
    {
        public BadGateway() : base(StatusCodes.Status502BadGateway, "Bad Gateway")
        {
        }

        public BadGateway(MetaError error) : base(StatusCodes.Status502BadGateway, error)
        {
        }

        public BadGateway(string error) : base(StatusCodes.Status502BadGateway, error)
        {
        }

        public string? GatewayResponse { get; init; }
    }
}
