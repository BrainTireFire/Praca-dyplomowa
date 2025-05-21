using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.DTOs.Session;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Services.Encounter;

public interface IEncounterService
{
    Task<PagedList<EncounterShortDto>> GetEncountersAsync(int ownedId, int campaignId, EncounterParams encounterParams);
    Task<EncounterSummaryDto> GetEncounterAsync(int encounterId, int userId);
    Task<ActionResult> CreateEncounterAsync(int ownerId, CreateEncounterDto createEncounterDto);
    Task<ActionResult> ToogleEncounterActiveAsync(int ownerId, int encounterId);
    Task<ActionResult> SetEncounterPositionAsync(int ownerId, int encounterId, SetEncounterPositionDto setEncounterPositionDto);
    Task<ActionResult> RollInitiativeAsync(int encounterId);
    Task<ActionResult> GetInitiativeQueueAsync(int encounterId);
    Task<ActionResult> ModifyInitiativeQueueAsync(int encounterId, List<ModifyInitiativeQueueOrderItem> newQueue);
    bool CheckIfIsGM(int encounterId, int userId);
    Task<bool> CheckIfItsMyTurn(int encounterId, int characterId, int userId);
    Task SetActiveTurn(int encounterId, int activeCharacterId);
    Task NextTurn(int encounterId);
    Task<List<int>> GetControlledCharacters(int encounterId, int userId);
    Task<Models.DTOs.Session.ParticipanceDataDto> GetParticipanceData(int encounterId, int characterId);
    Task UpdateParticipanceData(int encounterId, int characterId, Models.DTOs.Session.ParticipanceDataDto participanceDataDto);
    Task DeleteParticipanceData(int encounterId, int characterId, int userId);
    Task<List<int>> MoveCharacter(int encounterId, int characterId, List<int> fieldIds);
    Task<ConditionalEffectsSetDto> GetConditionalEffects(int encounterId, int characterId, int targetId);
    Task<ConditionalEffectsSetForManyTargetsDto> GetConditionalEffects(int encounterId, int characterId, List<int> targetId);
    Task<HitType> MakeWeaponAttackRoll(int encounterId, int characterId, int weaponId, int targetId, bool rangedAttack, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds);
    Task<WeaponDamageAndPowersDto> GetWeaponData(int encounterId, int characterId, int weaponId);
    Task<Character.WeaponHitResult> ApplyWeaponHit(int encounterId, int characterId, int weaponId, int targetId, bool rangedAttack, bool criticalHit, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds);
    Task<AttackRollAndDamageResultDto> AttackRollAndDamage(int encounterId, int characterId, int weaponId, int targetId, bool rangedAttack, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds);
    Task<WeaponAttackResultDto> MakeWeaponAttack(int encounterId, int characterId, int weaponId, int targetId, bool isRanged, WeaponAttackIncomingDataDto approvedConditionalEffects);
    Task<PowerDataForResolutionDto> GetPowerData(int encounterId, int characterId, int powerId, int? powerLevel, int? resourceLevel);
    Task<CastPowerResultDto> CastPower(int encounterId, int characterId, int powerId, int? powerLevel, int? immaterialResourceLevel, CastPowerIncomingDataDto incomingDataDto);
    Task MoveUpQueue(int encounterId, int characterId, int userId);
    Task MoveDownQueue(int encounterId, int characterId, int userId);
    Task<ActionResult> RemoveEncounterAsync(int ownerId, int encounterId);
    bool IsSingleAttackAvailable(Character character, ParticipanceData participance, bool reduceActions);
    Task<List<PowerForEncounterDto>> GetPowersForEncounter(Character character, ParticipanceData participanceData);
    List<WeaponAttacksForEncounterDto> GetWeaponAttacksForEncounter(Character character, ParticipanceData participanceData);
}