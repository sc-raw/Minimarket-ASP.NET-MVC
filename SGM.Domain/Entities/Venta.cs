using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Domain.Entities
{
    public class Venta
    {
        public long Id { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }          // El cajero que registró la venta
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Completada";
        public decimal Total { get; set; }

        // Navegación (opcional)
        public Cliente? Cliente { get; set; }
        public Usuario? Usuario { get; set; }
        public List<DetalleVenta> Detalles { get; set; } = new();
    }
}
