using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.Core.Extensions;

namespace API.Core.Http.Response
{
    public static class JsonResponseWriter
    {
        public static Task WriteAsync(HttpContext context, object value)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            return response.WriteAsync(value.ToJsonString());
        }

        public static Task WriteAsync(HttpContext context, string value)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            return response.WriteAsync(value);
        }
    }
}
