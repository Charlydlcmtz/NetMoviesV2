using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public string? RutaImagen { get; set; }
        public string? RutaLocalImagen { get; set; }
        public int TipoClasificacionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CategoriaId { get; set; }
    }
}
