using System.Collections.Generic;

namespace API.Core.Http.Response
{
    public class ObjectCollectionResponse<TObjectType> : Response
    {
        public ObjectCollectionResponse(List<TObjectType> collection)
        {
            Data = collection;
        }

        public List<TObjectType> Data { get; }
    }
}
