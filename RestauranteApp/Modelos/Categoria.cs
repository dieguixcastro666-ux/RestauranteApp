namespace RestauranteApp.Modelos
{
    public class Categoria
    {
        private int _idCategoria;
        private string _nombre = "";

        public int IdCategoria
        {
            get { return _idCategoria; }
            set { _idCategoria = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre de la categoría es obligatorio.");
                _nombre = value.Trim();
            }
        }

        public Categoria() { }

        public Categoria(int idCategoria, string nombre)
        {
            IdCategoria = idCategoria;
            Nombre = nombre;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}