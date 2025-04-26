using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Services.Websockets.Notification;

public interface INotificationService
{
    Task SendNotificationAndAddToGroup(int userId, int campaignId, string characterName, string campaignName);
    Task SendNotificationAndRemoveFromGroup(int userId, int campaignId, string characterName, string campaignName);
    Task SendNotificationSessionStarted(int ownerId, int? campaignId, int encounteId);
    Task SendNotificationAbilityRollRequested(int characterId, string characterName, int campaignId, Ability ability);
    Task SendNotificationAbilityRollPerformed(string characterName, int campaignId, Ability ability, int result);
    Task SendNotificationSkillRollRequested(int characterId, string characterName, int campaignId, Skill skill);
    Task SendNotificationSkillRollPerformed(string characterName, int campaignId, Skill skill, int result);
    Task SendNotificationSavingThrowRollRequested(int characterId, string characterName, int campaignId, Ability ability);
    Task SendNotificationSavingThrowRollPerformed(string characterName, int campaignId, Ability ability, int result);
}
