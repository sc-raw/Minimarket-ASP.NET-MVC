using Microsoft.Data.SqlClient;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;
using SGM.Infrastructure.DL.DALC.Persistence;
using System.Data;

namespace SGM.Infrastructure.DL.DALC.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IBDConexion _bd;

        public UsuarioRepository(IBDConexion bd)
        {
            _bd = bd;
        }

        public List<Usuario> Listar()
        {
            var lista = new List<Usuario>();

            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_LISTAR_USUARIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Usuario
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Username = dr["Username"].ToString() ?? string.Empty,
                    Password = dr["Password"].ToString() ?? string.Empty,
                    Rol = dr["Rol"].ToString() ?? string.Empty,
                    Estado = Convert.ToBoolean(dr["Estado"]),
                    Nombres = dr["Nombres"] as string,
                    Apellidos = dr["Apellidos"] as string
                });
            }

            return lista;
        }

        public Usuario? ObtenerPorId(int id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_OBTENER_USUARIO_POR_ID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            cn.Open();
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new Usuario
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Username = dr["Username"].ToString() ?? string.Empty,
                    Password = dr["Password"].ToString() ?? string.Empty,
                    Rol = dr["Rol"].ToString() ?? string.Empty,
                    Estado = Convert.ToBoolean(dr["Estado"]),
                    Nombres = dr["Nombres"] as string,
                    Apellidos = dr["Apellidos"] as string
                };
            }

            return null;
        }

        public Usuario? ObtenerPorUsername(string username)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_OBTENER_USUARIO_POR_USERNAME", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);

            cn.Open();
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new Usuario
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Username = dr["Username"].ToString() ?? string.Empty,
                    Password = dr["Password"].ToString() ?? string.Empty,
                    Rol = dr["Rol"].ToString() ?? string.Empty,
                    Estado = Convert.ToBoolean(dr["Estado"]),
                    Nombres = dr["Nombres"] as string,
                    Apellidos = dr["Apellidos"] as string
                };
            }

            return null;
        }

        public bool Registrar(Usuario usuario)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_REGISTRAR_USUARIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", usuario.Username);
            cmd.Parameters.AddWithValue("@Password", usuario.Password);
            cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
            cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
            cmd.Parameters.AddWithValue("@Nombres", (object?)usuario.Nombres ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Apellidos", (object?)usuario.Apellidos ?? DBNull.Value);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Actualizar(Usuario usuario)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ACTUALIZAR_USUARIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", usuario.Id);
            cmd.Parameters.AddWithValue("@Username", usuario.Username);
            cmd.Parameters.AddWithValue("@Password", usuario.Password);
            cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
            cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
            cmd.Parameters.AddWithValue("@Nombres", (object?)usuario.Nombres ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Apellidos", (object?)usuario.Apellidos ?? DBNull.Value);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ELIMINAR_USUARIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}