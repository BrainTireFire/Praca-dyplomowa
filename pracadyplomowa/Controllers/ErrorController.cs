using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;

namespace pracadyplomowa;

[Route("errors/{code}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : BaseApiController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}