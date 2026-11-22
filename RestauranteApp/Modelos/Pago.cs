namespace RestauranteApp.Modelos
{
    public class Pago
    {
        private int _idPago;
        private int _idPedido;
        private string _metodoPago = "Efectivo";
        private decimal _monto;
        private DateTime _fecha = DateTime.Now;

        public int IdPago
        {
            get { return _idPago; }
            set { _idPago = value; }
        }

        public int IdPedido
        {
            get { return _idPedido; }
            set { _idPedido = value; }
        }

        public string MetodoPago
        {
            get { return _metodoPago; }
            set
            {
                if (value != "Efectivo" && value != "Tarjeta")
                    throw new ArgumentException("El método de pago debe ser Efectivo o Tarjeta.");
                _metodoPago = value;
            }
        }

        public decimal Monto
        {
            get { return _monto; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El monto debe ser mayor que cero.");
                _monto = value;
            }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
    }
}