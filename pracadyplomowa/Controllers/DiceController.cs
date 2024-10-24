using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Repository;

namespace pracadyplomowa.Controllers
{
    public class DiceController()
    {
        [HttpPost("/roll-dice")]
        public Task<IActionResult<string[]>> GetAllDice(DiceDto)
        {

        }
    }
}