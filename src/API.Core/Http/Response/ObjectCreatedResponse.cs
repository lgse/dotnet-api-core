using Microsoft.AspNetCore.Http;

namespace API.Core.Http.Response
{
    public class ObjectCreatedResponse<TObjectType> : ObjectResponse<TObjectType>
    {
        public ObjectCreatedResponse(TObjectType obj) : base(obj)
        {
            StatusCode = StatusCodes.Status201Created;
            Data = obj;
        }
    }
}
