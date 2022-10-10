using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Core.Http.Response;

namespace API.Core.Extensions
{
    public static class ApiBehaviorOptionsExtension
    {
        public static void SetupInvalidStateResponseFactory(this ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var validationErrors = new ValidationProblemDetails(context.ModelState)
                    .Errors
                    .Where(kvp => kvp.Key.Replace("$.", "") != "")
                    .Select(kvp => new ValidationError {
                        Field = JsonNamingPolicy.CamelCase.ConvertName(kvp.Key.Replace("$.", "")),
                        Message = string.Join(" - ", kvp.Value.ToArray()),
                    }).ToList();

                var response = new BadRequestValidation(validationErrors);

                return new JsonObjectResult(response);
            };
        }
    }
}
