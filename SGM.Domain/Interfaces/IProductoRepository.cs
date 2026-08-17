using System;
using System.Collections.Generic;
using System.Text;
using SGM.Domain.Entities;

namespace SGM.Domain.Interfaces
{
    public interface IProductoRepository
    {
        List<Producto> Listar();
        Producto? ObtenerPorId(int id);
        bool Registrar(Producto producto);
        bool Actualizar(Producto producto);
        bool Eliminar(int id);
        bool ActualizarStock(int idProducto, int cantidad);
    }
}