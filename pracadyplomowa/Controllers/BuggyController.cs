using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;

namespace pracadyplomowa;

public class BuggyController : BaseApiController
{
    private readonly AppDbContext _context;
    public BuggyController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("testauth")]
    [Authorize]
    public ActionResult<string> GetSecretText()
    {
        return "You got me! This is top secret text! This is only for authorized users!";
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        return NotFound(new ApiResponse(404));
    }

    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var thing = _context.Users.Find(42);

        var thingToReturn = thing.ToString();

        return Ok();
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetNotFoundRequest(int id)
    {
        return Ok();
    }
}