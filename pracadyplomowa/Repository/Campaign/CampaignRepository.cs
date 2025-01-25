using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;

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
        public async Task<Campaign?> GetCampaignWithCharacters(int campaignId)
        {
            var campaign = await _context.Campaigns
            .Where(c => c.Id == campaignId)
            .Include(c => c.R_CampaignHasCharacters)
            .FirstOrDefaultAsync();

            return campaign;
        }

        public async Task RemoveCampaign(int campaignId)
        {
            var campaign = _context.Campaigns.Include(c => c.R_CampaignHasCharacters).FirstOrDefault(c => c.Id == campaignId);


            if (campaign == null)
            {
                throw new ArgumentNullException("Campaign " + nameof(campaign) + $" id:{campaignId} is null.");
            }

            var characters = campaign.R_CampaignHasCharacters.ToList();

            foreach (var character in characters)
            {
                character.R_Campaign = null;
                character.R_CampaignId = null;
            }

            campaign.R_CampaignHasCharacters = null;

            _context.Campaigns.Remove(campaign);

            await _context.SaveChangesAsync();
        }
    }
}