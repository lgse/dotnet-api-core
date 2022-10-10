namespace API.Core.Http.Response
{
    public class ErrorResponse : Response
    {
        protected ErrorResponse(int statusCode, string error) : base(statusCode)
        {
            Error = new MetaError(error);
        }

        protected ErrorResponse(int statusCode, MetaError error) : base(statusCode)
        {
            Error = error;
        }

        public MetaError Error { get; init; }
    }
}
