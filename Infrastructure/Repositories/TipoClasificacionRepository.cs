using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TipoClasificacionRepository : ITipoClasificacionRepository
    {
        private readonly ApplicationDbContext _db;

        public TipoClasificacionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ActualizarTipoClasificacion(TipoClasificacion tipoClasificacion)
        {
            tipoClasificacion.FechaCreacion = DateTime.Now;
            // Arreglar el problema del put
            var tipoClasificacionExistente = _db.TipoClasificaciones.Find(tipoClasificacion.Id);
            if (tipoClasificacionExistente != null)
            {
                _db.Entry(tipoClasificacionExistente).CurrentValues.SetValues(tipoClasificacion);
            }
            else
            {
                _db.TipoClasificaciones.Update(tipoClasificacion);
            }

            return await Guardar();
        }

        public async Task<bool> BorrarTipoClasificacion(TipoClasificacion tipoClasificacion)
        {
            _db.TipoClasificaciones.Remove(tipoClasificacion);
            return await Guardar();
        }

        public async Task<IEnumerable<TipoClasificacion>> BuscarClasificaciones(string nombre)
        {
            IQueryable<TipoClasificacion> query = _db.TipoClasificaciones;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(t => t.Nombre.Contains(nombre) || t.Descripcion.Contains(nombre));
            }
            return await query.ToListAsync();
        }

        public async Task<bool> CrearTipoClasificacion(TipoClasificacion tipoClasificacion)
        {
            tipoClasificacion.FechaCreacion = DateTime.Now;
            _db.TipoClasificaciones.Add(tipoClasificacion);
            return await Guardar();
        }

        public async Task<bool> ExisteTipoClasificacion(int id)
        {
            return await _db.TipoClasificaciones.AnyAsync(t => t.Id == id);
        }

        public async Task<bool> ExisteTipoClasificacion(string nombre)
        {
            return await _db.TipoClasificaciones.AnyAsync(t => t.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }

        public async Task<TipoClasificacion> GetTipoClasificacion(int TipoClasificacionId)
        {
            return await _db.TipoClasificaciones.FirstOrDefaultAsync(t => t.Id == TipoClasificacionId);
        }

        public async Task<ICollection<TipoClasificacion>> GetTipoClasificaciones()
        {
            return await _db.TipoClasificaciones.OrderBy(t => t.Nombre).ToListAsync();
        }

        public async Task<bool> Guardar()
        {
            return await _db.SaveChangesAsync() >= 0;
        }
    }
}
