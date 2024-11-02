using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using WebSocketAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly WebSocketConnectionManager _connectionManager;

    public ChatController(WebSocketConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    [HttpGet("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var connectionId = _connectionManager.AddSocket(webSocket);

            await ReceiveMessages(webSocket, connectionId);
        }
        else
        {
            HttpContext.Response.StatusCode = 400; // Bad Request
        }
    }

    private async Task ReceiveMessages(WebSocket socket, string connectionId)
    {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            // Recibir el mensaje como un string
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

            // Difundir el mensaje a todos los clientes conectados
            await BroadcastMessageAsync(connectionId, message);

            // Continuar recibiendo mensajes
            result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        // Cerrar la conexión cuando se reciba un mensaje de cierre
        await _connectionManager.RemoveSocket(connectionId);
        await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }

    private async Task BroadcastMessageAsync(string senderId, string message)
    {
        var connections = _connectionManager.GetAllConnections();

        foreach (var connection in connections)
        {
            if (connection.Key != senderId) // No enviar el mensaje al remitente
            {
                var socket = connection.Value;
                if (socket.State == WebSocketState.Open)
                {
                    var encodedMessage = Encoding.UTF8.GetBytes(message);
                    await socket.SendAsync(new ArraySegment<byte>(encodedMessage, 0, encodedMessage.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
