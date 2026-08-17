using Microsoft.Data.SqlClient;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;
using SGM.Infrastructure.DL.DALC.Persistence;
using System.Data;

namespace SGM.Infrastructure.DL.DALC.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IBDConexion _bd;

        public ClienteRepository(IBDConexion bd)
        {
            _bd = bd;
        }

        public List<Cliente> Listar()
        {
            var lista = new List<Cliente>();

            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_LISTAR_CLIENTE", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Cliente
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Dni = dr["Dni"].ToString() ?? string.Empty,
                    Nombres = dr["Nombres"].ToString() ?? string.Empty,
                    Apellidos = dr["Apellidos"].ToString() ?? string.Empty,
                    Direccion = dr["Direccion"] as string,
                    Telefono = dr["Telefono"] as string,
                    Correo = dr["Correo"] as string,
                    Estado = Convert.ToBoolean(dr["Estado"]),
                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                });
            }

            return lista;
        }

        public Cliente? ObtenerPorId(int id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_OBTENER_CLIENTE_POR_ID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            cn.Open();
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new Cliente
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Dni = dr["Dni"].ToString() ?? string.Empty,
                    Nombres = dr["Nombres"].ToString() ?? string.Empty,
                    Apellidos = dr["Apellidos"].ToString() ?? string.Empty,
                    Direccion = dr["Direccion"] as string,
                    Telefono = dr["Telefono"] as string,
                    Correo = dr["Correo"] as string,
                    Estado = Convert.ToBoolean(dr["Estado"]),
                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                };
            }

            return null;
        }

        public bool Registrar(Cliente cliente)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_REGISTRAR_CLIENTE", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Dni", cliente.Dni);
            cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
            cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
            cmd.Parameters.AddWithValue("@Direccion", (object?)cliente.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Correo", (object?)cliente.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Estado", cliente.Estado);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Actualizar(Cliente cliente)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ACTUALIZAR_CLIENTE", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", cliente.Id);
            cmd.Parameters.AddWithValue("@Dni", cliente.Dni);
            cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
            cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
            cmd.Parameters.AddWithValue("@Direccion", (object?)cliente.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Correo", (object?)cliente.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Estado", cliente.Estado);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_ELIMINAR_CLIENTE", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}