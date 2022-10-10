using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Core.Http.Response
{
    public class JsonObjectResult : JsonResult
    {
        public JsonObjectResult(object value) : base(value)
        {
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            return JsonResponseWriter.WriteAsync(context.HttpContext, Value);
        }
    }
}
