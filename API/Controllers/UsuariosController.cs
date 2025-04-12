using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly IJwtProvider _jwtProvider;

        public UsuariosController(IUsuarioService service, IJwtProvider jwtProvider)
        {
            _service = service;
            _jwtProvider = jwtProvider;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet(Name = "Listar Usuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _service.GetUsuariosAsync();
            return Ok(usuarios);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(string usuarioId)
        {
            var usuario = await _service.GetUsuarioByIdAsync(usuarioId);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }
         
        [AllowAnonymous]
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioRegistroDto>> Registro([FromBody] UsuarioRegistroDto usuarioRegistroDto)
        {
            var usuarioExiste = await _service.IsUniqueUsuarioAsync(usuarioRegistroDto.NombreUsuario);
            if (!usuarioExiste)
            {
                return BadRequest(new { mensaje = "El nombre del usuario ya existe" });
            }

            var resultado = await _service.RegistroAsync(usuarioRegistroDto);

            if (resultado.Usuario == null)
            {
                return BadRequest(new { mensaje = "Error al registrar al usuario" });
            }
            return Ok(resultado);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioLoginDto>> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var resultado = await _service.LoginAsync(usuarioLoginDto);

            if (resultado.Usuario == null || string.IsNullOrEmpty(resultado.Token))
            {
                return BadRequest( new { mensaje = "Nombre de usuario o contraseña inválidos" } );
            }
            return Ok(resultado);
        }

        [HttpPost("check-token")]
        public IActionResult CheckRenewToken([FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                var token = _jwtProvider.ExtracTokenFromHeader(authHeader);
                var newToken = _jwtProvider.RenewToken(token);

                return Ok( new { token = newToken } );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al renovar el token: {ex.Message}" });
            }
        }
    }
}
