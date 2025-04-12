using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITipoClasificacionRepository
    {
        Task<ICollection<TipoClasificacion>> GetTipoClasificaciones();
        Task<TipoClasificacion> GetTipoClasificacion(int id);
        Task<bool> ExisteTipoClasificacion(int id);
        Task<bool> ExisteTipoClasificacion(string nombre);
        Task<bool> CrearTipoClasificacion(TipoClasificacion tipoClasificacion);
        Task<bool> ActualizarTipoClasificacion(TipoClasificacion tipoClasificacion);
        Task<bool> BorrarTipoClasificacion(TipoClasificacion tipoClasificacion);
        Task<IEnumerable<TipoClasificacion>> BuscarClasificaciones(string nombre);
        Task<bool> Guardar();
    }
}
