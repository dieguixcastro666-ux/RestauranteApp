using System.Data;
using Microsoft.Data.SqlClient;

namespace RestauranteApp.Datos
{
    public class ReporteDatos
    {
        // Pagos registrados en una fecha (un renglón por cobro)
        public DataTable VentasPorFecha(DateTime fecha)
        {
            string sql = @"SELECT pg.IdPedido AS Pedido,
                                  m.Numero AS Mesa,
                                  pg.MetodoPago AS Metodo,
                                  pg.Monto AS Monto,
                                  pg.Fecha AS Fecha
                           FROM Pagos pg
                           INNER JOIN Pedidos p ON pg.IdPedido = p.IdPedido
                           INNER JOIN Mesas m ON p.IdMesa = m.IdMesa
                           WHERE CAST(pg.Fecha AS DATE) = @fecha
                           ORDER BY pg.Fecha";
            var tabla = new DataTable();
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@fecha", fecha.Date);
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(tabla);
                }
            }
            return tabla;
        }

        // Suma de los pagos de una fecha (0 si no hubo ventas)
        public decimal TotalVentas(DateTime fecha)
        {
            string sql = @"SELECT ISNULL(SUM(Monto), 0)
                           FROM Pagos
                           WHERE CAST(Fecha AS DATE) = @fecha";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@fecha", fecha.Date);
                cn.Open();
                return (decimal)cmd.ExecuteScalar();
            }
        }

        // Productos más vendidos, solo de pedidos ya pagados
        public DataTable ProductosMasVendidos()
        {
            string sql = @"SELECT pr.Nombre AS Producto,
                                  SUM(d.Cantidad) AS Cantidad,
                                  SUM(d.Subtotal) AS Total
                           FROM DetallePedido d
                           INNER JOIN Pedidos p ON d.IdPedido = p.IdPedido
                           INNER JOIN Productos pr ON d.IdProducto = pr.IdProducto
                           WHERE p.Estado = 'Pagado'
                           GROUP BY pr.Nombre
                           ORDER BY SUM(d.Cantidad) DESC";
            var tabla = new DataTable();
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                da.Fill(tabla);
            }
            return tabla;
        }
    }
}