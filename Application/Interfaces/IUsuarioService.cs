using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetUsuariosAsync();
        Task<UsuarioDto?> GetUsuarioByIdAsync(string usuarioId);
        Task<bool> IsUniqueUsuarioAsync(string usuario);
        Task<UsuarioLoginRespuestaDto> LoginAsync(UsuarioLoginDto usuarioLoginDto);
        Task<UsuarioRegistroRespuestaDto> RegistroAsync(UsuarioRegistroDto usuarioRegistroDto);
    }
}
