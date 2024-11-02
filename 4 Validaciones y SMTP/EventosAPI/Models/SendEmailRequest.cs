namespace EventosAPI.Models
{
    public record SendEmailRequest(
        string Subject,
        string Body,
        string To
        );
        
}
