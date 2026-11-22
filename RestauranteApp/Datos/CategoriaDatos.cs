using Microsoft.Data.SqlClient;
using RestauranteApp.Modelos;

namespace RestauranteApp.Datos
{
    public class CategoriaDatos
    {
        public List<Categoria> ObtenerTodas()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = "SELECT IdCategoria, Nombre FROM Categorias ORDER BY Nombre";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Categoria(dr.GetInt32(0), dr.GetString(1)));
                    }
                }
            }

            return lista;
        }

        public void Insertar(Categoria c)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = "INSERT INTO Categorias (Nombre) VALUES (@nombre)";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Categoria c)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = "UPDATE Categorias SET Nombre = @nombre WHERE IdCategoria = @id";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@id", c.IdCategoria);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int idCategoria)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = "DELETE FROM Categorias WHERE IdCategoria = @id";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", idCategoria);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Evita nombres repetidos. Al editar, se excluye la propia categoría.
        public bool ExisteNombre(string nombre, int idExcluir)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = "SELECT COUNT(*) FROM Categorias WHERE Nombre = @nombre AND IdCategoria <> @id";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@id", idExcluir);
                cn.Open();
                int cantidad = (int)cmd.ExecuteScalar();
                return cantidad > 0;
            }
        }

        // No se puede borrar una categoría que tiene productos
        public bool TieneProductos(int idCategoria)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = "SELECT COUNT(*) FROM Productos WHERE IdCategoria = @id";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", idCategoria);
                cn.Open();
                int cantidad = (int)cmd.ExecuteScalar();
                return cantidad > 0;
            }
        }
    }
}