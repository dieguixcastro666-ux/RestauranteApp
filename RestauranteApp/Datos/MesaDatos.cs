using Microsoft.Data.SqlClient;
using RestauranteApp.Modelos;

namespace RestauranteApp.Datos
{
    public class MesaDatos
    {
        public List<Mesa> ObtenerTodas()
        {
            var lista = new List<Mesa>();
            string sql = "SELECT IdMesa, Numero, Estado FROM Mesas ORDER BY Numero";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var m = new Mesa();
                        m.IdMesa = dr.GetInt32(0);
                        m.Numero = dr.GetInt32(1);
                        m.Estado = dr.GetString(2);
                        lista.Add(m);
                    }
                }
            }
            return lista;
        }

        public void Insertar(Mesa m)
        {
            string sql = "INSERT INTO Mesas (Numero, Estado) VALUES (@numero, 'Libre')";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@numero", m.Numero);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CambiarEstado(int idMesa, string estado)
        {
            string sql = "UPDATE Mesas SET Estado = @estado WHERE IdMesa = @id";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@id", idMesa);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int idMesa)
        {
            string sql = "DELETE FROM Mesas WHERE IdMesa = @id";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", idMesa);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool ExisteNumero(int numero)
        {
            string sql = "SELECT COUNT(*) FROM Mesas WHERE Numero = @numero";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@numero", numero);
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool TienePedidos(int idMesa)
        {
            string sql = "SELECT COUNT(*) FROM Pedidos WHERE IdMesa = @id";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", idMesa);
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }
}