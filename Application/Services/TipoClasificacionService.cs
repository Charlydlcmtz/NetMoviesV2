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
    public class TipoClasificacionService : ITipoClasificacionService
    {
        private readonly ITipoClasificacionRepository _repo;

        public TipoClasificacionService(ITipoClasificacionRepository repo)
        {
            _repo = repo;
        }

        public async Task ActualizarTipoClasificacion(int id, ActualizarTipoClasificacionDto actualizarTipoClasificacionDto)
        {
            var tipoClasificacion = await _repo.GetTipoClasificacion(id);
            if (tipoClasificacion == null) return;

            tipoClasificacion.Nombre = actualizarTipoClasificacionDto.Nombre;
            tipoClasificacion.Descripcion = actualizarTipoClasificacionDto.Descripcion;
            tipoClasificacion.FechaCreacion = DateTime.Now;

            await _repo.ActualizarTipoClasificacion(tipoClasificacion);
        }

        public async Task BorrarTipoClasificacion(int id)
        {
            var tipoClasificacion = await _repo.GetTipoClasificacion(id);
            if (tipoClasificacion != null)
            {
                await _repo.BorrarTipoClasificacion(tipoClasificacion);
            }
        }

        public async Task<IEnumerable<TipoClasificacionDto>> BuscarClasificaciones(string nombre)
        {
            var tipoClasificaciones = await _repo.BuscarClasificaciones(nombre);
            return tipoClasificaciones.Select(t => new TipoClasificacionDto 
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                FechaCreacion = t.FechaCreacion
            });
        }

        public async Task CrearTipoClasificacion(CrearTipoClasificacionDto creartipoClasificacionDto)
        {
            var tipoClasificacionNueva = new TipoClasificacion
            {
                Nombre = creartipoClasificacionDto.Nombre,
                Descripcion = creartipoClasificacionDto.Descripcion,
            };
            await _repo.CrearTipoClasificacion(tipoClasificacionNueva);
        }

        public async Task<bool> ExisteTipoClasificacion(int id)
        {
            return await _repo.ExisteTipoClasificacion(id);
        }

        public async Task<bool> ExisteTipoClasificacion(string nombre)
        {
            return await _repo.ExisteTipoClasificacion(nombre);
        }

        public async Task<TipoClasificacionDto> GetTipoClasificacion(int id)
        {
            var tipoClasificacion = await _repo.GetTipoClasificacion(id);
            if ( tipoClasificacion == null ) return null;

            return new TipoClasificacionDto
            {
                Nombre = tipoClasificacion.Nombre,
                Descripcion = tipoClasificacion.Descripcion,
                FechaCreacion = tipoClasificacion.FechaCreacion,
            };
        }

        public async Task<IEnumerable<TipoClasificacionDto>> GetTipoClasificaciones()
        {
            var tipoClasificaciones = await _repo.GetTipoClasificaciones();
            return tipoClasificaciones.Select(t => new TipoClasificacionDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                FechaCreacion = t.FechaCreacion,
            });
        }
    }
}
