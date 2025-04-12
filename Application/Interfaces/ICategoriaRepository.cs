using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<ICollection<Categoria>> GetCategorias();
        Task<Categoria> GetCategoria(int id);
        Task<bool> ExisteCategoria(int id);
        Task<bool> ExisteCategoria(string nombre);
        Task<bool> CrearCategoria(Categoria categoria);
        Task<bool> ActualizarCategoria(Categoria categoria);
        Task<bool> BorrarCategoria(Categoria categoria);
        Task<bool> Guardar();

    }
}
