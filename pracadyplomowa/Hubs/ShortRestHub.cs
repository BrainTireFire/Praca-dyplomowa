using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.Websockets.Connection;

namespace pracadyplomowa.Hubs
{
[Authorize]
    public class ShortRestHub(IConnectionService connectionService, IUnitOfWork unitOfWork) : Hub
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;   
        private readonly IConnectionService _connectionService = connectionService;

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.GetUserId();
            var campaignId = GetCampaignId();
            if (userId.HasValue)
            {

                await Groups.AddToGroupAsync(Context.ConnectionId, $"ShortRest_{campaignId}");
                var campaign = _unitOfWork.CampaignRepository.GetById(int.Parse(campaignId!));
                if(campaign != null && campaign.R_OwnerId == userId){
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"ShortRest_{campaignId}_GM");
                }
                await Groups.AddToGroupAsync(Context.ConnectionId, "Global");
                _connectionService.AddUserConnection(userId.Value, Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.GetUserId();
            var campaignId = GetCampaignId();
            if (userId.HasValue)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"ShortRest_{campaignId}");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"ShortRest_{campaignId}_GM");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Global");
                _connectionService.RemoveUserConnection(userId.Value, Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }
        
        private string? GetCampaignId()
        {
            return Context.GetHttpContext()?.Request.Query["campaignId"].ToString();
        }

        public async Task ShortRestDiceSelected(int campaignId, DiceSetDto hitDiceLocal)
        {
            var campaign = await _unitOfWork.CampaignRepository.GetCampaignWithCharacters(campaignId);
            var character = campaign?.R_CampaignHasCharacters
                .FirstOrDefault(x => x.R_OwnerId == Context.User?.GetUserId());

            if (character == null)
            {
                throw new HubException("Character not found for current user in this campaign.");
            }

            var campaignGroup = $"ShortRest_{campaignId}_GM";

            await Clients.Group(campaignGroup)
                .SendAsync("ShortRestDiceSelectedToGm", character.Id, hitDiceLocal);
        }
    }
}