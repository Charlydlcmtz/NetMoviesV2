using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITipoClasificacionService
    {
        Task<IEnumerable<TipoClasificacionDto>> GetTipoClasificaciones();
        Task<TipoClasificacionDto> GetTipoClasificacion(int id);
        Task<bool> ExisteTipoClasificacion(int id);
        Task<bool> ExisteTipoClasificacion(string nombre);
        Task CrearTipoClasificacion(CrearTipoClasificacionDto creartipoClasificacionDto);
        Task ActualizarTipoClasificacion(int id, ActualizarTipoClasificacionDto actualizarTipoClasificacionDto);
        Task BorrarTipoClasificacion(int id);
        Task<IEnumerable<TipoClasificacionDto>> BuscarClasificaciones(string nombre);
    }
}
