using Microsoft.Data.SqlClient;
using RestauranteApp.Modelos;

namespace RestauranteApp.Datos
{
    public class PedidoDatos
    {
        // Devuelve el pedido abierto de una mesa (o null si no tiene)
        public Pedido? ObtenerAbiertoPorMesa(int idMesa)
        {
            Pedido? pedido = null;
            using (var cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sqlPedido = @"SELECT TOP 1 IdPedido, IdMesa, Fecha
                                     FROM Pedidos
                                     WHERE IdMesa = @mesa AND Estado = 'Abierto'
                                     ORDER BY IdPedido DESC";
                using (var cmd = new SqlCommand(sqlPedido, cn))
                {
                    cmd.Parameters.AddWithValue("@mesa", idMesa);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            pedido = new Pedido();
                            pedido.IdPedido = dr.GetInt32(0);
                            pedido.IdMesa = dr.GetInt32(1);
                            pedido.Fecha = dr.GetDateTime(2);
                        }
                    }
                }

                if (pedido == null) return null;

                string sqlDetalle = @"SELECT d.IdProducto, p.Nombre, d.Cantidad, d.PrecioUnitario
                                      FROM DetallePedido d
                                      INNER JOIN Productos p ON d.IdProducto = p.IdProducto
                                      WHERE d.IdPedido = @id";
                using (var cmd = new SqlCommand(sqlDetalle, cn))
                {
                    cmd.Parameters.AddWithValue("@id", pedido.IdPedido);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            pedido.Detalles.Add(new DetallePedido(
                                dr.GetInt32(0), dr.GetString(1),
                                dr.GetInt32(2), dr.GetDecimal(3)));
                        }
                    }
                }
            }
            return pedido;
        }

        // Guarda un pedido nuevo o actualiza uno abierto, y marca la mesa como Ocupada
        public void Guardar(Pedido p)
        {
            using (var cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        int idPedido = p.IdPedido;
                        decimal total = p.CalcularTotal();

                        if (idPedido == 0)
                        {
                            string sqlNuevo = @"INSERT INTO Pedidos (IdMesa, Fecha, Estado, Total)
                                                OUTPUT INSERTED.IdPedido
                                                VALUES (@mesa, @fecha, 'Abierto', @total)";
                            using (var cmd = new SqlCommand(sqlNuevo, cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@mesa", p.IdMesa);
                                cmd.Parameters.AddWithValue("@fecha", p.Fecha);
                                cmd.Parameters.AddWithValue("@total", total);
                                idPedido = (int)cmd.ExecuteScalar();
                            }
                        }
                        else
                        {
                            string sqlAct = "UPDATE Pedidos SET Total = @total WHERE IdPedido = @id";
                            using (var cmd = new SqlCommand(sqlAct, cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@total", total);
                                cmd.Parameters.AddWithValue("@id", idPedido);
                                cmd.ExecuteNonQuery();
                            }
                            string sqlBorrar = "DELETE FROM DetallePedido WHERE IdPedido = @id";
                            using (var cmd = new SqlCommand(sqlBorrar, cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@id", idPedido);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        string sqlDet = @"INSERT INTO DetallePedido (IdPedido, IdProducto, Cantidad, PrecioUnitario)
                                          VALUES (@pedido, @producto, @cantidad, @precio)";
                        foreach (DetallePedido d in p.Detalles)
                        {
                            using (var cmd = new SqlCommand(sqlDet, cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@pedido", idPedido);
                                cmd.Parameters.AddWithValue("@producto", d.IdProducto);
                                cmd.Parameters.AddWithValue("@cantidad", d.Cantidad);
                                cmd.Parameters.AddWithValue("@precio", d.PrecioUnitario);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        string sqlMesa = "UPDATE Mesas SET Estado = 'Ocupada' WHERE IdMesa = @mesa";
                        using (var cmd = new SqlCommand(sqlMesa, cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@mesa", p.IdMesa);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        p.IdPedido = idPedido;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}