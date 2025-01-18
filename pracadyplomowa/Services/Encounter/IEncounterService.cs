using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.DTOs.Session;

namespace pracadyplomowa.Services.Encounter;

public interface IEncounterService
{
    Task<PagedList<EncounterShortDto>> GetEncountersAsync(int ownedId, EncounterParams encounterParams);
    Task<EncounterSummaryDto> GetEncounterAsync(int encounterId, int userId);
    Task<ActionResult> CreateEncounterAsync(int ownerId, CreateEncounterDto createEncounterDto);
    Task<ActionResult> UpdateEncounterAsync(int ownerId, int encounterId, UpdateEncounterDto updateEncounterDto);
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
    Task<List<int>> MoveCharacter(int encounterId, int characterId, List<int> fieldIds);
}