using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa.Repository.Encounter;

public class EncounterRepository : BaseRepository<Models.Entities.Campaign.Encounter>, IEncounterRepository
{
    public EncounterRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Models.Entities.Campaign.Encounter>> GetEncounters(int ownerId, EncounterParams encounterParams)
    {
        var query = _context.Encounters
            .Where(e => e.R_OwnerId == ownerId)
            .Include(e => e.R_Campaign)
                .ThenInclude(c => c.R_CampaignHasCharacters)
            .Include(e => e.R_Board)
            .Include(e => e.R_Participances)
                .ThenInclude(p => p.R_Character)
            .Include(e => e.R_Participances)
                .ThenInclude(p => p.R_OccupiedFields)
            .AsSplitQuery()
            .AsQueryable();

        // Sorting
        query = encounterParams.OrderBy switch
        {
            "name" => query.OrderBy(c => c.Name),
            "nameDesc" => query.OrderByDescending(c => c.Name),
            _ => query.OrderBy(c => c.Name)
        };
            
        return await PagedList<Models.Entities.Campaign.Encounter>.CreateAsync(
            query, 
            encounterParams.PageNumber, 
            encounterParams.PageSize
            );
    }

    public Task<Models.Entities.Campaign.Encounter> GetEncounterSummary(int encounterId)
    {
        var encounter = _context.Encounters
            .Where(e => e.Id == encounterId)
            .Include(e => e.R_Campaign)
                .ThenInclude(c => c.R_CampaignHasCharacters)
            .Include(e => e.R_Board)
                .ThenInclude(b => b.R_ConsistsOfFields)
            .Include(e => e.R_Participances)
                .ThenInclude(p => p.R_Character)
            .Include(e => e.R_Participances)
                .ThenInclude(p => p.R_OccupiedFields)
            .AsSplitQuery()
            .FirstAsync();
        
        return encounter;
    }
}