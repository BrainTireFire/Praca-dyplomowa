using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.Websockets.Connection;

namespace pracadyplomowa.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    private readonly IUnitOfWork _unitOfWork;   
    private readonly IConnectionService _connectionService;
    
    public NotificationHub(IConnectionService connectionService, IUnitOfWork unitOfWork)
    {
        _connectionService = connectionService;
        _unitOfWork = unitOfWork;
    }
    
    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.GetUserId();
        if (userId.HasValue)
        {
            var users = await _unitOfWork.CharacterRepository
                .GetCharactersByUserId(userId.Value);
                
            foreach (var campaign in users.Select(character => character.R_Campaign).OfType<Campaign>())
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Campaign_{campaign.Id}");
            }
            
            await Groups.AddToGroupAsync(Context.ConnectionId, "Global");
            _connectionService.AddUserConnection(userId.Value, Context.ConnectionId);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.GetUserId();
        if (userId.HasValue)
        {
            var users = await _unitOfWork.CharacterRepository
                .GetCharactersByUserId(userId.Value);
                
            foreach (var campaign in users.Select(character => character.R_Campaign).OfType<Campaign>())
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Campaign_{campaign.Id}");
            }
            
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Global");
            _connectionService.RemoveUserConnection(userId.Value, Context.ConnectionId);
        }
        await base.OnDisconnectedAsync(exception);
    }
    
    // public override async Task OnConnectedAsync()
    // {
    //     var userId = Context.User?.GetUserId();
    //     
    //     if (userId != null)
    //     {
    //         await Groups.AddToGroupAsync(Context.ConnectionId, "Global");
    //         
    //         var users = await _unitOfWork.CharacterRepository
    //             .GetCharactersByUserId(userId.Value);
    //         
    //         foreach (var campaign in users.Select(character => character.R_Campaign).OfType<Campaign>())
    //         {
    //             await Groups.AddToGroupAsync(Context.ConnectionId, $"Campaign_{campaign.Id}");
    //         }
    //     }
    //
    //     await base.OnConnectedAsync();
    // }
    //
    // public override async Task OnDisconnectedAsync(Exception? exception)
    // {
    //     var userId = Context.User?.GetUserId();
    //
    //     if (userId != null)
    //     {
    //         await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Global");
    //
    //         var users = await _unitOfWork.CharacterRepository
    //             .GetCharactersByUserId(userId.Value);
    //         
    //         foreach (var campaign in users.Select(character => character.R_Campaign).OfType<Campaign>())
    //         {
    //             await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Campaign_{campaign.Id}");
    //         }
    //     }
    //
    //     await base.OnDisconnectedAsync(exception);
    // }
    
    // public async Task SendNotificationJoinCampaign(string message, int campaignId)
    // {
    //     var userId = Context.User?.GetUserId();
    //     
    //     await ValidateUserCampaignAccess(userId, campaignId);
    //     
    //     await Clients.Group($"Campaign_{campaignId}").SendAsync("ReceiveNotification", message, campaignId);
    // }
    //
    // public async Task SendNotificationKickCharacterFromCampaign(string message, int campaignId)
    // {
    //     var userId = Context.User?.GetUserId();
    //     
    //     await ValidateUserCampaignAccess(userId, campaignId);
    //     
    //     await Clients.Group($"Campaign_{campaignId}").SendAsync("ReceiveNotification", message, campaignId);
    // }
    //
    // private async Task ValidateUserCampaignAccess(int? userId, int campaignId)
    // {
    //     if (userId == null)
    //     {
    //         throw new UnauthorizedAccessException("User is not authenticated.");
    //     }
    //
    //     var campaign = await _unitOfWork.CampaignRepository.GetCampaign(campaignId);
    //     if (campaign == null)
    //         throw new ArgumentException($"Campaign with ID {campaignId} not found.");
    //
    //     var userInCampaign = campaign.R_UsersAttendsCampaigns.Any(user => user.Id == userId);
    //     if (!userInCampaign)
    //     {
    //         throw new UnauthorizedAccessException("User is not part of this campaign.");
    //     }
    // }
}