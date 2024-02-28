namespace SignalRChat.Models.ViewModel
{
    public class ChatViewModel
    {
        public int MaxRoomAllowed { get; set; }
        public IList<ChatRoom> Rooms { get; set; }

        public string? UserId { get; set; }

        public bool AllowAddRoom => Rooms == null || Rooms.Count < MaxRoomAllowed;

        public ChatViewModel()
        {
            Rooms = new List<ChatRoom>();
        }
    }
}
