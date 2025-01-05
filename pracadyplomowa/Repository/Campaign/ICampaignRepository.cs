using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository
{
    public interface ICampaignRepository : IBaseRepository<Campaign>
    {
        public Task<List<Campaign>> GetCampaigns(int OwnerId);
        public Task<Campaign> GetCampaign(int campaignId);
        public Task RemoveCampaign(int campaignId);
    }
}