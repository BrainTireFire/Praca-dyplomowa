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
            List<Campaign> campaigns = await _context.Campaigns
                .Where(c => c.R_OwnerId == OwnerId)
                .Include(c => c.R_Owner)
                .ToListAsync();

            return campaigns;
        }
        public async Task<List<Campaign>> GetCampaignsForMenu(int OwnerId)
        {
            List<Campaign> campaigns = await _context.Campaigns
                .Where(c => c.R_OwnerId == OwnerId)
                .Include(c => c.R_Owner)
                .Include(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharacterHasLevelsInClass).ThenInclude(c => c.R_Class)
                .Include(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharacterBelongsToRace)
                .ToListAsync();

            return campaigns;
        }
        
        public async Task<Campaign> GetCampaign(int userId, int campaignId)
        {
            var campaign = await _context.Campaigns
            .Where(c => c.Id == campaignId)
            .Where(c => c.R_OwnerId == userId || c.R_UsersAttendsCampaigns.Any(u => u.Id == userId))
            .Include(c => c.R_Owner)
            .Include(c => c.R_CampaignHasCharacters)
            .ThenInclude(ch => ch.R_CharacterHasLevelsInClass).ThenInclude(cl => cl.R_Class)
            .Include(c => c.R_CampaignHasCharacters)
            .ThenInclude(ch => ch.R_CharacterBelongsToRace)
            .FirstOrDefaultAsync();

            return campaign;
        }
        
        public async Task<Campaign> GetCampaignWithUsersAttends(int campaignId)
        {
            return await _context.Campaigns
                .Where(c => c.Id == campaignId)
                .Include(c => c.R_UsersAttendsCampaigns)
                .Include(c => c.R_CampaignHasCharacters)
                .FirstOrDefaultAsync();
        }

        public async Task<Campaign?> GetCampaignJoin(int campaignId)
        {
            return await _context.Campaigns.Where(c => c.Id == campaignId).FirstOrDefaultAsync();
        }

        public async Task<List<Campaign>> GetAttendCampaigns(int userId)
        {
            return await _context.Campaigns
                .Where(c => c.R_UsersAttendsCampaigns.Any(u => u.Id == userId))
                .Include(c => c.R_Owner)
                .ToListAsync();
        }

        public async Task<List<Campaign>> GetAttendedCampaignsForMenu(int userId)
        {
            return await _context.Campaigns
                .Where(c => c.R_UsersAttendsCampaigns.Any(u => u.Id == userId))
                .Include(c => c.R_Owner)
                .Include(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharacterHasLevelsInClass).ThenInclude(c => c.R_Class)
                .Include(c => c.R_CampaignHasCharacters).ThenInclude(c => c.R_CharacterBelongsToRace)
                .ToListAsync();
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
            var campaign = _context.Campaigns
                .Include(c => c.R_CampaignHasCharacters)
                .Include(c => c.R_CampaignHasEncounters)
                .FirstOrDefault(c => c.Id == campaignId);


            if (campaign == null)
            {
                throw new ArgumentNullException("Campaign " + nameof(campaign) + $" id:{campaignId} is null.");
            }
            
            foreach (var encounter in campaign.R_CampaignHasEncounters.ToList())
            {
                _context.Encounters.Remove(encounter);
            }

            var characters = campaign.R_CampaignHasCharacters.ToList();

            foreach (var character in characters)
            {
                character.R_Campaign = null;
                character.R_CampaignId = null;
            }
            
            campaign.R_CampaignHasCharacters.Clear();
            campaign.R_CampaignHasEncounters.Clear();

            _context.Campaigns.Remove(campaign);

            await _context.SaveChangesAsync();
        }
    }
}