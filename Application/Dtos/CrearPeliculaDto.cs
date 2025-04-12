using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CrearPeliculaDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public string? RutaImagen { get; set; }
        public IFormFile Imagen { get; set; }
        public int TipoClasificacionId { get; set; }
        public int categoriaId { get; set; }
    }
}
