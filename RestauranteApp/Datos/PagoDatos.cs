using Microsoft.Data.SqlClient;

namespace RestauranteApp.Datos
{
    public class PagoDatos
    {
        // Registra el pago, marca el pedido como Pagado y libera la mesa
        public void Cobrar(int idPedido, int idMesa, string metodoPago, decimal monto)
        {
            using (var cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        string sqlPago = @"INSERT INTO Pagos (IdPedido, MetodoPago, Monto)
                                           VALUES (@pedido, @metodo, @monto)";
                        using (var cmd = new SqlCommand(sqlPago, cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@pedido", idPedido);
                            cmd.Parameters.AddWithValue("@metodo", metodoPago);
                            cmd.Parameters.AddWithValue("@monto", monto);
                            cmd.ExecuteNonQuery();
                        }

                        string sqlPedido = "UPDATE Pedidos SET Estado = 'Pagado' WHERE IdPedido = @pedido";
                        using (var cmd = new SqlCommand(sqlPedido, cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@pedido", idPedido);
                            cmd.ExecuteNonQuery();
                        }

                        string sqlMesa = "UPDATE Mesas SET Estado = 'Libre' WHERE IdMesa = @mesa";
                        using (var cmd = new SqlCommand(sqlMesa, cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@mesa", idMesa);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
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