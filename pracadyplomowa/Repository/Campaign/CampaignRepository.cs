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

        public async Task<List<CampaignInsertDto>> GetCampaigns(int OwnerId)
        {
            List<CampaignInsertDto> campaigns = await _context.Campaigns
            .Where(c => c.R_OwnerId == OwnerId)
            .Select(c => new CampaignInsertDto(c.Name, c.Description, c.InvitationLink)).ToListAsync();
            return campaigns;
        }
    }
}