using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategorias()
        {
            var categorias = await _service.GetCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDto>> GetCategoria(int id)
        {
            var categoria = await _service.GetCategoria(id);
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> CrearCategoria([FromBody] CrearCategoriaDto crearCategoriaDto)
        {
            await _service.CrearCategoria(crearCategoriaDto);
            return Ok( new { mensaje = "Categoria creada correctamente." });
        }

        [HttpPut("{categoriaId:int}", Name = "ActualizarCategoria")]
        public async Task<ActionResult> ActualizarCategoria(int categoriaId, [FromBody] ActualizarCategoriaDto actualizarCategoriaDto)
        {
            if (!ModelState.IsValid || categoriaId != actualizarCategoriaDto.Id)
            {
                return BadRequest();
            }

            await _service.ActualizarCategoria(categoriaId, actualizarCategoriaDto);
            return Ok(new { mensaje = "Categoria actualizada correctamente" });
        }

        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        public async Task<ActionResult> BorrarCategoria(int categoriaId)
        {
            await _service.BorrarCategoria(categoriaId);
            return Ok(new { mensaje = "Categoria eliminada correctamente" });
        }

        
    }
}
