using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Core.Http.Response
{
    public class JsonStringResult : ActionResult
    {
        private readonly string _value;

        public JsonStringResult(string value)
        {
            _value = value;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            return JsonResponseWriter.WriteAsync(context.HttpContext, _value);
        }
    }
}
