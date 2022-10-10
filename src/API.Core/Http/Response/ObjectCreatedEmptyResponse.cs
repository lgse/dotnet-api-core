using Microsoft.AspNetCore.Http;

namespace API.Core.Http.Response
{
    public class ObjectCreatedEmptyResponse : Response
    {
        public ObjectCreatedEmptyResponse() : base(StatusCodes.Status201Created)
        {
        }
    }
}
