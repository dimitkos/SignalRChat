using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Data;

namespace SignalRChat.Hubs
{
    public class BasicChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public BasicChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("MessageReceived", user, message);
        }

        [Authorize]
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            var userId = _context.Users.First(x=> x.Email.ToLower() == receiver.ToLower()).Id;

            await Clients.User(userId).SendAsync("MessageReceived", sender, message);
        }
    }
}
