using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.DTOs.Session;
using pracadyplomowa.Services.Encounter;
using static pracadyplomowa.Services.Encounter.EncounterService;

namespace pracadyplomowa.Controllers;

[Authorize]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;

    public EncounterController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }
    
    [HttpGet("myEncounters/{campaignId}")]
    public async Task<ActionResult<IEnumerable<EncounterShortDto>>> GetEncounters(int campaignId, [FromQuery] EncounterParams encounterParams)
    {
        var userId = User.GetUserId();
        var encounters = await _encounterService.GetEncountersAsync(userId, campaignId, encounterParams);

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
    [HttpGet("{encounterId}/gmCheck")]
    public ActionResult<List<InitiativeQueueItemDto>> CheckIfIsGM(int encounterId)
    {
        var result = _encounterService.CheckIfIsGM(encounterId, User.GetUserId());
        return Ok(result);
    }
    [HttpGet("{encounterId}/turnCheck/{characterId}")]
    public async Task<ActionResult<List<InitiativeQueueItemDto>>> CheckIfItsMyTurn(int encounterId, int characterId)
    {
        var result = await _encounterService.CheckIfItsMyTurn(encounterId, characterId, User.GetUserId());
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
    [HttpPost("{encounterId}/nextTurn")]
    public async Task<ActionResult<List<InitiativeQueueItemDto>>> NextTurn(int encounterId, int characterId)
    {
        await _encounterService.NextTurn(encounterId);
        return Ok();
    }
    [HttpGet("{encounterId}/controlledCharacters")]
    public async Task<ActionResult<List<int>>> GetControlledCharacters(int encounterId)
    {
        var result = await _encounterService.GetControlledCharacters(encounterId, User.GetUserId());
        return Ok(result);
    }
    [HttpGet("{encounterId}/participanceData/{characterId}")]
    public async Task<ActionResult<Models.DTOs.Session.ParticipanceDataDto>> GetParticipanceData(int encounterId, int characterId)
    {
        var result = await _encounterService.GetParticipanceData(encounterId, characterId);
        return Ok(result);
    }
    [HttpPatch("{encounterId}/participanceData/{characterId}")]
    public async Task<ActionResult<Models.DTOs.Session.ParticipanceDataDto>> GetParticipanceData(int encounterId, int characterId, [FromBody] Models.DTOs.Session.ParticipanceDataDto participanceDataDto)
    {
        await _encounterService.UpdateParticipanceData(encounterId, characterId, participanceDataDto);
        return Ok();
    }
    [HttpPost("{encounterId}/movement/{characterId}")]
    public async Task<ActionResult<List<int>>> MoveCharacter(int encounterId, int characterId, [FromBody] List<int> fieldIds)
    {
        var result = await _encounterService.MoveCharacter(encounterId, characterId, fieldIds);
        return Ok(result);
    }
    [HttpGet("{encounterId}/conditionalEffectsForAttackRoll")]
    public async Task<ActionResult<WeaponAttackConditionalEffectsDtos>> GetConditionalEffectsFromAttackRoll(int encounterId, [FromQuery] int characterId, [FromQuery] int targetId, [FromQuery] int weaponId, [FromQuery] bool isRanged)
    {
        try{
            var result = await _encounterService.GetConditionalEffectForWeaponAttackRoll(encounterId, characterId, weaponId, targetId, isRanged);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("{encounterId}/attackRoll")]
    public async Task<ActionResult<WeaponAttackConditionalEffectsDtos>> MakeAttackRoll(int encounterId, [FromQuery] int characterId, [FromQuery] int targetId, [FromQuery] int weaponId, [FromQuery] bool isRanged, [FromBody] ConditionalEffectsDtos conditionalEffectsDtos)
    {
        try{
            var result = await _encounterService.MakeAttackRoll(encounterId, characterId, weaponId, targetId, isRanged, conditionalEffectsDtos.CasterConditionalEffects, conditionalEffectsDtos.TargetsConditionalEffects[targetId]);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{encounterId}/conditionalEffectsForWeaponHit")]
    public async Task<ActionResult<WeaponAttackConditionalEffectsDtos>> GetConditionalEffectsForWeaponHit(int encounterId, [FromQuery] int characterId, [FromQuery] int targetId, [FromQuery] int weaponId)
    {
        try{
            var result = await _encounterService.GetConditionalEffectForWeaponHit(encounterId, characterId, weaponId, targetId);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }
    // [HttpPost("{encounterId}/weaponHit")]
    // public async Task<ActionResult<WeaponAttackConditionalEffectsDtos>> ApplyWeaponHitEffects(int encounterId, [FromQuery] int characterId, [FromQuery] int targetId, [FromQuery] int weaponId, [FromBody] ConditionalEffectsDtos conditionalEffectsDtos)
    // {
    //     try{
    //         var result = await _encounterService.MakeAttackRoll(encounterId, characterId, weaponId, targetId, isRanged, conditionalEffectsDtos.CasterConditionalEffects, conditionalEffectsDtos.TargetsConditionalEffects[targetId]);
    //         return Ok(result);
    //     }
    //     catch(SessionNotFoundException ex){
    //         return NotFound(ex.Message);
    //     }
    //     catch(SessionBadRequestException ex){
    //         return BadRequest(ex.Message);
    //     }
    // }
}