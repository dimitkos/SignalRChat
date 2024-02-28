using Microsoft.AspNetCore.SignalR;
using SignalRChat.Data;
using System.Security.Claims;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("MessageReceived", user, message);
        }

        public override Task OnConnectedAsync()
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!String.IsNullOrEmpty(UserId))
            {
                var userName = _context.Users.FirstOrDefault(u => u.Id == UserId)?.UserName ?? string.Empty;

                Clients.Users(HubConnections.OnlineUsers()).SendAsync("ReceiveUserConnected", UserId, userName);
                HubConnections.AddUserConnection(UserId, Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

        //[Authorize]
        //public async Task SendMessageToReceiver(string sender, string receiver, string message)
        //{
        //    var userId = _context.Users.First(x=> x.Email.ToLower() == receiver.ToLower()).Id;

        //    await Clients.User(userId).SendAsync("MessageReceived", sender, message);
        //}
    }
}
