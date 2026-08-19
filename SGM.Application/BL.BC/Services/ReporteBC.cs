using Microsoft.Data.SqlClient;
using SGM.Application.BL.BE;
using SGM.Infrastructure.DL.DALC.Persistence;
using System.Data;

namespace SGM.Application.BL.BC.Service
{
    public class ReporteBC : IReporteService
    {
        private readonly IBDConexion _bd;

        public ReporteBC(IBDConexion bd)
        {
            _bd = bd;
        }

        public ReporteResumen ObtenerResumen()
        {
            var resumen = new ReporteResumen();

            using var cn = _bd.ObtenerConexion();
            using var cmd = new SqlCommand("SP_REPORTE_RESUMEN", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                resumen.TotalClientes = Convert.ToInt32(dr["TotalClientes"]);
                resumen.TotalVentas = Convert.ToInt32(dr["TotalVentas"]);
                resumen.TotalCategorias = Convert.ToInt32(dr["TotalCategorias"]);
                resumen.TotalProductos = Convert.ToInt32(dr["TotalProductos"]);
                resumen.TotalEmpleados = Convert.ToInt32(dr["TotalEmpleados"]);
                resumen.MontoTotalVendido = Convert.ToDecimal(dr["MontoTotalVendido"]);
            }

            return resumen;
        }
    }
}