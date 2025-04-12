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
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repo;

        public CategoriaService(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public async Task ActualizarCategoria(int id, ActualizarCategoriaDto actualizarCategoriaDto)
        {
            var categoria = await _repo.GetCategoria(id);
            if (categoria == null) return;

            categoria.Nombre = actualizarCategoriaDto.Nombre;
            categoria.FechaCreacion = DateTime.Now;

            await _repo.ActualizarCategoria(categoria);
        }

        public async Task BorrarCategoria(int id)
        {
            var categoria = await _repo.GetCategoria(id);
            if (categoria != null)
            {
                await _repo.BorrarCategoria(categoria);
            }
        }

        public async Task CrearCategoria(CrearCategoriaDto crearCategoriaDto)
        {
            var categoriaNueva = new Categoria
            {
                Nombre = crearCategoriaDto.Nombre,
                FechaCreacion = DateTime.Now,
            };
            await _repo.CrearCategoria(categoriaNueva);
        }

        public async Task<bool> ExisteCategoria(int id)
        {
            return await _repo.ExisteCategoria(id);
        }

        public async Task<bool> ExisteCategoria(string nombre)
        {
            return await _repo.ExisteCategoria(nombre);
        }

        public async Task<CategoriaDto> GetCategoria(int id)
        {
            var categoria = await _repo.GetCategoria(id);
            if (categoria == null) return null;

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                FechaCreacion = categoria.FechaCreacion,
            };
        }

        public async Task<IEnumerable<CategoriaDto>> GetCategorias()
        {
            var categorias = await _repo.GetCategorias();
            return categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                FechaCreacion = c.FechaCreacion,
            });
        }
    }
}
