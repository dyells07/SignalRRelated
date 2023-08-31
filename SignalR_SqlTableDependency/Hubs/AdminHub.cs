using Microsoft.AspNetCore.SignalR;

namespace SignalR_SqlTableDependency.Hubs
{
    public class AdminHub : Hub
    {
        public async Task SendJobStatus(string type, string message, string status)
        {
            await Clients.All.SendAsync("ReceivedMessage", type, message, status);
        }
    }
}
