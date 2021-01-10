using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace server.Hubs
{
    public class TodoHub : Hub
    {
        public async Task MySuperDuperAction(object data)
        {
            await Clients.All.SendAsync("asdasd");
        }
    }
}