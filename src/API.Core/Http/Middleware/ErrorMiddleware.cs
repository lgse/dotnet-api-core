using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using API.Core.Http.Exceptions;
using API.Core.Http.Response;
using API.Core.Repositories;
using FluentValidation;

#nullable enable

namespace API.Core.Http.Middleware
{
    public class ErrorMiddleware : IAsyncExceptionFilter
    {
        private readonly bool _displayErrorDetails;
        private readonly ILogger _logger;

        public ErrorMiddleware(ILoggerFactory loggerFactory, bool displayErrorDetails)
        {
            _displayErrorDetails = displayErrorDetails;
            _logger = loggerFactory.CreateLogger<ErrorMiddleware>();
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            switch (context.Exception) {
                case HttpBadRequestException: {
                    var badRequestError = new BadRequestError(context.Exception.Message);

                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                    context.Result = new JsonObjectResult(badRequestError) {
                        StatusCode = StatusCodes.Status400BadRequest,
                    };

                    break;
                }

                case EntityNotFoundException:
                case HttpNotFoundException: {
                    var notFound = new NotFound(context.Exception.Message);

                    context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                    context.Result = new JsonObjectResult(notFound) {
                        StatusCode = StatusCodes.Status404NotFound,
                    };

                    break;
                }

                case ValidationException validationException:
                    var validationErrors = validationException.Errors
                        .Select(kvp => new ValidationError {
                            Field = JsonNamingPolicy.CamelCase.ConvertName(kvp.PropertyName.Replace("$.", "")),
                            Message = kvp.ErrorMessage,
                        }).ToList();

                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var badRequestValidation = new BadRequestValidation(validationErrors);
                    context.Result = new JsonObjectResult(badRequestValidation);

                    break;

                case HttpBadGatewayException badGatewayException:
                    context.HttpContext.Response.StatusCode = badGatewayException.StatusCode;

                    var badGateway = new BadGateway()
                    {
                        GatewayResponse = badGatewayException.GatewayResponse
                    };

                    context.Result = new JsonObjectResult(badGateway) {
                        StatusCode = StatusCodes.Status500InternalServerError,
                    };

                    break;


                default: {
                    var details = ExceptionDetails.FromException(context.Exception, _displayErrorDetails);
                    var internalServerError = new InternalServerError(new MetaError("Internal server error", details));

                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    context.Result = new JsonObjectResult(internalServerError) {
                        StatusCode = StatusCodes.Status500InternalServerError,
                    };

                    break;
                }
            }

            _logger.LogError("{Type}: {Message}", context.Exception.GetType(), context.Exception.Message);

            return Task.CompletedTask;
        }
    }
}
