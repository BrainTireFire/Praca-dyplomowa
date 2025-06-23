namespace pracadyplomowa.Services.Websockets;

public interface ISessionService
{
    Task RequeryInitiative(int encounterId, int exceptUserId);
    Task UpdateParticipanceData(int encounterId);
}