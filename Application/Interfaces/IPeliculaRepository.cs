using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPeliculaRepository
    {
        Task<ICollection<Pelicula>> GetPeliculas(int pageNumber, int pageSize);
        Task<int> GetTotalPeliculas();
        Task<ICollection<Pelicula>> GetPeliculasEnCategoria(int catId);
        Task<IEnumerable<Pelicula>> BuscarPeliculas(string nombre);
        Task<Pelicula> GetPelicula(int PeliculaId);
        Task<bool> ExistePelicula(int id);
        Task<bool> ExistePelicula(string nombre);
        Task<bool> CrearPelicula(Pelicula pelicula);
        Task<bool> ActualizarPelicula(Pelicula pelicula);
        Task<bool> BorrarPelicula(Pelicula pelicula);
        Task<bool> Guardar();
    }
}
