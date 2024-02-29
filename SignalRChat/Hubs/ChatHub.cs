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

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (HubConnections.HasUserConnection(UserId, Context.ConnectionId))
            {
                var UserConnections = HubConnections.Users[UserId];
                UserConnections.Remove(Context.ConnectionId);

                HubConnections.Users.Remove(UserId);

                if (UserConnections.Any())
                    HubConnections.Users.Add(UserId, UserConnections);
            }

            if (!String.IsNullOrEmpty(UserId))
            {
                var userName = _context.Users.FirstOrDefault(u => u.Id == UserId).UserName;
                Clients.Users(HubConnections.OnlineUsers()).SendAsync("ReceiveUserDisconnected", UserId, userName);
                HubConnections.AddUserConnection(UserId, Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendAddRoomMessage(int maxRoom, int roomId, string roomName)
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.FirstOrDefault(u => u.Id == UserId).UserName;

            await Clients.All.SendAsync("ReceiveAddRoomMessage", maxRoom, roomId, roomName, UserId, userName);
        }
    }
}
