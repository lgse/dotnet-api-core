namespace API.Core.Http.Exceptions
{
    public class HttpNotFoundException : HttpException
    {
        public new int StatusCode = 404;

        public HttpNotFoundException(string message) : base(message)
        {
        }
    }
}
