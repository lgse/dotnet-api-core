namespace API.Core.Http.Exceptions
{
    public class HttpUnauthorizedException : HttpException
    {
        public new int StatusCode = 401;

        public HttpUnauthorizedException(string message = "Unauthorized") : base(message)
        {
        }
    }
}
