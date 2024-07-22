using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace pracadyplomowa.Hubs
{
    public class MessageDto
    {
        public string Username { get; set; }
        public string Message { get; set; }
    }
    
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(string groupName, string message)
        {
            Console.WriteLine("Sending message to group: " + message);
            var username = Context.User.GetUsername();

            var messageDto = new MessageDto
            {
                Username = username,
                Message = message
            };

            await Clients.Group(groupName).SendAsync("ReceiveMessage", messageDto);
        }

        public async Task AddToGroup(string groupName)
        {
            string connectionId = Context.ConnectionId;
            await Groups.AddToGroupAsync(connectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            string connectionId = Context.ConnectionId;
            await Groups.RemoveFromGroupAsync(connectionId, groupName);
        }
        
        public async Task JoinGroup(string groupName)
        {
            string connectionId = Context.ConnectionId;
            await Groups.AddToGroupAsync(connectionId, groupName);
        }
    }
}