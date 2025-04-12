using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;

        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ExtracTokenFromHeader(string authHeader)
        {
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                throw new ArgumentException("Invalid Authorization header format.");
            }

            return authHeader.Substring("Bearer ".Length).Trim();
        }

        public string GenerateToken(string username, string role)
        {
            var manejadorToken = new JwtSecurityTokenHandler();
            var claveSecreta = _configuration["ApiSettings:Secreta"];
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role ?? "Registrado")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);
            return manejadorToken.WriteToken(token);
        }

        public string RenewToken(string token)
        {
            //Aqui puedes validar el token renovarlo
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["ApiSettings:Secreta"]);
            Console.WriteLine($"Token Nuevo: {key}");

            var securityToken = handler.ReadToken(token) as JwtSecurityToken;
            if (securityToken == null)
            {
                throw new ArgumentException("Invalid token");
            }

            //Generar un nuevo token
            var newTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(securityToken.Claims),
                Expires = DateTime.UtcNow.AddMinutes(30), //Renovar por 30 minutos
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var newToken = handler.CreateToken(newTokenDescriptor);
            return handler.WriteToken(newToken);
        }
    }
}
