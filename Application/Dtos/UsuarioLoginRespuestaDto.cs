﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UsuarioLoginRespuestaDto
    {
        public UsuarioDatosDto Usuario { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
