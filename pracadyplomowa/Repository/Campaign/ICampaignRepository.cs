using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository
{
    public interface ICampaignRepository : IBaseRepository<Campaign>
    {
        public Task<List<Campaign>> GetCampaigns(int OwnerId);
        public Task<List<Campaign>> GetCampaignsForMenu(int OwnerId);
        public Task<List<Campaign>> GetAttendCampaigns(int userId);
        public Task<List<Campaign>> GetAttendedCampaignsForMenu(int userId);
        public Task<Campaign> GetCampaign(int userId, int campaignId);
        public Task<Campaign> GetCampaignWithUsersAttends(int campaignId);
        public Task<Campaign?> GetCampaignJoin(int campaignId);
        public Task RemoveCampaign(int campaignId);
        public Task<Campaign?> GetCampaignWithCharacters(int campaignId);
    }
}