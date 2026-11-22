namespace RestauranteApp.Modelos
{
    public class Producto
    {
        private int _idProducto;
        private string _nombre = "";
        private decimal _precio;
        private int _idCategoria;

        public int IdProducto
        {
            get { return _idProducto; }
            set { _idProducto = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre del producto es obligatorio.");
                _nombre = value.Trim();
            }
        }

        public decimal Precio
        {
            get { return _precio; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El precio debe ser mayor que cero.");
                _precio = value;
            }
        }

        public int IdCategoria
        {
            get { return _idCategoria; }
            set { _idCategoria = value; }
        }

        // Solo para mostrar en tablas (viene de la unión con Categorias)
        public string NombreCategoria { get; set; } = "";

        public Producto() { }

        public Producto(int idProducto, string nombre, decimal precio, int idCategoria)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Precio = precio;
            IdCategoria = idCategoria;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}