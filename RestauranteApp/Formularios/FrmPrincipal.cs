namespace RestauranteApp.Formularios
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            btnPedidos.Click += btnPedidos_Click;
            btnCobro.Click += btnCobro_Click;
            btnMesas.Click += btnMesas_Click;
            btnProductos.Click += btnProductos_Click;
            btnCategorias.Click += btnCategorias_Click;
            btnReportes.Click += btnReportes_Click;
            btnSalir.Click += btnSalir_Click;
        }

        // Cada botón abre su formulario de forma modal: el principal queda
        // en espera hasta que el usuario cierre la ventana abierta.
        private void AbrirFormulario(Form formulario)
        {
            using (formulario)
            {
                formulario.ShowDialog(this);
            }
        }

        private void btnPedidos_Click(object? sender, EventArgs e)
        {
            AbrirFormulario(new FrmPedidos());
        }

        private void btnCobro_Click(object? sender, EventArgs e)
        {
            AbrirFormulario(new FrmFacturacion());
        }

        private void btnMesas_Click(object? sender, EventArgs e)
        {
            AbrirFormulario(new FrmMesas());
        }

        private void btnProductos_Click(object? sender, EventArgs e)
        {
            AbrirFormulario(new FrmProductos());
        }

        private void btnCategorias_Click(object? sender, EventArgs e)
        {
            AbrirFormulario(new FrmCategorias());
        }

        private void btnReportes_Click(object? sender, EventArgs e)
        {
            AbrirFormulario(new FrmReportes());
        }

        private void btnSalir_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}