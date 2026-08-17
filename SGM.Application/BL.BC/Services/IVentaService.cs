using System;
using System.Collections.Generic;
using System.Text;
using SGM.Application.BL.BE;
using SGM.Domain.Entities;

namespace SGM.Application.BL.BC.Service
{
    public interface IVentaService
    {
        List<Venta> Listar();
        Venta? ObtenerPorId(long id);
        long Registrar(CrearVentaRequest request);
        bool Anular(long id);
    }
}