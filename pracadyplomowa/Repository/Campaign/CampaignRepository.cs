using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository
{
    public class CampaignRepository : BaseRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<List<Campaign>> GetCampaigns(int OwnerId)
        {
            List<Campaign> campaigns = await _context.Campaigns.Where(c => c.R_OwnerId == OwnerId).ToListAsync();

            return campaigns;
        }
        public async Task<Campaign> GetCampaign(int campaignId)
        {
            var campaign = await _context.Campaigns
            .Where(c => c.Id == campaignId)
            .Include(c => c.R_CampaignHasCharacters)
            .ThenInclude(ch => ch.R_CharacterHasLevelsInClass).ThenInclude(cl => cl.R_Class)
            .Include(c => c.R_CampaignHasCharacters)
            .ThenInclude(ch => ch.R_CharacterBelongsToRace)
            .FirstOrDefaultAsync();

            return campaign;
        }
    }
}