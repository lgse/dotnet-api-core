using Microsoft.AspNetCore.Http;

namespace API.Core.Http.Response
{
    public class NotFound : ErrorResponse
    {
        public NotFound() : base(StatusCodes.Status404NotFound, "Not Found")
        {
        }


        public NotFound(MetaError error) : base(StatusCodes.Status404NotFound, error)
        {
        }

        public NotFound(string error) : base(StatusCodes.Status404NotFound, error)
        {
        }
    }
}
