using System.Collections.Concurrent;

namespace pracadyplomowa.Services.Websockets.Connection;

public class ConnectionService : IConnectionService
{
    private static readonly ConcurrentDictionary<int, HashSet<string>> UserConnections = new();
    
    public void AddUserConnection(int userId, string connectionId)
    {
        if (userId > 0 && !string.IsNullOrEmpty(connectionId))
        {
            if (!UserConnections.ContainsKey(userId))
            {
                UserConnections[userId] = new HashSet<string>();
            }
            UserConnections[userId].Add(connectionId);
        }
    }
    
    public void RemoveUserConnection(int userId, string connectionId)
    {
        if (userId > 0 && !string.IsNullOrEmpty(connectionId))
        {
            if (UserConnections.ContainsKey(userId))
            {
                UserConnections[userId].Remove(connectionId);
                if (UserConnections[userId].Count == 0)
                {
                    UserConnections.TryRemove(userId, out _);
                }
            }
        }
    }

    public bool IsUserInGroup(int userId, string groupName)
    {
        return UserConnections.ContainsKey(userId) && UserConnections[userId].Contains(groupName);
    }

    public string? GetConnectionIdByUserId(int userId)
    {
        return UserConnections.ContainsKey(userId) ? UserConnections[userId].FirstOrDefault() : null;
    }
}