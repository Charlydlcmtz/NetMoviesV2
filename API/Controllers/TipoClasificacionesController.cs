using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/Clasificaciones")]
    [ApiController]
    public class TipoClasificacionesController : ControllerBase
    {
        private readonly ITipoClasificacionService _service;

        public TipoClasificacionesController(ITipoClasificacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoClasificacionDto>>> GetTipoClasificacion()
        {
            var tipoClasificaciones = await _service.GetTipoClasificaciones();
            return Ok(tipoClasificaciones);
        }

        [HttpGet("{ClasificacionId}")]
        public async Task<ActionResult<TipoClasificacionDto>> GetTipoClasificacion(int ClasificacionId)
        {
            var tipoClasificacion = await _service.GetTipoClasificacion(ClasificacionId);
            if (tipoClasificacion == null) return NotFound(new { mensaje = "No se encontro la clasificacion." });
            return Ok(tipoClasificacion);
        }

        [HttpPost]
        public async Task<ActionResult> CrearTipoClasificacion([FromBody] CrearTipoClasificacionDto crearTipoClasificacionDto)
        {
            await _service.CrearTipoClasificacion(crearTipoClasificacionDto);
            return Ok(new { mensaje = "Clasificación creada correctamente." });
        }
    }
}
