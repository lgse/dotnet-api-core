namespace API.Core.Http.Exceptions
{
    public class HttpBadRequestException : HttpException
    {
        public new int StatusCode = 400;

        public HttpBadRequestException(string message) : base(message)
        {
        }
    }
}
