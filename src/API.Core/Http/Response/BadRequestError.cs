using Microsoft.AspNetCore.Http;

namespace API.Core.Http.Response
{
    public class BadRequestError : ErrorResponse
    {
        public BadRequestError() : base(StatusCodes.Status400BadRequest, "Bad Request")
        {
        }

        public BadRequestError(MetaError error) : base(StatusCodes.Status400BadRequest, error)
        {
        }

        public BadRequestError(string error) : base(StatusCodes.Status400BadRequest, error)
        {
        }
    }
}
