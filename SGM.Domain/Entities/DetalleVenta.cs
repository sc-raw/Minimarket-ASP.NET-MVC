using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Domain.Entities
{
    public class DetalleVenta
    {
        public long Id { get; set; }
        public long IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; }

        // Navegación (opcional)
        public Venta? Venta { get; set; }
        public Producto? Producto { get; set; }
    }
}
