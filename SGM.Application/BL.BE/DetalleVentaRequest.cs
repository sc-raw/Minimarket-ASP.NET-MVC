using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Application.BL.BE
{
    public class DetalleVentaRequest
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
