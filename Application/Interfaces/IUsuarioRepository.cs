using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<AppUsuarios>> GetUsuariosAsync();
        Task<AppUsuarios?> GetUsuarioByIdAsync(string usuarioId);
        Task<AppUsuarios?> GetUsuarioByNombreAsync(string nombreUsuario);
        Task<bool> IsUniqueUserAsync(string usuario);
        Task<AppUsuarios?> ValidarCredencialesAsync(string usuario, string password);
        Task<AppUsuarios> RegistrarUsuarioAsync(AppUsuarios nuevoUsuario, string password);
    }
}
