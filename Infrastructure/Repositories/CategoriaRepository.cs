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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ActualizarCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            //Arreglar Problema de Put
            var categoriaExistente = _db.Categorias.Find(categoria.Id);
            if (categoriaExistente != null)
            {
                _db.Entry(categoriaExistente).CurrentValues.SetValues(categoria);
            }
            else
            {
                _db.Categorias.Update(categoria);
            }

                return await Guardar();
        }

        public async Task<bool> BorrarCategoria(Categoria categoria)
        {
            _db.Categorias.Remove(categoria);
            return await Guardar();
        }

        public async Task<bool> CrearCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            _db.Categorias.Add(categoria);
            return await Guardar();
        }

        public async Task<bool> ExisteCategoria(int id)
        {
            return await _db.Categorias.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> ExisteCategoria(string nombre)
        {
            bool valor = await _db.Categorias.AnyAsync(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public async Task<Categoria> GetCategoria(int CategoriaId)
        {
            return await _db.Categorias.FirstOrDefaultAsync(c => c.Id == CategoriaId);
        }

        public async Task<ICollection<Categoria>> GetCategorias()
        {
            return await _db.Categorias.OrderBy(c => c.Nombre).ToListAsync();
        }

        public async Task<bool> Guardar()
        {
            return await _db.SaveChangesAsync() >= 0;
        }
    }
}
