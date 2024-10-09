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
            .Select(c => new CampaignDto(c.Id, c.Name, c.Description, c.InvitationLink)).ToListAsync();
            return campaigns;
        }
        public Task<Campaign> GetCampaign(int campaignId)
        {
            var campaign = _context.Campaigns
            .Where(c => c.Id == campaignId)
            .FirstAsync();

            return campaign;
        }
    }
}