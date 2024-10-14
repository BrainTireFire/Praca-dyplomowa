using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository
{
    public interface ICampaignRepository : IBaseRepository<Campaign>
    {
        public Task<List<CampaignDto>> GetCampaigns(int OwnerId);
        public Task<Campaign> GetCampaign(int campaignId);
    }
}