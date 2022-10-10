using Microsoft.AspNetCore.Http;

namespace API.Core.Http.Response
{
    public class Unauthorized : ErrorResponse
    {
        public Unauthorized() : base(StatusCodes.Status401Unauthorized, new MetaError("Unauthorized"))
        {
        }

        public Unauthorized(MetaError error) : base(StatusCodes.Status401Unauthorized, error)
        {
        }

        public Unauthorized(string error) : base(StatusCodes.Status401Unauthorized, error)
        {
        }
    }
}
