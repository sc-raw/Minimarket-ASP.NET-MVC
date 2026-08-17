using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SGM.Infrastructure.DL.DALC.Persistence
{
    public class BDConexion : IBDConexion
    {
        private readonly string _cadenaConexion;

        public BDConexion(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'");
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}
