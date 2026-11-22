using Microsoft.Data.SqlClient;

namespace RestauranteApp.Datos
{
    public static class Conexion
    {
        private const string CadenaConexion =
            "Server=localhost\\SQLEXPRESS;Database=RestauranteDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public static bool ProbarConexion(out string mensaje)
        {
            try
            {
                using (SqlConnection cn = ObtenerConexion())
                {
                    cn.Open();
                    mensaje = "Conexión exitosa a " + cn.Database;
                    return true;
                }
            }
            catch (SqlException ex)
            {
                mensaje = "No se pudo conectar: " + ex.Message;
                return false;
            }
        }
    }
}