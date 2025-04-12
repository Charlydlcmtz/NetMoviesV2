using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UsuarioRegistroDto
    {
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
