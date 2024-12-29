using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Controllers
{

    public class CampaignController(IUnitOfWork unitOfWork) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        
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

            _unitOfWork.CampaignRepository.Add(campaign);
            await _unitOfWork.SaveChangesAsync();
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<List<CampaignDto>>> GetCampaigns()
        {
            List<Campaign> campaigns = await _unitOfWork.CampaignRepository.GetCampaigns(User.GetUserId());

            List<CampaignDto> campaignsDto = campaigns.Select(c => new CampaignDto(c)).ToList();

            return Ok(campaignsDto);
        }

        [HttpGet("{campaignId}")]
        public async Task<ActionResult<CampaignDto>> GetCampaign(int campaignId)
        {
            var campaign = await _unitOfWork.CampaignRepository.GetCampaign(campaignId);

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
            var campaign = _unitOfWork.CampaignRepository.GetById(campaignId);
            var character = _unitOfWork.CharacterRepository.GetById(characterId);


            if (campaign == null || character == null)
            {
                return BadRequest(new ApiResponse(400, "campaign or character with given id - does not exist"));
            }

            campaign.R_CampaignHasCharacters.Add(character);

            _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}