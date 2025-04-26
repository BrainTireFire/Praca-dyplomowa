using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.Hubs;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Services.Websockets.Connection;

namespace pracadyplomowa.Services.Websockets.Notification;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IConnectionService _connectionService;

    public NotificationService(IHubContext<NotificationHub> hubContext, IConnectionService connectionService)
    {
        _hubContext = hubContext;
        _connectionService = connectionService;
    }

    public async Task SendNotificationAndAddToGroup(int userId, int campaignId, string characterName, string campaignName)
    {
        var connectionId = _connectionService.GetConnectionIdByUserId(userId);
        if (connectionId != null)
        {
            if (!_connectionService.IsUserInGroup(userId, $"Campaign_{campaignId}"))
            {
                await _hubContext.Groups.AddToGroupAsync(connectionId, $"Campaign_{campaignId}");
            }
        }
        
        var notificationMessage = $"{characterName} has been successfully added to the campaign \"{campaignName}\".";
        await _hubContext.Clients.Group($"Campaign_{campaignId}").SendAsync("ReceiveNotification", notificationMessage, campaignId);
        //await _hubContext.Clients.Group($"Global").SendAsync("ReceiveNotification", notificationMessage, campaignId);
    }
    
    public async Task SendNotificationAndRemoveFromGroup(int userId, int campaignId, string characterName, string campaignName)
    {
        var connectionId = _connectionService.GetConnectionIdByUserId(userId);
        if (connectionId != null)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, $"Campaign_{campaignId}");
        }
        
        var notificationMessage = $"{characterName} has been successfully removed from the campaign \"{campaignName}\".";
        var isKick = true;
        await _hubContext.Clients.Group($"Campaign_{campaignId}").SendAsync("ReceiveNotification", notificationMessage, campaignId, isKick);
        //await _hubContext.Clients.Group($"Global").SendAsync("ReceiveNotification", notificationMessage, campaignId);
    }

    public async Task SendNotificationSessionStarted(int ownerId, int? campaignId, int encounteId)
    {
        if (campaignId != null)
        {
            var campaignGroup = $"Campaign_{campaignId}";
            var ownerConnectionId = _connectionService.GetConnectionIdByUserId(ownerId);

            if (ownerConnectionId != null)
            {
                await _hubContext.Clients.GroupExcept(campaignGroup, ownerConnectionId)
                    .SendAsync("SessionHasBeenStarted", "Session has been started. Have fun!", campaignId, encounteId);
            }
            else
            {
                await _hubContext.Clients.Group(campaignGroup)
                    .SendAsync("SessionHasBeenStarted", "Session has been started. Have fun!", campaignId, encounteId);
            }
        }
    }

    public async Task SendNotificationAbilityRollRequested(int characterId, string characterName, int campaignId, Ability ability){
        var campaignDMGroup =  $"Campaign_{campaignId}_GM";
        await _hubContext.Clients.Group(campaignDMGroup)
                    .SendAsync("AbilityRollRequest", "Ability roll requested", characterId, characterName, Enum.GetName(typeof(Ability), ability));
    }

    public async Task SendNotificationAbilityRollPerformed(string characterName, int campaignId, Ability ability, int result){
        var campaignGroup = $"Campaign_{campaignId}";
        await _hubContext.Clients.Group(campaignGroup)
                    .SendAsync("AbilityRollPerformed", "Ability roll performed", characterName, Enum.GetName(typeof(Ability), ability), result);
    }
    public async Task SendNotificationSkillRollRequested(int characterId, string characterName, int campaignId, Skill skill){
        var campaignDMGroup =  $"Campaign_{campaignId}_GM";
        await _hubContext.Clients.Group(campaignDMGroup)
                    .SendAsync("SkillRollRequest", "Skill roll requested", characterId, characterName, Enum.GetName(typeof(Skill), skill));
    }

    public async Task SendNotificationSkillRollPerformed(string characterName, int campaignId, Skill skill, int result){
        var campaignGroup = $"Campaign_{campaignId}";
        await _hubContext.Clients.Group(campaignGroup)
                    .SendAsync("SkillRollPerformed", "Skill roll performed", characterName, Enum.GetName(typeof(Skill), skill), result);
    }
    public async Task SendNotificationSavingThrowRollRequested(int characterId, string characterName, int campaignId, Ability ability){
        var campaignDMGroup =  $"Campaign_{campaignId}_GM";
        await _hubContext.Clients.Group(campaignDMGroup)
                    .SendAsync("SavingThrowRollRequest", "Saving Throw roll requested", characterId, characterName, Enum.GetName(typeof(Ability), ability));
    }

    public async Task SendNotificationSavingThrowRollPerformed(string characterName, int campaignId, Ability ability, int result){
        var campaignGroup = $"Campaign_{campaignId}";
        await _hubContext.Clients.Group(campaignGroup)
                    .SendAsync("SavingThrowRollPerformed", "Saving Throw roll performed", characterName, Enum.GetName(typeof(Ability), ability), result);
    }

}