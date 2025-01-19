using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.DTOs.Session;

namespace pracadyplomowa.Hubs;

[Authorize]
public class SessionHub  : Hub
{
    //TODO the same users is connecting!
    //
    private static readonly ConcurrentDictionary<string, List<string>> UsersConnected = new();
    private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, Coordinate>> UserSelections = new();
    private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, Coordinate>> UserCursors = new();
    private static readonly ConcurrentDictionary<string, List<int>> SelectedPath = new();
    
    public override async Task OnConnectedAsync()
    {
        var groupName = GetGroupName();

        if (!string.IsNullOrEmpty(groupName))
        {
            var userName = Context.User?.GetUsername();
            
            if (userName != null)
            {
                // Check if the user is authorized for this campaign (this could be an async operation)
                if (true)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                    AddUserToGroup(groupName, userName);
                    
                    await Clients.Group(groupName).SendAsync("UserJoined", userName);
                    
                    // Initialize user cursors
                    if (!UserCursors.ContainsKey(groupName))
                    {
                        UserCursors[groupName] = new ConcurrentDictionary<string, Coordinate>();
                    }
                    UserCursors[groupName][Context.ConnectionId] = new Coordinate { X = 0, Y = 0 };
                    await NotifyAllCursorsInGroup(groupName);
                    
                    if (!UserSelections.ContainsKey(groupName))
                    {
                        UserSelections[groupName] = new ConcurrentDictionary<string, Coordinate>();
                    }
                    UserSelections[groupName][Context.ConnectionId] = new Coordinate { X = 0, Y = 0 };
                    await NotifyAllSelectionsInGroup(groupName);
                }
                else
                {
                    Context.Abort();
                }
            }
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userName = Context.User?.GetUsername();
        var groupName = GetGroupName();

        if (userName != null && groupName != null)
        {
            RemoveUserFromGroup(groupName, userName);
            await Clients.Group(groupName).SendAsync("UserLeft", userName);

            if (UserCursors.ContainsKey(groupName))
            {
                UserCursors[groupName].TryRemove(Context.ConnectionId, out _);
                if (UserCursors[groupName].Count == 0)
                {
                    UserCursors.TryRemove(groupName, out _);
                }
                await NotifyAllCursorsInGroup(groupName);
            }
            
            if (UserSelections.ContainsKey(groupName))
            {
                UserSelections[groupName].TryRemove(Context.ConnectionId, out _);
                if (UserSelections[groupName].Count == 0)
                {
                    UserSelections.TryRemove(groupName, out _);
                }
                await NotifyAllSelectionsInGroup(groupName);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    
    /*
     * Group management
     */
    public List<string> GetUsersInGroup(string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
        {
            throw new ArgumentException("Group name cannot be null or empty.", nameof(groupName));
        }

        if (UsersConnected.TryGetValue(groupName, out var users))
        {
            return users;
        }

        return new List<string>();
    }
    
    private void AddUserToGroup(string groupName, string userName)
    {
        UsersConnected.AddOrUpdate(groupName,
            _ => new List<string> { userName },
            (_, list) => { list.Add(userName); return list; });
    }

    private void RemoveUserFromGroup(string groupName, string userId)
    {
        if (UsersConnected.TryGetValue(groupName, out var users))
        {
            users.Remove(userId);
            if (users.Count == 0)
            {
                UsersConnected.TryRemove(groupName, out _);
            }
        }
    }
    
    private string? GetGroupName()
    {
        return Context.GetHttpContext()?.Request.Query["groupName"].ToString();
    }
    
    /*
     * Board management=
     */
    public async Task SendSelectedBoxes(string groupName, Dictionary<string, Coordinate> boxes)
    {
        if (!UserSelections.ContainsKey(groupName))
        {
            UserSelections[groupName] = new ConcurrentDictionary<string, Coordinate>();
        }
        foreach (var box in boxes)
        {
            UserSelections[groupName][box.Key] = box.Value;
        }
        await NotifyAllSelectionsInGroup(groupName);
    }
    
    private async Task NotifyAllSelectionsInGroup(string groupName)
    {
        if (UserSelections.TryGetValue(groupName, out var selections))
        {
            await Clients.Group(groupName).SendAsync("ReceiveSelectedBoxes", selections);
        }
    }
    
    public async Task SendCursorPosition(Coordinate position)
    {
        var groupName = GetGroupName();
        if (groupName != null)
        {
            if (!UserCursors.ContainsKey(groupName))
            {
                UserCursors[groupName] = new ConcurrentDictionary<string, Coordinate>();
            }

            UserCursors[groupName][Context.ConnectionId] = position;
            await NotifyAllCursorsInGroup(groupName);
        }
    }

    private async Task NotifyAllCursorsInGroup(string groupName)
    {
        if (UserCursors.TryGetValue(groupName, out var cursors))
        {
            await Clients.Group(groupName).SendAsync("UpdateCursors", cursors);
        }
    }
    
    /*
     * Message management
     */
    public async Task SendMessageToGroup(string groupName, string message)
    {
        var username = Context.User?.GetUsername();
            
        var messageDto = new MessageDto()
        {
            Username = username,
            Message = message
        };
            
        await Clients.Group(groupName).SendAsync("ReceiveMessage", messageDto);
    }
    
    public async Task SendSelectedPath(List<int> fieldIds)
    {
        var groupName = GetGroupName();
        if (groupName != null)
        {
            SelectedPath[groupName] = fieldIds;
            await NotifyAllInGroupAboutPath(groupName);
        }
    }

    private async Task NotifyAllInGroupAboutPath(string groupName)
    {
        if (SelectedPath.TryGetValue(groupName, out var paths))
        {
            await Clients.GroupExcept(groupName, Context.ConnectionId).SendAsync("UpdatePath", paths);
        }
    }

    public async Task SendRequeryInitiative(){
        
        var groupName = GetGroupName();
        if (groupName != null)
        {
            await NotifyAllRequeryInitiative(groupName);
        }
    }
    private async Task NotifyAllRequeryInitiative(string groupName)
    {
        await Clients.GroupExcept(groupName, Context.ConnectionId).SendAsync("RequeryInitiative");
    }
    

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}