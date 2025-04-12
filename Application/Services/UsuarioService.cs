using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<AppUsuarios> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtProvider _jwtProvider;

        public UsuarioService (
            UserManager<AppUsuarios> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtProvider = jwtProvider;
        }


        public async Task<UsuarioDto?> GetUsuarioByIdAsync(string usuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                NombreUsuario = usuario.UserName,
                Role = "", // Opcional
                Password = "" // Nunca se devuelve
            };
        }

        public async Task<IEnumerable<UsuarioDto>> GetUsuariosAsync()
        {
            var usuarios = _userManager.Users.ToList();

            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                NombreUsuario = u.UserName,
                Role = "", // Puedes obtener roles si es necesario
                Password = "" //nunca se expone, solo si es interno
            });
        }

        public async Task<bool> IsUniqueUsuarioAsync(string usuario)
        {
            var usuarioBd = await _userManager.FindByNameAsync(usuario);
            return usuarioBd == null;
        }

        public async Task<UsuarioLoginRespuestaDto> LoginAsync(UsuarioLoginDto usuarioLoginDto)
        {
            var usuario = await _userManager.FindByNameAsync(usuarioLoginDto.NombreUsuario);

            if (usuario == null || !await _userManager.CheckPasswordAsync(usuario, usuarioLoginDto.Password))
            {
                return new UsuarioLoginRespuestaDto
                {
                    Token = string.Empty,
                    Usuario = null,
                };
            }

            var roles = await _userManager.GetRolesAsync(usuario);
            var token = _jwtProvider.GenerateToken(usuario.UserName, roles.FirstOrDefault());

            return new UsuarioLoginRespuestaDto
            {
                Token = token,
                Role = roles.FirstOrDefault(),
                Usuario = new UsuarioDatosDto
                {
                    ID = usuario.Id,
                    Username = usuario.UserName,
                    Nombre = usuario.Nombre,
                }
            };
        }

        public async Task<UsuarioRegistroRespuestaDto> RegistroAsync(UsuarioRegistroDto usuarioRegistroDto)
        {
            var nuevoUsuario = new AppUsuarios
            {
                UserName = usuarioRegistroDto.NombreUsuario,
                Email = usuarioRegistroDto.NombreUsuario,
                NormalizedEmail = usuarioRegistroDto.NombreUsuario.ToUpper(),
                Nombre = usuarioRegistroDto.Nombre
            };

            var resultado = await _userManager.CreateAsync(nuevoUsuario, usuarioRegistroDto.Password);
            if (!resultado.Succeeded)
            {
                return new UsuarioRegistroRespuestaDto
                {
                    Token = string.Empty,
                    Usuario = null,
                };
            }

            // Crear roles si no existen
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Registrado"));
            }

            // Asignar rol por defecto
            var rolAsignado = string.IsNullOrEmpty(usuarioRegistroDto.Role) ? "Registrado" : usuarioRegistroDto.Role;
            await _userManager.AddToRoleAsync(nuevoUsuario, rolAsignado);

            var roles = await _userManager.GetRolesAsync(nuevoUsuario);
            var token = _jwtProvider.GenerateToken(nuevoUsuario.UserName, roles.FirstOrDefault());

            return new UsuarioRegistroRespuestaDto
            {
                Token = token,
                Role = roles.FirstOrDefault(),
                Usuario = new UsuarioDatosDto
                {
                    ID = nuevoUsuario.Id,
                    Nombre = nuevoUsuario.Nombre,
                    Username = nuevoUsuario.UserName,
                }
            };
        }
    }
}
