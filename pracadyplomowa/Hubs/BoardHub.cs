using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;

namespace pracadyplomowa.Hubs;

[Authorize]
public class BoardHub : Hub
{
    private static readonly ConcurrentDictionary<string, List<string>> GroupUsers = new();

    public async Task UpdateSelectedBox(string groupName, Coordinate box)
    {
        // Notify all clients in the specified group about the selected box update
        await Clients.Group(groupName).SendAsync("ReceiveSelectedBox", box);
    }
    
    public async Task JoinGroup(string groupName)
    {
        string connectionId = Context.ConnectionId;
        await Groups.AddToGroupAsync(connectionId, groupName);

        if (!GroupUsers.ContainsKey(groupName))
        {
            GroupUsers[groupName] = new List<string>();
        }

        GroupUsers[groupName].Add(connectionId);
        await SendUsersInGroup(groupName);
    }
    
    public async Task LeaveGroup(string groupName)
    {
        string connectionId = Context.ConnectionId;
        await Groups.RemoveFromGroupAsync(connectionId, groupName);

        if (GroupUsers.ContainsKey(groupName))
        {
            GroupUsers[groupName].Remove(connectionId);
            if (GroupUsers[groupName].Count == 0)
            {
                GroupUsers.TryRemove(groupName, out _);
            }
        }

        await SendUsersInGroup(groupName);
    }
    
    public Task SendMessageToGroup(string groupName, string message)
    {
        return Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }

    public async Task SendUsersInGroup(string groupName)
    {
        if (GroupUsers.TryGetValue(groupName, out var users))
        {
            await Clients.Group(groupName).SendAsync("ReceiveUsersInGroup", users);
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        foreach (var group in GroupUsers.Keys)
        {
            if (GroupUsers[group].Contains(Context.ConnectionId))
            {
                await LeaveGroup(group);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
