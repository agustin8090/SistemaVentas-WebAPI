using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace proyecto_final.Negocio
{
    public class Informes
    {
        public (int cant_presentes, int cant_ausentes, int cant_total,
         double porc_presentes, double porc_ausentes)
        obtener_asistencia()
        {
            int presentes = 0, ausentes = 0, total = 0;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT 
            SUM(CASE WHEN Estado = 'PRESENTE' THEN 1 ELSE 0 END) AS Presentes,
            SUM(CASE WHEN Estado = 'AUSENTE' THEN 1 ELSE 0 END) AS Ausentes,
            COUNT(*) AS Total
            FROM Turno";

                SqlCommand comando = new SqlCommand(query, conexion);

                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    presentes = lector["Presentes"] != DBNull.Value ? Convert.ToInt32(lector["Presentes"]) : 0;
                    ausentes = lector["Ausentes"] != DBNull.Value ? Convert.ToInt32(lector["Ausentes"]) : 0;
                    total = lector["Total"] != DBNull.Value ? Convert.ToInt32(lector["Total"]) : 0;
                }
            }

            double porc_pres = 0, porc_aus = 0;

            if (total > 0)
            {
                porc_pres = (presentes * 100.0) / total;
                porc_aus = (ausentes * 100.0) / total;
            }

            return (presentes, ausentes, total, porc_pres, porc_aus);
        }
    }
}
    
