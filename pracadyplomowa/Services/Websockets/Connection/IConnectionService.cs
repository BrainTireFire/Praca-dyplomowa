namespace pracadyplomowa.Services.Websockets.Connection;

public interface IConnectionService
{
    void AddUserConnection(int userId, string connectionId);
    void RemoveUserConnection(int userId, string connectionId);
    bool IsUserInGroup(int userId, string groupName);
    string? GetConnectionIdByUserId(int userId);
}