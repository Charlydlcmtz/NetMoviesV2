using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : Controller
    {
        private readonly IPeliculaService _service;

        public PeliculasController(IPeliculaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaDto>>> GetPeliculas()
        {
            var peliculas = await _service.GetPeliculas(1, 10);
            return Ok(peliculas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaDto>> GetPelicula(int id)
        {
            var pelicula = await _service.GetPelicula(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return Ok(pelicula);
        }

        [HttpPost]
        public async Task<ActionResult> CrearPelicula([FromBody] CrearPeliculaDto crearPeliculaDto)
        {
            await _service.CrearPelicula(crearPeliculaDto);
            return Ok(new { mensaje = "Película creada correctamente" });
        }

        [HttpPut("{peliculaId:int}", Name = "ActualizarPelicula")]
        [Consumes("multipart/form-data")] // ✅ Muy importante para Swagger
        public async Task<ActionResult> ActualizarPelicula(int peliculaId, [FromForm] ActualizarPeliculaDto actualizarPeliculaDto)
        {
            if (!ModelState.IsValid || peliculaId != actualizarPeliculaDto.Id)
            {
                return BadRequest();
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            await _service.ActualizarPelicula(peliculaId, actualizarPeliculaDto, baseUrl);
            return Ok(new { mensaje = "Pelicula actualizada correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarPelicula(int id)
        {
            await _service.BorrarPelicula(id);
            return Ok(new { mensaje = "Película borrada correctamente" });
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<PeliculaDto>>> BuscarPeliculas([FromQuery] string nombre)
        {
            var peliculas = await _service.BuscarPeliculas(nombre);
            return Ok(peliculas);
        }

        [HttpGet("categoria/{catId}")]
        public async Task<ActionResult<IEnumerable<PeliculaDto>>> GetPorCategoria(int catId)
        {
            var peliculas = await _service.GetPeliculasEnCategoria(catId);
            return Ok(peliculas);
        }

        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalPeliculas()
        {
            var total = await _service.GetTotalPeliculas();
            return Ok(total);
        }

    }
}
