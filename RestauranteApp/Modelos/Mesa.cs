namespace RestauranteApp.Modelos
{
    public class Mesa
    {
        private int _idMesa;
        private int _numero;
        private string _estado = "Libre";

        public int IdMesa
        {
            get { return _idMesa; }
            set { _idMesa = value; }
        }

        public int Numero
        {
            get { return _numero; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El número de mesa debe ser mayor que cero.");
                _numero = value;
            }
        }

        public string Estado
        {
            get { return _estado; }
            set
            {
                if (value != "Libre" && value != "Ocupada")
                    throw new ArgumentException("El estado debe ser Libre u Ocupada.");
                _estado = value;
            }
        }

        public bool EstaLibre
        {
            get { return _estado == "Libre"; }
        }

        public void Ocupar()
        {
            _estado = "Ocupada";
        }

        public void Liberar()
        {
            _estado = "Libre";
        }

        public Mesa() { }

        public Mesa(int idMesa, int numero, string estado)
        {
            IdMesa = idMesa;
            Numero = numero;
            Estado = estado;
        }

        public override string ToString()
        {
            return "Mesa " + Numero;
        }
    }
}