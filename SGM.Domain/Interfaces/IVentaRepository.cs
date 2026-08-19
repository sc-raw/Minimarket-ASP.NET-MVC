using System;
using System.Collections.Generic;
using System.Text;
using SGM.Domain.Entities;

namespace SGM.Domain.Interfaces
{
    public interface IVentaRepository
    {
        List<Venta> Listar();
        Venta? ObtenerPorId(long id);
        List<DetalleVenta> ListarDetallesPorVenta(long idVenta);
        long Registrar(Venta venta, List<DetalleVenta> detalles);
        bool Anular(long id);
    }
}