using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace clickandgo.hub
{
    public class MessageHub: Hub
    {
          public async Task SendMessage( string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
            
        }
    }
}