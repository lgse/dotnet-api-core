namespace API.Core.Http.Response
{
    public abstract class Response
    {
        protected Response(int statusCode = 200)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
