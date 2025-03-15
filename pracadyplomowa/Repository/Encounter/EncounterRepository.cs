using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Repository.Encounter;

public class EncounterRepository : BaseRepository<Models.Entities.Campaign.Encounter>, IEncounterRepository
{
    public EncounterRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Models.Entities.Campaign.Encounter>> GetEncounters(int ownerId, int campaignId,  EncounterParams encounterParams)
    {
        var query = _context.Encounters
            .Where(e => e.R_OwnerId == ownerId)
            .Where(e => e.R_Campaign.Id == campaignId)
            .Include(e => e.R_Board).ThenInclude(e => e.R_ConsistsOfFields).ThenInclude(e => e.R_CasterPowers)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharacterBelongsToRace)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharacterHasLevelsInClass).ThenInclude(c => c.R_Class)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_AffectedBy.Where(x => x is SizeEffectInstance))
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_CharacterBelongsToRace)
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_CharacterHasLevelsInClass).ThenInclude(c => c.R_Class)
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_AffectedBy.Where(x => x is SizeEffectInstance))
            .Include(e => e.R_Participances).ThenInclude(p => p.R_OccupiedField)
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
            .Include(e => e.R_Board).ThenInclude(e => e.R_ConsistsOfFields).ThenInclude(e => e.R_CasterPowers)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_Owner)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharacterBelongsToRace)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharacterHasLevelsInClass).ThenInclude(c => c.R_Class)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_AffectedBy.Where(x => x is SizeEffectInstance))
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_Owner)
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_CharacterBelongsToRace)
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_CharacterHasLevelsInClass).ThenInclude(c => c.R_Class)
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_AffectedBy.Where(x => x is SizeEffectInstance))
            .Include(e => e.R_Participances).ThenInclude(p => p.R_OccupiedField)
            .AsSplitQuery()
            .FirstAsync();
        
        return encounter;
    }

    public Task<Models.Entities.Campaign.Encounter> GetEncounterForPositionUpdate(int encounterId)
    {
        var encounter = _context.Encounters
            .Where(e => e.Id == encounterId)
            .Include(e => e.R_Board).ThenInclude(e => e.R_ConsistsOfFields).ThenInclude(e => e.R_CasterPowers)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_Owner)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharacterBelongsToRace)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_AffectedBy.Where(x => x is SizeEffectInstance))
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharactersParticipatesInEncounters.Where(x => x.R_EncounterId == encounterId))
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_Owner)
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_CharacterBelongsToRace)
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_AffectedBy.Where(x => x is SizeEffectInstance))
            .Include(e => e.R_Participances).ThenInclude(p => p.R_Character).ThenInclude(c => c.R_CharactersParticipatesInEncounters.Where(x => x.R_EncounterId == encounterId))
            .Include(e => e.R_Participances).ThenInclude(p => p.R_OccupiedField)
            .AsSplitQuery()
            .FirstAsync();
        
        return encounter;
    }
    public Task<Models.Entities.Campaign.Encounter> GetEncounterSummaryForDelete(int encounterId)
    {
        var encounter = _context.Encounters
            .Where(e => e.Id == encounterId)
            .Include(e => e.R_Owner)
            .Include(e => e.R_Board)
            .Include(e => e.R_Campaign).ThenInclude(c => c.R_CampaignHasEncounters.Where(e => e.Id == encounterId))
            .Include(e => e.R_Participances).ThenInclude(p => p.R_OccupiedField)
            .AsSplitQuery()
            .FirstAsync();
        
        return encounter;
    }
    public Task<Models.Entities.Campaign.Encounter> GetEncounterSummaryWithFieldPowers(int encounterId)
    {
        var encounter = _context.Encounters
            .Where(e => e.Id == encounterId)
            .Include(e => e.R_Campaign)
                .ThenInclude(c => c.R_CampaignHasCharacters)
            .Include(e => e.R_Board)
                .ThenInclude(b => b.R_ConsistsOfFields)
                    .ThenInclude(b => b.R_CasterPowers)
                        .ThenInclude(p => p.R_EffectBlueprints)
            .Include(e => e.R_Participances)
                .ThenInclude(p => p.R_Character)
                    .ThenInclude(c => c.R_Owner)
            .Include(e => e.R_Participances)
                .ThenInclude(p => p.R_OccupiedField)
            .AsSplitQuery()
            .FirstAsync();
        
        return encounter;
    }
    public Task<Models.Entities.Campaign.Encounter> GetEncounterWithParticipances(int encounterId)
    {
        var encounter = _context.Encounters
            .Where(e => e.Id == encounterId)
            .Include(e => e.R_Participances)
                .ThenInclude(p => p.R_Character)
            .FirstAsync();
        
        return encounter;
    }
    public Task<Models.Entities.Campaign.Encounter> GetEncounterWithParticipancesAndCampaign(int encounterId)
    {
        var encounter = _context.Encounters
            .Where(e => e.Id == encounterId)
            .Include(e => e.R_Participances)
                .ThenInclude(p => p.R_Character)
            .Include(e => e.R_Campaign)
            .FirstAsync();
        
        return encounter;
    }

    public Task<Models.Entities.Campaign.Encounter> GetEncounterWithParticipance(int encounterId, int characterId){
        var encounter = _context.Encounters
            .Where(e => e.Id == encounterId)
            .Include(e => e.R_Participances.Where(p => p.R_CharacterId == characterId))
                .ThenInclude(p => p.R_Character)
            .FirstAsync();
        
        return encounter;
    }
    public Task<Models.Entities.Campaign.Encounter> GetEncounterWithPlayerDetails(int encounterId)
    {
        var encounter = _context.Encounters
            .Where(e => e.Id == encounterId)
            .Include(e => e.R_Participances)
                .ThenInclude(p => p.R_Character)
                    .ThenInclude(c => c.R_Owner)
            .FirstAsync();
        
        return encounter;
    }
}