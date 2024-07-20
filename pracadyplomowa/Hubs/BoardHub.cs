using Microsoft.AspNetCore.SignalR;

namespace pracadyplomowa.Hubs;

public class BoardHub : Hub
{
    public async Task UpdateSelectedBox(Coordinate box)
    {
        // Notify all clients about the selected box update
        await Clients.All.SendAsync("ReceiveSelectedBox", box);
    }
    
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}