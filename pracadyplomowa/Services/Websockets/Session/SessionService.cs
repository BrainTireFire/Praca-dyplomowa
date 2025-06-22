using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.Hubs;
using pracadyplomowa.Services.Websockets.Connection;

namespace pracadyplomowa.Services.Websockets;

public class SessionService : ISessionService
{
    private readonly IHubContext<SessionHub> _hubContext;
    private readonly IConnectionService _connectionService;

    public SessionService(IHubContext<SessionHub> hubContext, IConnectionService connectionService)
    {
        _hubContext = hubContext;
        _connectionService = connectionService;
    }

    public async Task RequeryInitiative(int encounterId, int exceptUserId){
        var connectionId = _connectionService.GetConnectionIdByUserId(exceptUserId);
        if (connectionId != null)
        {
            await _hubContext.Clients.GroupExcept(encounterId.ToString(), connectionId).SendAsync("RequeryInitiative");
        }
        else{
            await _hubContext.Clients.Group(encounterId.ToString()).SendAsync("RequeryInitiative");
        }
    }
    
    public async Task UpdateParticipanceData(int encounterId)
    {
        await _hubContext.Clients
            .Group($"{encounterId}")
            .SendAsync("RequeryParticipanceData", new {
                EncounterId = encounterId
            });
    }
}