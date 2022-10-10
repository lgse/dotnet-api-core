using Microsoft.AspNetCore.Mvc;
using API.Core.Http.Exceptions;

namespace API.Core.Http.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("{*url}", Order = 999)]
    public class ErrorController : Controller
    {
        public IActionResult CatchAll()
        {
            throw new HttpNotFoundException("Route not found");
        }
    }
}
