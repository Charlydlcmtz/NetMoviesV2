using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPeliculaService
    {
        Task<IEnumerable<PeliculaDto>> GetPeliculas(int pageNumber, int pageSize);
        Task<int> GetTotalPeliculas();
        Task<ICollection<PeliculaDto>> GetPeliculasEnCategoria(int catId);
        Task<IEnumerable<PeliculaDto>> BuscarPeliculas(string nombre);
        Task<PeliculaDto> GetPelicula(int PeliculaId);
        Task<bool> ExistePelicula(int id);
        Task<bool> ExistePelicula(string nombre);
        Task CrearPelicula(CrearPeliculaDto crearPeliculaDto);
        Task ActualizarPelicula(int id, ActualizarPeliculaDto actualizarPeliculaDto, string baseUrl);
        Task BorrarPelicula(int id);
    }
}
