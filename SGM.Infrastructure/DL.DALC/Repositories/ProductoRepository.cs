using Microsoft.Data.SqlClient;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;
using SGM.Infrastructure.DL.DALC.Persistence;
using System.Data;

namespace SGM.Infrastructure.DL.DALC.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IBDConexion _bd;

        public ProductoRepository(IBDConexion bd)
        {
            _bd = bd;
        }

        public List<Producto> Listar()
        {
            var lista = new List<Producto>();

            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_LISTAR_PRODUCTO", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Producto
                {
                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                    Codigo = dr["Codigo"].ToString() ?? string.Empty,
                    Nombre = dr["Nombre"].ToString() ?? string.Empty,
                    Descripcion = dr["Descripcion"] as string,
                    Precio = Convert.ToDecimal(dr["Precio"]),
                    Stock = Convert.ToInt32(dr["Stock"]),
                    Estado = Convert.ToBoolean(dr["Estado"]),
                    IdCategoria = Convert.ToInt32(dr["IdCategoria"])
                });
            }

            return lista;
        }

        public Producto? ObtenerPorId(int id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_OBTENER_PRODUCTO_POR_ID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdProducto", id);

            cn.Open();
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new Producto
                {
                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                    Codigo = dr["Codigo"].ToString() ?? string.Empty,
                    Nombre = dr["Nombre"].ToString() ?? string.Empty,
                    Descripcion = dr["Descripcion"] as string,
                    Precio = Convert.ToDecimal(dr["Precio"]),
                    Stock = Convert.ToInt32(dr["Stock"]),
                    Estado = Convert.ToBoolean(dr["Estado"]),
                    IdCategoria = Convert.ToInt32(dr["IdCategoria"])
                };
            }

            return null;
        }

        public bool Registrar(Producto producto)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_REGISTRAR_PRODUCTO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", producto.Codigo);
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", (object?)producto.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Precio", producto.Precio);
            cmd.Parameters.AddWithValue("@Stock", producto.Stock);
            cmd.Parameters.AddWithValue("@Estado", producto.Estado);
            cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Actualizar(Producto producto)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ACTUALIZAR_PRODUCTO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
            cmd.Parameters.AddWithValue("@Codigo", producto.Codigo);
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", (object?)producto.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Precio", producto.Precio);
            cmd.Parameters.AddWithValue("@Stock", producto.Stock);
            cmd.Parameters.AddWithValue("@Estado", producto.Estado);
            cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ELIMINAR_PRODUCTO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdProducto", id);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool ActualizarStock(int idProducto, int cantidad)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ACTUALIZAR_PRODUCTO_STOCK", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdProducto", idProducto);
            cmd.Parameters.AddWithValue("@Cantidad", cantidad);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}