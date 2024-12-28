namespace pracadyplomowa.Repository.Encounter;

public interface IEncounterRepository : IBaseRepository<Models.Entities.Campaign.Encounter>
{
    public Task<PagedList<Models.Entities.Campaign.Encounter>> GetEncounters(int ownerId, EncounterParams encounterParams);
    
    public Task<Models.Entities.Campaign.Encounter> GetEncounterSummary(int encounterId);
}