using System.Net.WebSockets;

namespace WebSocketAPI.Services
{
    public class WebSocketConnectionManager
    {
        private Dictionary<string, WebSocket> _sockets = new Dictionary<string, WebSocket>();

        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public Dictionary<string, WebSocket> GetAllConnections()
        {
            return _sockets;
        }

        public string AddSocket(WebSocket socket)
        {
            var id = Guid.NewGuid().ToString();
            _sockets.Add(id, socket);
            return id;
        }

        public async Task RemoveSocket(string id)
        {
            if (_sockets.ContainsKey(id))
            {
                var socket = _sockets[id];
                _sockets.Remove(id);
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by server", CancellationToken.None);
            }
        }
    }
}
