namespace WebSocketAPI.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string User { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
