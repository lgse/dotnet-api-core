using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace API.Core.Http.Response
{
    public class BadRequestValidation : Response
    {
        public BadRequestValidation(List<ValidationError> validationErrors)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ValidationErrors = validationErrors;
        }

        public List<ValidationError> ValidationErrors { get; set; }
    }
}
