using System.Data.SqlClient;

public class Conexion
{
    private static string cadena =
        @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=CLINICA; Integrated Security=True";

    public static SqlConnection ObtenerConexion()
    {
        return new SqlConnection(cadena);
    }
}
