using Microsoft.Data.SqlClient;
using RestauranteApp.Modelos;

namespace RestauranteApp.Datos
{
    public class ProductoDatos
    {
        public List<Producto> ObtenerTodos()
        {
            var lista = new List<Producto>();
            string sql = @"SELECT p.IdProducto, p.Nombre, p.Precio,
                                  p.IdCategoria, c.Nombre AS NombreCategoria
                           FROM Productos p
                           INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
                           ORDER BY p.Nombre";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var p = new Producto();
                        p.IdProducto = dr.GetInt32(0);
                        p.Nombre = dr.GetString(1);
                        p.Precio = dr.GetDecimal(2);
                        p.IdCategoria = dr.GetInt32(3);
                        p.NombreCategoria = dr.GetString(4);
                        lista.Add(p);
                    }
                }
            }
            return lista;
        }

        public void Insertar(Producto p)
        {
            string sql = @"INSERT INTO Productos (Nombre, Precio, IdCategoria)
                           VALUES (@nombre, @precio, @idCategoria)";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@precio", p.Precio);
                cmd.Parameters.AddWithValue("@idCategoria", p.IdCategoria);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Producto p)
        {
            string sql = @"UPDATE Productos
                           SET Nombre = @nombre, Precio = @precio, IdCategoria = @idCategoria
                           WHERE IdProducto = @id";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@precio", p.Precio);
                cmd.Parameters.AddWithValue("@idCategoria", p.IdCategoria);
                cmd.Parameters.AddWithValue("@id", p.IdProducto);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int idProducto)
        {
            string sql = "DELETE FROM Productos WHERE IdProducto = @id";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", idProducto);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool ExisteNombre(string nombre, int idExcluir)
        {
            string sql = @"SELECT COUNT(*) FROM Productos
                           WHERE Nombre = @nombre AND IdProducto <> @id";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@id", idExcluir);
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool TieneVentas(int idProducto)
        {
            string sql = "SELECT COUNT(*) FROM DetallePedido WHERE IdProducto = @id";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", idProducto);
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }
}