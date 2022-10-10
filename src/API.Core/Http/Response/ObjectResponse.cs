namespace API.Core.Http.Response
{
    public class ObjectResponse<TObjectType> : Response
    {
        public ObjectResponse(TObjectType obj)
        {
            Data = obj;
        }

        public TObjectType Data { get; protected init; }
    }
}
