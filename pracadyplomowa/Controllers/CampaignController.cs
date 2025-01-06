using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services;

namespace pracadyplomowa.Controllers
{

    public class CampaignController(IUnitOfWork unitOfWork, ICharacterService characterService) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICharacterService _characterService = characterService;

        [HttpPost]
        public async Task<ActionResult<int>> CreateCampaign(CampaignInsertDto campaignInsertDto)
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
            return Ok(campaign.Id);
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
        public async Task<ActionResult> AddCharacterToCampaign(int campaignId, int characterId)
        {
            var campaign = _unitOfWork.CampaignRepository.GetById(campaignId);
            var character = _unitOfWork.CharacterRepository.GetById(characterId);


            if (campaign == null || character == null)
            {
                return BadRequest(new ApiResponse(400, "Campaign or Character with given id - does not exist"));
            }

            campaign.R_CampaignHasCharacters.Add(character);

            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("removeCharacterFromCampaign/{characterId}")]
        public async Task<ActionResult> removeCharacterFromCampaign(int characterId)
        {
            var character = _unitOfWork.CharacterRepository.GetById(characterId);
            if (character == null)
                return BadRequest(new ApiResponse(400, "A Character with given id - does not exist"));

            var campaign = _unitOfWork.CampaignRepository.GetById(character.R_CampaignId.GetValueOrDefault());

            if (campaign == null)
                return BadRequest(new ApiResponse(400, "This Character doesn't belong to any Campaign"));

            campaign.R_CampaignHasCharacters.Remove(character);

            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{campaignId}")]
        public async Task<ActionResult> RemoveCampaign(int campaignId)
        {
            await _unitOfWork.CampaignRepository.RemoveCampaign(campaignId);

            return Ok();
        }

        

        [HttpGet("{campaignId}/myCharacter")]
        public async Task<ActionResult> GetCharacter(int campaignId)
        {   
            var campaign = await _unitOfWork.CampaignRepository.GetCampaign(campaignId);
            if (campaign == null)
            {
                return BadRequest(new ApiResponse(400, "Campaign with given id - does not exist"));
            }
            var userId = User.GetUserId();
            var character = campaign.R_CampaignHasCharacters.FirstOrDefault(character => character.R_OwnerId == userId);
            if(character == null){
                return BadRequest(new ApiResponse(400, "Campaign with given id - does not contain your character"));
            }
            if(!_characterService.CheckExistenceAndReadEditAccess(character.Id, userId, [Character.AccessLevels.Read], out var errorResult, out var grantedAccessLevels)){
                return errorResult;
            }
            character = await _unitOfWork.CharacterRepository.GetByIdWithAll(character.Id);

            var characterDto = new CharacterFullDto(character)
            {
                AccessLevels = grantedAccessLevels
            };
            return Ok(characterDto);
        }
    }
}