using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly ApplicationDbContext _db;

        public PeliculaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ActualizarPelicula(Pelicula pelicula)
        {
            pelicula.FechaCreacion = DateTime.Now;

            //Arreglar Problema de Put
            var peliculaExistente = _db.Peliculas.Find(pelicula.Id);
            if (peliculaExistente != null)
            {
                _db.Entry(peliculaExistente).CurrentValues.SetValues(pelicula);
            }
            else
            {
                _db.Peliculas.Update(pelicula);
            }

            return await Guardar();
        }

        public async Task<bool> BorrarPelicula(Pelicula pelicula)
        {
            _db.Peliculas.Remove(pelicula);
            return await Guardar();
        }

        public async Task<IEnumerable<Pelicula>> BuscarPeliculas(string nombre)
        {
            IQueryable<Pelicula> query = _db.Peliculas;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Nombre.Contains(nombre) || e.Descripcion.Contains(nombre));
            }
            return await query.ToListAsync();
        }

        public async Task<bool> CrearPelicula(Pelicula pelicula)
        {
            pelicula.FechaCreacion = DateTime.Now;
            await _db.Peliculas.AddAsync(pelicula);
            return await Guardar();
        }

        public async Task<bool> ExistePelicula(int id)
        {
            return await _db.Peliculas.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> ExistePelicula(string nombre)
        {
            bool valor = await _db.Peliculas.AnyAsync(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public async Task<Pelicula> GetPelicula(int PeliculaId)
        {
            return await _db.Peliculas.FirstOrDefaultAsync(c => c.Id == PeliculaId);
        }

        public async Task<ICollection<Pelicula>> GetPeliculas(int pageNumber, int pageSize)
        {
            return await _db.Peliculas.OrderBy(p => p.Nombre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<ICollection<Pelicula>> GetPeliculasEnCategoria(int catId)
        {
            return await _db.Peliculas.Include( ca => ca.Categoria ).Where(ca => ca.CategoriaId == catId).ToListAsync();
        }

        public async Task<int> GetTotalPeliculas()
        {
            return await _db.Peliculas.CountAsync();
        }

        public async Task<bool> Guardar()
        {
            return await _db.SaveChangesAsync() >= 0;
        }
    }
}
