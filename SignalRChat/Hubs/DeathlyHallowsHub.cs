using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class DeathlyHallowsHub : Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            return StaticDetail.DealthyHallowRace;
        }
    }
}
