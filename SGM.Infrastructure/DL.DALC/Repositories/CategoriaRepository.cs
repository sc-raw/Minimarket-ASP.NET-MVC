using Microsoft.Data.SqlClient;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;
using SGM.Infrastructure.DL.DALC.Persistence;
using System.Data;

namespace SGM.Infrastructure.DL.DALC.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IBDConexion _bd;

        public CategoriaRepository(IBDConexion bd)
        {
            _bd = bd;
        }

        public List<Categoria> Listar()
        {
            var lista = new List<Categoria>();

            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_LISTAR_CATEGORIA", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Categoria
                {
                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                    Nombre = dr["Nombre"].ToString() ?? string.Empty
                });
            }

            return lista;
        }

        public Categoria? ObtenerPorId(int id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_OBTENER_CATEGORIA_POR_ID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCategoria", id);

            cn.Open();
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new Categoria
                {
                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                    Nombre = dr["Nombre"].ToString() ?? string.Empty
                };
            }

            return null;
        }

        public bool Registrar(Categoria categoria)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_REGISTRAR_CATEGORIA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Actualizar(Categoria categoria)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ACTUALIZAR_CATEGORIA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
            cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ELIMINAR_CATEGORIA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCategoria", id);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
