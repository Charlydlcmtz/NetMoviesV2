using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PeliculaService : IPeliculaService
    {

        private readonly IPeliculaRepository _repo;

        public PeliculaService(IPeliculaRepository repo)
        {
            _repo = repo;
        }

        public async Task ActualizarPelicula(int id, ActualizarPeliculaDto actualizarPeliculaDto, string baseUrl)
        {
            var pelicula = await _repo.GetPelicula(id);
            if (pelicula == null) return;

            // Guardar nueva imagen si viene en el DTO
            if (actualizarPeliculaDto.Imagen != null && actualizarPeliculaDto.Imagen.Length > 0)
            {
                var nombreArchivo = $"{id}_{Guid.NewGuid()}{Path.GetExtension(actualizarPeliculaDto.Imagen.FileName)}";
                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);

                // Eliminar imagen anterior si existe (opcional)
                if (!string.IsNullOrEmpty(pelicula.RutaLocalImagen))
                {
                    var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(), pelicula.RutaLocalImagen.Replace("/", "\\"));
                    if (File.Exists(rutaAnterior))
                    {
                        File.Delete(rutaAnterior);
                    }
                }

                var rutaFisica = Path.Combine(rutaCarpeta, nombreArchivo);
                using (var stream = new FileStream(rutaFisica, FileMode.Create))
                {
                    await actualizarPeliculaDto.Imagen.CopyToAsync(stream);
                }

                pelicula.RutaImagen = $"{baseUrl}/images/{nombreArchivo}";
                pelicula.RutaLocalImagen = $"wwwroot/images/{nombreArchivo}";
            }

            // Actualizar datos base
            pelicula.Nombre = actualizarPeliculaDto.Nombre;
            pelicula.Descripcion = actualizarPeliculaDto.Descripcion;
            pelicula.Duracion = actualizarPeliculaDto.Duracion;
            pelicula.FechaCreacion = DateTime.Now; // O considerar FechaActualizacion
            pelicula.CategoriaId = actualizarPeliculaDto.categoriaId;

            await _repo.ActualizarPelicula(pelicula);
        }

        public async Task BorrarPelicula(int id)
        {
            var pelicula = await _repo.GetPelicula(id);
            if (pelicula != null)
            {
                await _repo.BorrarPelicula(pelicula);
            }
        }

        public async Task<IEnumerable<PeliculaDto>> BuscarPeliculas(string nombre)
        {
            var peliculas = await _repo.BuscarPeliculas(nombre);
            return peliculas.Select(p => new PeliculaDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                CategoriaId = p.CategoriaId
            });
        }

        public async Task CrearPelicula(CrearPeliculaDto crearPeliculaDto)
        {
            var nueva = new Pelicula
            {
                Nombre = crearPeliculaDto.Nombre,
                Descripcion = crearPeliculaDto.Descripcion,
                Duracion = crearPeliculaDto.Duracion,
                FechaCreacion = DateTime.Now
            };

            await _repo.CrearPelicula(nueva);
        }

        public async Task<bool> ExistePelicula(int id)
        {
            return await _repo.ExistePelicula(id);
        }

        public async Task<bool> ExistePelicula(string nombre)
        {
            return await _repo.ExistePelicula(nombre);
        }

        public async Task<PeliculaDto> GetPelicula(int PeliculaId)
        {
            var pelicula = await _repo.GetPelicula(PeliculaId);
            if (pelicula == null) return null;

            return new PeliculaDto
            {
                Id = pelicula.Id,
                Nombre = pelicula.Nombre,
                Descripcion = pelicula.Descripcion,
                CategoriaId = pelicula.CategoriaId
            };
        }

        public async Task<IEnumerable<PeliculaDto>> GetPeliculas(int pageNumber, int pageSize)
        {
            var peliculas = await _repo.GetPeliculas(pageNumber, pageSize);
            return peliculas.Select(p => new PeliculaDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Duracion = p.Duracion,
                FechaCreacion = p.FechaCreacion
            });
        }

        public async Task<ICollection<PeliculaDto>> GetPeliculasEnCategoria(int catId)
        {
            var peliculas = await _repo.GetPeliculasEnCategoria(catId);

            return peliculas.Select(p => new PeliculaDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                CategoriaId = p.CategoriaId
            }).ToList();
        }

        public async Task<int> GetTotalPeliculas()
        {
            return await _repo.GetTotalPeliculas();
        }
    }
}
