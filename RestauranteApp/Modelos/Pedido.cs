namespace RestauranteApp.Modelos
{
    public class Pedido
    {
        private int _idPedido;
        private int _idMesa;
        private DateTime _fecha = DateTime.Now;
        private string _estado = "Abierto";
        private List<DetallePedido> _detalles = new List<DetallePedido>();

        public int IdPedido
        {
            get { return _idPedido; }
            set { _idPedido = value; }
        }

        public int IdMesa
        {
            get { return _idMesa; }
            set { _idMesa = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set
            {
                if (value != "Abierto" && value != "Pagado")
                    throw new ArgumentException("El estado debe ser Abierto o Pagado.");
                _estado = value;
            }
        }

        public List<DetallePedido> Detalles
        {
            get { return _detalles; }
        }

        public void AgregarProducto(Producto producto, int cantidad)
        {
            if (producto == null)
                throw new ArgumentException("El producto no existe.");
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            // Si el producto ya está en el pedido, solo suma la cantidad
            foreach (DetallePedido d in _detalles)
            {
                if (d.IdProducto == producto.IdProducto)
                {
                    d.Cantidad += cantidad;
                    return;
                }
            }

            _detalles.Add(new DetallePedido(producto.IdProducto, producto.Nombre, cantidad, producto.Precio));
        }

        public void QuitarProducto(int idProducto)
        {
            for (int i = 0; i < _detalles.Count; i++)
            {
                if (_detalles[i].IdProducto == idProducto)
                {
                    _detalles.RemoveAt(i);
                    return;
                }
            }
        }

        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (DetallePedido d in _detalles)
            {
                total += d.Subtotal;
            }
            return total;
        }
    }
}