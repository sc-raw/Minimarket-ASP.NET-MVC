using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Application.BL.BE
{
    public class CrearVentaRequest
    {
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public List<DetalleVentaRequest> Detalles { get; set; } = new();
    }
}