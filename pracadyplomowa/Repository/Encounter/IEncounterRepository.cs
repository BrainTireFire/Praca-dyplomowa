namespace pracadyplomowa.Repository.Encounter;

public interface IEncounterRepository : IBaseRepository<Models.Entities.Campaign.Encounter>
{
    public Task<PagedList<Models.Entities.Campaign.Encounter>> GetEncounters(int ownerId, int campaignId, EncounterParams encounterParams);
    
    public Task<Models.Entities.Campaign.Encounter> GetEncounterSummary(int encounterId);

    public Task<Models.Entities.Campaign.Encounter> GetEncounterWithParticipances(int encounterId);
    public Task<Models.Entities.Campaign.Encounter> GetEncounterWithParticipance(int encounterId, int characterId);

    public Task<Models.Entities.Campaign.Encounter> GetEncounterWithPlayerDetails(int encounterId);
}