#nullable enable

namespace API.Core.Http.Response
{
    public class MetaError
    {
        public MetaError(string message)
        {
            Message = message;
        }

        public MetaError(string message, object? details)
        {
            Message = message;
            Details = details;
        }

        public string Message { get; }

        public object? Details { get; }
    }
}
