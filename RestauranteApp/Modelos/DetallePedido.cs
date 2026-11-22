namespace RestauranteApp.Modelos
{
    public class DetallePedido
    {
        private int _idDetalle;
        private int _idPedido;
        private int _idProducto;
        private int _cantidad;
        private decimal _precioUnitario;

        public int IdDetalle
        {
            get { return _idDetalle; }
            set { _idDetalle = value; }
        }

        public int IdPedido
        {
            get { return _idPedido; }
            set { _idPedido = value; }
        }

        public int IdProducto
        {
            get { return _idProducto; }
            set { _idProducto = value; }
        }

        // Solo para mostrar en tablas
        public string NombreProducto { get; set; } = "";

        public int Cantidad
        {
            get { return _cantidad; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La cantidad debe ser mayor que cero.");
                _cantidad = value;
            }
        }

        public decimal PrecioUnitario
        {
            get { return _precioUnitario; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El precio debe ser mayor que cero.");
                _precioUnitario = value;
            }
        }

        // Se calcula solo: no se puede asignar desde fuera
        public decimal Subtotal
        {
            get { return _cantidad * _precioUnitario; }
        }

        public DetallePedido() { }

        public DetallePedido(int idProducto, string nombreProducto, int cantidad, decimal precioUnitario)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}