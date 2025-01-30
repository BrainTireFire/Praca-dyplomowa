namespace pracadyplomowa.Services.Websockets.Notification;

public interface INotificationService
{
    Task SendNotificationAndAddToGroup(int userId, int campaignId, string characterName, string campaignName);
    Task SendNotificationAndRemoveFromGroup(int userId, int campaignId, string characterName, string campaignName);
    Task SendNotificationSessionStarted(int ownerId, int? campaignId, int encounteId);
}
