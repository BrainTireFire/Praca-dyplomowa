using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.Hubs;
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
        await _hubContext.Clients.Group($"Campaign_{campaignId}").SendAsync("ReceiveNotification", notificationMessage, campaignId);
        //await _hubContext.Clients.Group($"Global").SendAsync("ReceiveNotification", notificationMessage, campaignId);
    }

}