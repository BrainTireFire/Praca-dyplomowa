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


        public async Task<List<CampaignDto>> GetCampaigns(int OwnerId)
        {
            List<CampaignDto> campaigns = await _context.Campaigns
            .Where(c => c.R_OwnerId == OwnerId)
            .Select(c => new CampaignDto(c.Id, c.Name, c.Description)).ToListAsync();

            return campaigns;
        }
        public Task<Campaign> GetCampaign(int campaignId)
        {
            var campaign = _context.Campaigns
            .Where(c => c.Id == campaignId)
            .FirstAsync();

            return campaign;
        }

        public async void AddCharacter(int campaignId, int characterId)
        {
            var campaign = await _context.Campaigns.Where(c => c.Id == campaignId).FirstAsync();
            var character = await _context.Characters.Where(c => c.Id == characterId).FirstAsync();
            campaign.R_CampaignHasCharacters.Add(character);
            character.R_CampaignId = campaignId;
            character.R_Campaign = campaign;

            await _context.SaveChangesAsync();
        }
    }
}