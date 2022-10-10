using Microsoft.AspNetCore.Http;

namespace API.Core.Http.Response
{
    public class InternalServerError : ErrorResponse
    {
        public InternalServerError() : base(StatusCodes.Status500InternalServerError, "Internal Server Error")
        {
        }

        public InternalServerError(MetaError error) : base(StatusCodes.Status500InternalServerError, error)
        {
        }

        public InternalServerError(string error) : base(StatusCodes.Status500InternalServerError, error)
        {
        }
    }
}
