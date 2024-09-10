using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository
{
    public interface ICampaignRepository : IBaseRepository<Campaign>
    {
        public Task<List<CampaignInsertDto>> GetCampaigns(int OwnerId);
    }
}