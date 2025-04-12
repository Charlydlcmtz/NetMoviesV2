using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public string? RutaImagen { get; set; }
        public string? RutaLocalImagen { get; set; }

        // Relación con Categoría
        public int TipoClasificacionId { get; set; }
        public TipoClasificacion TipoClasificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Relación con Categoría
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }
}
