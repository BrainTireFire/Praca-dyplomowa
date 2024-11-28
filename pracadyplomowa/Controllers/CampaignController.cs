using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Repository;

namespace pracadyplomowa.Controllers
{

    public class CampaignController(ICampaignRepository campaignRepository, ICharacterRepository characterRepository) : BaseApiController
    {
        private readonly ICampaignRepository _campaignRepository = campaignRepository;
        private readonly ICharacterRepository _characterRepository = characterRepository;
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
            List<Campaign> campaigns = await _campaignRepository.GetCampaigns(User.GetUserId());

            List<CampaignDto> campaignsDto = campaigns.Select(c => new CampaignDto(c)).ToList();

            return Ok(campaignsDto);
        }

        [HttpGet("{campaignId}")]
        public async Task<ActionResult<CampaignDto>> GetCampaign(int campaignId)
        {
            var campaign = await _campaignRepository.GetCampaign(campaignId);

            if (campaign == null)
            {
                return BadRequest(new ApiResponse(400, "Campaign with given id - does not exist"));
            }

            var campaignDto = new CampaignDto(campaign);

            return Ok(campaignDto);
        }

        [HttpPost("addCharacterToCampaign/{campaignId}/{characterId}")]
        public ActionResult AddCharacterToCampaign(int campaignId, int characterId)
        {
            var campaign = _campaignRepository.GetById(campaignId);
            var character = _characterRepository.GetById(characterId);


            if (campaign == null || character == null)
            {
                return BadRequest(new ApiResponse(400, "Campaign or Character with given id - does not exist"));
            }

            campaign.R_CampaignHasCharacters.Add(character);

            _campaignRepository.SaveChanges();
            return Ok();
        }

        [HttpDelete("removeCharacterFromCampaign/{characterId}")]
        public ActionResult removeCharacterFromCampaign(int characterId)
        {
            var character = _characterRepository.GetById(characterId);
            if (character == null)
                return BadRequest(new ApiResponse(400, "A Character with given id - does not exist"));

            var campaign = _campaignRepository.GetById(character.R_CampaignId.GetValueOrDefault());

            if (campaign == null)
                return BadRequest(new ApiResponse(400, "This Character doesn't belong to any Campaign"));

            campaign.R_CampaignHasCharacters.Remove(character);

            _campaignRepository.SaveChanges();
            return Ok();
        }
    }
}