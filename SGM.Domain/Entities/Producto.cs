using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Domain.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; } = true;
        public int IdCategoria { get; set; }

        // Navegación (opcional pero útil)
        public Categoria? Categoria { get; set; }
    }
}
