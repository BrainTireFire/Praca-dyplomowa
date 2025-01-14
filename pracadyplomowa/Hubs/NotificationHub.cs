using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    private readonly IUnitOfWork _unitOfWork;   
    
    public NotificationHub(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task SendNotification(string message, int campaignId)
    {
        var campaign = await _unitOfWork.CampaignRepository.GetCampaign(campaignId);

        if (campaign == null)
        {
            throw new ArgumentException($"Campaign with ID {campaignId} not found.");
        }
        
        var userId = Context.User?.GetUserId();
        
        var userInCampaign = campaign.R_UsersAttendsCampaigns
            .Any(user => user.Id== userId);

        if (!userInCampaign)
        {
            throw new UnauthorizedAccessException("User is not part of this campaign.");
        }
        
        foreach (var user in campaign.R_UsersAttendsCampaigns)
        {
            await Clients.User(user.Id.ToString()).SendAsync("ReceiveNotification", message, campaignId);
        }
    }

    public override async Task OnConnectedAsync()
    {
        // Optionally: log or handle user connections
        await base.OnConnectedAsync();
    }
}