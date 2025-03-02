using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.Errors;
using pracadyplomowa.Hubs;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services;
using pracadyplomowa.Services.Websockets.Notification;

namespace pracadyplomowa.Controllers
{

    public class CampaignController(IUnitOfWork unitOfWork, ICharacterService characterService, 
        INotificationService notificationService, IAccountRepository accountRepository) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly ICharacterService _characterService = characterService;
        private readonly INotificationService _notificationService = notificationService;

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
        
        [HttpGet("attendCampaigns")]
        public async Task<ActionResult<List<CampaignDto>>> GetAttendCampaigns()
        {
            var userId = User.GetUserId();
            List<Campaign> campaigns = await _unitOfWork.CampaignRepository.GetAttendCampaigns(userId);

            List<CampaignDto> campaignsDto = campaigns.Select(c => new CampaignDto(c)).ToList();

            return Ok(campaignsDto);
        }

        [HttpGet("{campaignId}")]
        public async Task<ActionResult<CampaignDto>> GetCampaign(int campaignId)
        {
            var userId = User.GetUserId();
            var campaign = await _unitOfWork.CampaignRepository.GetCampaign(userId, campaignId);

            if (campaign == null)
            {
                return BadRequest(new ApiResponse(400, "Campaign with given id - does not exist"));
            }

            var campaignDto = new CampaignDto(campaign);

            return Ok(campaignDto);
        }
        
        [HttpGet("joinInfo/{campaignId}")]
        public async Task<ActionResult<CampaignJoinDto>> GetCampaignJoin(int campaignId)
        {
            var campaign = await _unitOfWork.CampaignRepository.GetCampaignJoin(campaignId);

            if (campaign == null)
            {
                return BadRequest(new ApiResponse(400, "Campaign with given id - does not exist"));
            }

            var campaignDto = new CampaignJoinDto(campaign);

            return Ok(campaignDto);
        }

        [HttpPost("addCharacterToCampaign/{campaignId}/{characterId}")]
        public async Task<ActionResult> AddCharacterToCampaign(int campaignId, int characterId)
        {
            var userId = User.GetUserId();
            var user = await _accountRepository.GetUserById(userId);
            var campaign = _unitOfWork.CampaignRepository.GetById(campaignId);
            var character = _unitOfWork.CharacterRepository.GetById(characterId);
            
            if (campaign == null || character == null || user == null)
            {
                return BadRequest(new ApiResponse(400, "Campaign or Character with given id - does not exist"));
            }
            
            campaign.R_CampaignHasCharacters.Add(character);
            campaign.R_UsersAttendsCampaigns.Add(user);
            user.R_UserAttendsAsPlayerToCamgains.Add(campaign);

            await _unitOfWork.SaveChangesAsync();
            
            await _notificationService.SendNotificationAndAddToGroup(userId, campaignId, character.Name, campaign.Name);

            return Ok(campaignId);
        }

        [HttpDelete("removeCharacterFromCampaign/{characterId}")]
        public async Task<ActionResult> removeCharacterFromCampaign(int characterId)
        {
            var character = _unitOfWork.CharacterRepository.GetById(characterId);
            if (character == null)
                return BadRequest(new ApiResponse(400, "A Character with given id - does not exist"));

            var campaign = await _unitOfWork.CampaignRepository.GetCampaignWithUsersAttends(character.R_CampaignId.GetValueOrDefault());

            if (campaign == null)
                return BadRequest(new ApiResponse(400, "This Character doesn't belong to any Campaign"));

            var user = await _accountRepository.GetUserWithAttendCampaignById((int)character.R_OwnerId);
            campaign.R_UsersAttendsCampaigns.Remove(user);
            campaign.R_CampaignHasCharacters.Remove(character);
            user.R_UserAttendsAsPlayerToCamgains.Remove(campaign);

            _unitOfWork.ParticipanceDataRepository.RemoveByCharacterId(character.Id);
            
            await _unitOfWork.SaveChangesAsync();
            
            var userId = User.GetUserId();
            await _notificationService.SendNotificationAndRemoveFromGroup(userId, campaign.Id, character.Name, campaign.Name);
            
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
            var userId = User.GetUserId();
            var campaign = await _unitOfWork.CampaignRepository.GetCampaign(userId, campaignId);
            if (campaign == null)
            {
                return BadRequest(new ApiResponse(400, "Campaign with given id - does not exist"));
            }
            
            var character = campaign.R_CampaignHasCharacters.FirstOrDefault(character => character.R_OwnerId == userId);
            if(character == null){
                return BadRequest(new ApiResponse(400, "You do not have a player character in this campaign"));
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

        [HttpPatch("{campaignId}/longRest")]
        public async Task<ActionResult> PerformLongRest(int campaignId){
            var campaign = await _unitOfWork.CampaignRepository.GetCampaignWithCharacters(campaignId);
            if(campaign == null){
                return NotFound();
            }
            if(campaign.R_OwnerId != User.GetUserId()){
                return BadRequest("You are not the Dungeon Master");
            }
            foreach(var character in campaign.R_CampaignHasCharacters){
                await _unitOfWork.CharacterRepository.GetByIdWithAll(character.Id);
                character.PerformLongRest();
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}