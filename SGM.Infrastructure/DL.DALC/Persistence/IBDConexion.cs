using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace SGM.Infrastructure.DL.DALC.Persistence
{
    public interface IBDConexion
    {
        SqlConnection ObtenerConexion();
    }
}
