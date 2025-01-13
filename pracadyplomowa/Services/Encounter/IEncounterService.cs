using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs.Encounter;

namespace pracadyplomowa.Services.Encounter;

public interface IEncounterService
{
    Task<PagedList<EncounterShortDto>> GetEncountersAsync(int ownedId, int campaignId, EncounterParams encounterParams);
    Task<EncounterSummaryDto> GetEncounterAsync(int encounterId);
    Task<ActionResult> CreateEncounterAsync(int ownerId, CreateEncounterDto createEncounterDto);
    Task<ActionResult> UpdateEncounterAsync(int ownerId, int encounterId, UpdateEncounterDto updateEncounterDto);
}