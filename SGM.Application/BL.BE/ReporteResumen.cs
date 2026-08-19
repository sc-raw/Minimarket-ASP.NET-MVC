using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Application.BL.BE
{
    public class ReporteResumen
    {
        public int TotalClientes { get; set; }
        public int TotalVentas { get; set; }
        public int TotalCategorias { get; set; }
        public int TotalProductos { get; set; }
        public int TotalEmpleados { get; set; }
        public decimal MontoTotalVendido { get; set; }
    }
}
