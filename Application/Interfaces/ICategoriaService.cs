using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDto>> GetCategorias();
        Task<CategoriaDto> GetCategoria(int id);
        Task<bool> ExisteCategoria(int id);
        Task<bool> ExisteCategoria(string nombre);
        Task CrearCategoria(CrearCategoriaDto crearCategoriaDto);
        Task ActualizarCategoria(int id, ActualizarCategoriaDto actualizarCategoriaDto);
        Task BorrarCategoria(int id);
    }
}
