using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;

namespace pracadyplomowa.Hubs
{
    [Authorize]
    public class BoardHub2 : Hub
    {
        private static readonly ConcurrentDictionary<string, List<string>> GroupUsers = new();
        private static readonly ConcurrentDictionary<string, string> GroupOwners = new();
        
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
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

        public async Task UpdateSelectedBox(string groupName, Coordinate box)
        {
            await Clients.Group(groupName).SendAsync("ReceiveSelectedBox", box);
        }

        public async Task StartBoard(string groupName)
        {
            var userId = Context.User.GetUserId();
            if (GroupOwners.TryGetValue(groupName, out var ownerId) && ownerId == userId.ToString())
            {
                await Clients.Group(groupName).SendAsync("BoardStarted");
            }
            else
            {
                await Clients.Caller.SendAsync("Error", "Only the campaign owner can start the board.");
            }
        }

        public async Task AddToGroup(string groupName)
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

        public async Task SendUsersInGroup(string groupName)
        {
            if (GroupUsers.TryGetValue(groupName, out var users))
            {
                await Clients.Group(groupName).SendAsync("ReceiveUsersInGroup", users);
            }
        }

        public async Task AssignOwner(string groupName, string userId)
        {
            if (Context.User.GetUserId().ToString() == userId)
            {
                GroupOwners[groupName] = userId;
            }
            else
            {
                await Clients.Caller.SendAsync("Error", "You are not authorized to assign the owner.");
            }
        }

        public class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
