using System;
using System.Collections.Generic;
using System.Text;
using SGM.Application.BL.BE;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;

namespace SGM.Application.BL.BC.Service
{
    public class VentaBC : IVentaService
    {
        private readonly IVentaRepository _ventaRepo;
        private readonly IProductoRepository _productoRepo;
        private readonly IClienteRepository _clienteRepo;
        private readonly IUsuarioRepository _usuarioRepo;

        public VentaBC(
            IVentaRepository ventaRepo,
            IProductoRepository productoRepo,
            IClienteRepository clienteRepo,
            IUsuarioRepository usuarioRepo)
        {
            _ventaRepo = ventaRepo;
            _productoRepo = productoRepo;
            _clienteRepo = clienteRepo;
            _usuarioRepo = usuarioRepo;
        }

        public List<Venta> Listar() => _ventaRepo.Listar();

        public Venta? ObtenerPorId(long id)
        {
            var venta = _ventaRepo.ObtenerPorId(id);
            if (venta == null)
                return null;

            venta.Detalles = _ventaRepo.ListarDetallesPorVenta(id);
            venta.Cliente = _clienteRepo.ObtenerPorId(venta.IdCliente);
            venta.Usuario = _usuarioRepo.ObtenerPorId(venta.IdUsuario);

            return venta;
        }

        public long Registrar(CrearVentaRequest request)
        {
            if (request.Detalles == null || request.Detalles.Count == 0)
                throw new Exception("La venta debe tener al menos un producto.");

            var detalles = new List<DetalleVenta>();
            decimal total = 0;

            foreach (var item in request.Detalles)
            {
                var producto = _productoRepo.ObtenerPorId(item.IdProducto);

                if (producto == null)
                    throw new Exception($"El producto con ID {item.IdProducto} no existe.");

                if (producto.Stock < item.Cantidad)
                    throw new Exception($"Stock insuficiente para el producto {producto.Nombre}.");

                var subtotal = item.Precio * item.Cantidad;
                total += subtotal;

                detalles.Add(new DetalleVenta
                {
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    Subtotal = subtotal
                });
            }

            var venta = new Venta
            {
                IdCliente = request.IdCliente,
                IdUsuario = request.IdUsuario,
                Total = total,
                Estado = "Completada",
                FechaRegistro = DateTime.Now
            };

            return _ventaRepo.Registrar(venta, detalles);
        }

        public bool Anular(long id) => _ventaRepo.Anular(id);
    }
}