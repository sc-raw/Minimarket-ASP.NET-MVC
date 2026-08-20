using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Domain.Entities
{
    public class Venta
    {
        public long Id { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }        
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Completada";
        public decimal Total { get; set; }

        public string? NombreCliente { get; set; }
        public string? NombreCajero { get; set; }

        public Cliente? Cliente { get; set; }
        public Usuario? Usuario { get; set; }
        public List<DetalleVenta> Detalles { get; set; } = new();
    }
}
