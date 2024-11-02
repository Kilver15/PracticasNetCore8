using JwtAPI.Custom;
using JwtAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtDbContext _context;
        private readonly Utilidades _utilidades;
        public AuthController(JwtDbContext jwtDbContext, Utilidades utilidades)
        {
            _context = jwtDbContext;
            _utilidades = utilidades;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(User modelo)
        {
            if (modelo == null)
            {
                return BadRequest("Invalid request");
            }

            var user = new User
            {
                Username = modelo.Username,
                Password = _utilidades.encriptarSHA256(modelo.Password)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado Correctamente.");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO modelo)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == modelo.Username && 
                u.Password == _utilidades.encriptarSHA256(modelo.Password));

            if (user == null)
            {
                return Unauthorized("Usuario Invalido");
            }

            return Ok(new
            {
                mensaje = "Autenticación exitosa",
                token = _utilidades.generarJWT(user)
            });

        }
    }
}
