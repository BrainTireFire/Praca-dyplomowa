using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.DTOs.Session;
using pracadyplomowa.Models.Enums;
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
    public async Task<ActionResult> SetEncounterPosition(int encounterId, [FromBody] SetEncounterPositionDto setEncounterPositionDto)
    {
        var ownerId = User.GetUserId();
        var result = await _encounterService.SetEncounterPositionAsync(ownerId, encounterId, setEncounterPositionDto);

        return result;
    }
    
    [HttpPut("toggleActive/{encounterId}")]
    public async Task<ActionResult> ToggleEncounterActive(int encounterId)
    {
        var ownerId = User.GetUserId();
        var result = await _encounterService.ToogleEncounterActiveAsync(ownerId, encounterId);
    
        return result;
    }
    
    [HttpDelete("{encounterId}")]
    public async Task<ActionResult> RemoveEncounter(int encounterId)
    {
        var ownerId = User.GetUserId();
        var result = await _encounterService.RemoveEncounterAsync(ownerId, encounterId);
    
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
    
    [HttpPatch("{encounterId}/initiative/{characterId}/up")]
    public async Task<ActionResult> MoveUpQueue(int encounterId, int characterId)
    {
        try{
            await _encounterService.MoveUpQueue(encounterId, characterId, User.GetUserId());
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
        return Ok();
    }
    
    [HttpPatch("{encounterId}/initiative/{characterId}/down")]
    public async Task<ActionResult> MoveDownQueue(int encounterId, int characterId)
    {
        try{
            await _encounterService.MoveDownQueue(encounterId, characterId, User.GetUserId());
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
        return Ok();
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
    
    [HttpDelete("{encounterId}/participanceData/{characterId}")]
    public async Task<ActionResult> RemoveParticipanceData(int encounterId, int characterId){
        try{
            await _encounterService.DeleteParticipanceData(encounterId, characterId, User.GetUserId());
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
        return Ok();
    }


    [HttpPost("{encounterId}/movement/{characterId}")]
    public async Task<ActionResult<List<int>>> MoveCharacter(int encounterId, int characterId, [FromBody] List<int> fieldIds)
    {
        var result = await _encounterService.MoveCharacter(encounterId, characterId, fieldIds);
        if(result.Count != fieldIds.Count){
            return BadRequest("Movement not possible");
        }
        return Ok(result);
    }

    [HttpPost("{encounterId}/attackRoll")]
    public async Task<ActionResult<HitType>> MakeAttackRoll(int encounterId, [FromQuery] int characterId, [FromQuery] int targetId, [FromQuery] int weaponId, [FromQuery] bool isRanged, [FromBody] ApprovedConditionalEffectsDto conditionalEffectsDtos)
    {
        try{
            var result = await _encounterService.MakeWeaponAttackRoll(encounterId, characterId, weaponId, targetId, isRanged, conditionalEffectsDtos.CasterConditionalEffects, conditionalEffectsDtos.TargetConditionalEffects);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("{encounterId}/weaponHit")]
    public async Task<ActionResult<Dictionary<DamageType, int>>> ApplyWeaponHit(int encounterId, [FromQuery] int characterId, [FromQuery] int targetId, [FromQuery] int weaponId, [FromQuery] bool isRanged, [FromQuery] bool isCritical, [FromBody] ApprovedConditionalEffectsDto conditionalEffectsDtos)
    {
        try{
            var result = await _encounterService.ApplyWeaponHit(encounterId, characterId, weaponId, targetId, isRanged, isCritical, conditionalEffectsDtos.CasterConditionalEffects, conditionalEffectsDtos.TargetConditionalEffects);
            return Ok(result.DamageTaken);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("{encounterId}/weaponAttack")]
    public async Task<ActionResult<AttackRollAndDamageResultDto>> MakeAttackRollAndApplyDamage(int encounterId, [FromQuery] int characterId, [FromQuery] int targetId, [FromQuery] int weaponId, [FromQuery] bool isRanged, [FromBody] ApprovedConditionalEffectsDto conditionalEffectsDtos)
    {
        try{
            var result = await _encounterService.AttackRollAndDamage(encounterId, characterId, weaponId, targetId, isRanged, conditionalEffectsDtos.CasterConditionalEffects, conditionalEffectsDtos.TargetConditionalEffects);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{encounterId}/weaponData")]
    public async Task<ActionResult<WeaponDamageAndPowersDto>> GetWeaponDamageAndPowersOnHit(int encounterId, [FromQuery] int characterId, [FromQuery] int weaponId){
        
        try{
            var result = await _encounterService.GetWeaponData(encounterId, characterId, weaponId);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{encounterId}/conditionalEffects")]
    public async Task<ActionResult<ConditionalEffectsSetDto>> GetConditionalEffects(int encounterId, [FromQuery] int characterId, [FromQuery] int targetId)
    {
        try{
            var result = await _encounterService.GetConditionalEffects(encounterId, characterId, targetId);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{encounterId}/registerAttackMade")]
    public async Task<ActionResult<ConditionalEffectsSetDto>> RegisterAttackMade(int encounterId, [FromQuery] int characterId, [FromQuery] int targetId)
    {
        try{
            var result = await _encounterService.GetConditionalEffects(encounterId, characterId, targetId);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{encounterId}/weaponAttackData")]
    public async Task<ActionResult<WeaponAttackDataDto>> GetWeaponDamageAndPowersOnHitAndConditionalEffects(int encounterId, [FromQuery] int characterId, [FromQuery] int weaponId, [FromQuery] int targetId){
        
        try{
            var weaponData = await _encounterService.GetWeaponData(encounterId, characterId, weaponId);
            var conditionalEffects = await _encounterService.GetConditionalEffects(encounterId, characterId, targetId);
            var result = new WeaponAttackDataDto
            {
                WeaponDamageAndPowers = weaponData,
                ConditionalEffects = conditionalEffects
            };
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{encounterId}/powerCastData")]
    public async Task<ActionResult<PowerDataAndConditionalEffectsDto>> GetPowerCastConditionalEffects(int encounterId, [FromQuery] int characterId, [FromQuery] int powerId, [FromQuery] List<int> targetIds){
        
        try{
            var powerData = await _encounterService.GetPowerData(encounterId, characterId, powerId);
            var conditionalEffects = await _encounterService.GetConditionalEffects(encounterId, characterId, targetIds);
            var result = new PowerDataAndConditionalEffectsDto
            {
                PowerData = powerData,
                ConditionalEffects = conditionalEffects
            };
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{encounterId}/makeWeaponAttack")]
    public async Task<ActionResult<WeaponAttackResultDto>> MakeWeaponAttack(int encounterId, [FromQuery] int characterId, [FromQuery] int weaponId, [FromQuery] int targetId, [FromQuery] bool isRanged, [FromBody] WeaponAttackIncomingDataDto approvedConditionalEffects){
        
        try{
            var result = await _encounterService.MakeWeaponAttack(encounterId, characterId, weaponId, targetId, isRanged, approvedConditionalEffects);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{encounterId}/castPower")]
    public async Task<ActionResult<CastPowerResultDto>> CastPower(int encounterId, [FromQuery] int characterId, [FromQuery] int powerId, [FromBody] CastPowerIncomingDataDto incomingDataDto){
        
        try{
            var result = await _encounterService.CastPower(encounterId, characterId, powerId, incomingDataDto);
            return Ok(result);
        }
        catch(SessionNotFoundException ex){
            return NotFound(ex.Message);
        }
        catch(SessionBadRequestException ex){
            return BadRequest(ex.Message);
        }
    }
}