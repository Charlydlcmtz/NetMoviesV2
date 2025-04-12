using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<AppUsuarios> _userManager;

        public UsuarioRepository(UserManager<AppUsuarios> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUsuarios?> GetUsuarioByIdAsync(string usuarioId)
        {
            return await _userManager.FindByIdAsync(usuarioId);
        }

        public async Task<AppUsuarios?> GetUsuarioByNombreAsync(string nombreUsuario)
        {
            return await _userManager.FindByNameAsync(nombreUsuario);
        }

        public async Task<IEnumerable<AppUsuarios>> GetUsuariosAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<bool> IsUniqueUserAsync(string usuario)
        {
            var user = await _userManager.FindByNameAsync(usuario);
            return user == null;
        }

        public async Task<AppUsuarios> RegistrarUsuarioAsync(AppUsuarios nuevoUsuario, string password)
        {
            var result = await _userManager.CreateAsync(nuevoUsuario, password);
            if (!result.Succeeded) throw new Exception("Error al registrar usuario.");
            return nuevoUsuario;
        }

        public async Task<AppUsuarios?> ValidarCredencialesAsync(string usuario, string password)
        {
            var user = await _userManager.FindByNameAsync(usuario);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }

            return null;
        }
    }
}
