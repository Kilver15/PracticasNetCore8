using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtAPI.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var excludedPaths = new[] { "/api/Auth/Login", "/api/Auth/Register" };
            // Verificar si la ruta es una de las excluidas
            if (excludedPaths.Contains(context.Request.Path.Value))
            {
                await _next(context);
                return;
            }

            // Verificar si el encabezado Authorization está presente
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Encabezado Authorization no presente.");
                return;
            }

            // Obtener el token del encabezado Authorization
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token JWT no proporcionado.");
                return;
            }

            // Si el token es válido, continúa con la siguiente parte del pipeline
            await _next(context);
        }
    }
}
