using EventosAPI.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IMessage _message;

    public EmailController(IMessage message)
    {
        _message = message;
    }

    [HttpPost("send")]
    public IActionResult SendEmail([FromBody] EmailRequest emailRequest)
    {
        if (string.IsNullOrEmpty(emailRequest.To) || string.IsNullOrEmpty(emailRequest.Subject) || string.IsNullOrEmpty(emailRequest.Body))
        {
            return BadRequest("Todos los campos son requeridos.");
        }

        // Llama al método SendEmail
        _message.SendEmail(emailRequest.Subject, emailRequest.Body, emailRequest.To);
        return Ok("Correo enviado exitosamente.");
    }
}

public class EmailRequest
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
