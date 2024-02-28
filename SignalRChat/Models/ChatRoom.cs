using System.ComponentModel.DataAnnotations;

namespace SignalRChat.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
