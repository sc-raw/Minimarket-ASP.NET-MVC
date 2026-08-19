using Microsoft.Data.SqlClient;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;
using SGM.Infrastructure.DL.DALC.Persistence;
using System.Data;

namespace SGM.Infrastructure.DL.DALC.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly IBDConexion _bd;

        public VentaRepository(IBDConexion bd)
        {
            _bd = bd;
        }

        public List<Venta> Listar()
        {
            var lista = new List<Venta>();

            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_LISTAR_VENTAS", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Venta
                {
                    Id = Convert.ToInt64(dr["Id"]),
                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                    Estado = dr["Estado"].ToString() ?? string.Empty,
                    Total = Convert.ToDecimal(dr["Total"]),
                    NombreCliente = dr["NombreCliente"]?.ToString(),
                    NombreCajero = dr["NombreCajero"]?.ToString()
                });
            }

            return lista;
        }

        public Venta? ObtenerPorId(long id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_OBTENER_VENTAS_POR_ID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            cn.Open();
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new Venta
                {
                    Id = Convert.ToInt64(dr["Id"]),
                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                    Estado = dr["Estado"].ToString() ?? string.Empty,
                    Total = Convert.ToDecimal(dr["Total"])
                };
            }

            return null;
        }

        public List<DetalleVenta> ListarDetallesPorVenta(long idVenta)
        {
            var lista = new List<DetalleVenta>();

            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_LISTAR_DETALLE_VENTA_POR_ID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdVenta", idVenta);

            cn.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new DetalleVenta
                {
                    Id = Convert.ToInt64(dr["Id"]),
                    IdVenta = Convert.ToInt64(dr["IdVenta"]),
                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                    Precio = Convert.ToDecimal(dr["Precio"]),
                    Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                    Producto = new Producto
                    {
                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                        Nombre = dr["NombreProducto"].ToString() ?? string.Empty,
                        Codigo = dr["CodigoProducto"].ToString() ?? string.Empty
                    }
                });
            }

            return lista;
        }

        public long Registrar(Venta venta, List<DetalleVenta> detalles)
        {
            using var cn = _bd.ObtenerConexion();
            cn.Open();

            using var transaction = cn.BeginTransaction();

            try
            {
                // 1. Insertar la venta
                using var cmdVenta = new SqlCommand("SP_REGISTRAR_VENTA", cn, transaction);
                cmdVenta.CommandType = CommandType.StoredProcedure;
                cmdVenta.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                cmdVenta.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);
                cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                cmdVenta.Parameters.AddWithValue("@Estado", venta.Estado);

                var idVentaParam = new SqlParameter("@IdVenta", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                cmdVenta.Parameters.Add(idVentaParam);

                cmdVenta.ExecuteNonQuery();
                long idVenta = Convert.ToInt64(idVentaParam.Value);

                // 2. Insertar los detalles y descontar stock
                foreach (var detalle in detalles)
                {
                    using var cmdDetalle = new SqlCommand("SP_REGISTRAR_DETALLE_VENTA", cn, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                    cmdDetalle.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@Precio", detalle.Precio);
                    cmdDetalle.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);
                    cmdDetalle.ExecuteNonQuery();

                    // Descontar stock
                    using var cmdStock = new SqlCommand("SP_ACTUALIZAR_PRODUCTO_STOCK", cn, transaction);
                    cmdStock.CommandType = CommandType.StoredProcedure;
                    cmdStock.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmdStock.Parameters.AddWithValue("@Cantidad", -detalle.Cantidad); // negativo = descontar
                    cmdStock.ExecuteNonQuery();
                }

                transaction.Commit();
                return idVenta;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool Anular(long id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ANULAR_VENTA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}