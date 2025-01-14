using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.DTOs.Session;
using pracadyplomowa.Services.Encounter;

namespace pracadyplomowa.Controllers;

[Authorize]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;

    public EncounterController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }
    
    [HttpGet("myEncounters")]
    public async Task<ActionResult<IEnumerable<EncounterShortDto>>> GetEncounters([FromQuery] EncounterParams encounterParams)
    {
        var userId = User.GetUserId();
        var encounters = await _encounterService.GetEncountersAsync(userId, encounterParams);

        Response.AddPaginationHeader(encounters);

        return Ok(encounters);
    }
    

    [HttpGet("{encounterId}")]
    public async Task<ActionResult<EncounterSummaryDto>> GetEncounter(int encounterId)
    {
        var encounter = await _encounterService.GetEncounterAsync(encounterId, User.GetUserId());
        
        return Ok(encounter);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateEncounter(CreateEncounterDto createEncounterDto)
    {
        var ownerId = User.GetUserId();
        var result = await _encounterService.CreateEncounterAsync(ownerId, createEncounterDto);

        return result;
    }
    
    [HttpPut("placeEncounter/{encounterId}")]
    public async Task<ActionResult> UpdateEncounter(int encounterId, [FromBody] UpdateEncounterDto updateEncounterDto)
    {
        var ownerId = User.GetUserId();
        var result = await _encounterService.UpdateEncounterAsync(ownerId, encounterId, updateEncounterDto);

        return result;
    }
    
    
    [HttpPost("{encounterId}/initiative")]
    public async Task<ActionResult<EncounterSummaryDto>> RollInitiative(int encounterId)
    {
        return await _encounterService.RollInitiativeAsync(encounterId);
    }
    [HttpGet("{encounterId}/initiative")]
    public async Task<ActionResult<List<InitiativeQueueItemDto>>> InitiativeQueue(int encounterId)
    {
        return await _encounterService.GetInitiativeQueueAsync(encounterId);
    }
    [HttpPatch("{encounterId}/initiative")]
    public async Task<ActionResult> ModifyInitiativeQueue(int encounterId, List<ModifyInitiativeQueueOrderItem> newQueue)
    {
        return await _encounterService.ModifyInitiativeQueueAsync(encounterId, newQueue);
    }
    [HttpGet("{encounterId}/movement/{participanceId}")]
    public async Task<ActionResult<List<InitiativeQueueItemDto>>> MoveCharacter(int encounterId, int participanceId, [FromBody] List<int> fieldIds)
    {
        return await _encounterService.GetInitiativeQueueAsync(encounterId);
    }
    [HttpGet("{encounterId}/gmCheck")]
    public ActionResult<List<InitiativeQueueItemDto>> CheckIfIsGM(int encounterId)
    {
        var result = _encounterService.CheckIfIsGM(encounterId, User.GetUserId());
        return Ok(result);
    }
    [HttpPost("{encounterId}/initiative/{characterId}")]
    public async Task<ActionResult<List<InitiativeQueueItemDto>>> SetActiveTurn(int encounterId, int characterId)
    {
        var result = _encounterService.CheckIfIsGM(encounterId, User.GetUserId());
        if(!result){
            return Unauthorized("You are not the Game Master");
        }
        await _encounterService.SetActiveTurn(encounterId, characterId);
        return Ok();
    }
}