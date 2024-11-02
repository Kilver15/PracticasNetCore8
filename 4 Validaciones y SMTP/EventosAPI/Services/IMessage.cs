namespace EventosAPI.Services
{
    public interface IMessage
    {
        void SendEmail(string subject, string body, string to);
    }
}
