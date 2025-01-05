using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Repository;

namespace pracadyplomowa.Controllers
{
    public class DiceController() : BaseApiController
    {
        [HttpPost("/roll-dice")]
        public ActionResult<DiceRollDto> RollDice(DiceRollDto input)
        {
            DiceSet diceSet = new DiceSet(input);
            DiceRollDto results = new DiceRollDto(diceSet.RollSeparate());
            return Ok(results);
        }
    }
}