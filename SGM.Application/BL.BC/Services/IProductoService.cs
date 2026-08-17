using System;
using System.Collections.Generic;
using System.Text;
using SGM.Domain.Entities;

namespace SGM.Application.BL.BC.Service
{
    public interface IProductoService
    {
        List<Producto> Listar();
        Producto? ObtenerPorId(int id);
        bool Registrar(Producto producto);
        bool Actualizar(Producto producto);
        bool Eliminar(int id);
    }
}
