using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs.Encounter;
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
        var encounter = await _encounterService.GetEncounterAsync(encounterId);
        
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
    
}