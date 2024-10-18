using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Repository;

namespace pracadyplomowa.Controllers
{

    public class CampaignController(ICampaignRepository campaignRepository) : BaseApiController
    {
        private readonly ICampaignRepository _campaignRepository = campaignRepository;

        [HttpPost]
        public async Task<ActionResult> CreateCampaign(CampaignInsertDto campaignInsertDto)
        {
            var campaign = new Campaign
            {
                Name = campaignInsertDto.Name,
                Description = campaignInsertDto.Description
            };
            var userId = User.GetUserId();
            campaign.R_OwnerId = userId;

            _campaignRepository.Add(campaign);
            await _campaignRepository.SaveChanges();
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<List<CampaignDto>>> GetCampaigns()
        {
            var campaigns = await _campaignRepository.GetCampaigns(User.GetUserId());

            return Ok(campaigns);
        }

        [HttpGet("{campaignId}")]
        public async Task<ActionResult<CampaignDto>> GetCampaign(int campaignId)
        {
            var campaign = await _campaignRepository.GetCampaign(campaignId);

            var campaignDto = new CampaignDto(campaign.Id, campaign.Name, campaign.Description);

            return Ok(campaignDto);
        }

        [HttpPost("addCharacterToCampaign/{campaignId}/{characterId}")]
        public ActionResult AddCharacterToCampaign(int campaignId, int characterId)
        {
            _campaignRepository.AddCharacter(campaignId, characterId);
            return Ok();
        }
    }
}