using Microsoft.Data.SqlClient;
using RestauranteApp.Datos;
using RestauranteApp.Modelos;

namespace RestauranteApp.Formularios
{
    public partial class FrmFacturacion : Form
    {
        private readonly MesaDatos _mesaDatos = new MesaDatos();
        private readonly PedidoDatos _pedidoDatos = new PedidoDatos();
        private readonly PagoDatos _pagoDatos = new PagoDatos();

        // Pedido abierto de la mesa elegida (null si no hay mesa elegida)
        private Pedido? _pedido = null;

        public FrmFacturacion()
        {
            InitializeComponent();
            Load += FrmFacturacion_Load;
            cmbMetodo.SelectedIndexChanged += cmbMetodo_SelectedIndexChanged;
            txtRecibido.TextChanged += txtRecibido_TextChanged;
            btnCobrar.Click += btnCobrar_Click;
        }

        private void FrmFacturacion_Load(object? sender, EventArgs e)
        {
            cmbMetodo.Items.Add("Efectivo");
            cmbMetodo.Items.Add("Tarjeta");
            cmbMetodo.SelectedIndex = 0;

            CargarMesasOcupadas();
            MostrarPedido();
        }

        // Solo las mesas ocupadas pueden cobrarse
        private void CargarMesasOcupadas()
        {
            cmbMesa.SelectedIndexChanged -= cmbMesa_SelectedIndexChanged;
            cmbMesa.DataSource = _mesaDatos.ObtenerTodas().Where(m => !m.EstaLibre).ToList();
            cmbMesa.SelectedIndex = -1;
            cmbMesa.SelectedIndexChanged += cmbMesa_SelectedIndexChanged;
        }

        private void cmbMesa_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var mesa = cmbMesa.SelectedItem as Mesa;
            if (mesa == null) return;

            _pedido = _pedidoDatos.ObtenerAbiertoPorMesa(mesa.IdMesa);
            MostrarPedido();
        }

        private void MostrarPedido()
        {
            dgvDetalle.DataSource = null;
            if (_pedido != null)
                dgvDetalle.DataSource = _pedido.Detalles.ToList();

            if (dgvDetalle.Columns.Count > 0)
            {
                dgvDetalle.Columns["IdDetalle"].Visible = false;
                dgvDetalle.Columns["IdPedido"].Visible = false;
                dgvDetalle.Columns["IdProducto"].Visible = false;
                dgvDetalle.Columns["NombreProducto"].HeaderText = "Producto";
                dgvDetalle.Columns["PrecioUnitario"].HeaderText = "Precio";
                dgvDetalle.Columns["PrecioUnitario"].DefaultCellStyle.Format = "$0.00";
                dgvDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "$0.00";
                dgvDetalle.ClearSelection();
            }

            decimal total = _pedido != null ? _pedido.CalcularTotal() : 0;
            lblTotal.Text = "Total: " + total.ToString("$0.00");
            ActualizarCambio();
        }

        private void cmbMetodo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // El efectivo recibido solo se usa al pagar en efectivo
            bool esEfectivo = cmbMetodo.SelectedItem?.ToString() == "Efectivo";
            txtRecibido.Enabled = esEfectivo;
            if (!esEfectivo) txtRecibido.Clear();
            ActualizarCambio();
        }

        private void txtRecibido_TextChanged(object? sender, EventArgs e)
        {
            ActualizarCambio();
        }

        private void ActualizarCambio()
        {
            if (_pedido == null || cmbMetodo.SelectedItem?.ToString() != "Efectivo")
            {
                lblCambio.Text = "Cambio: $0.00";
                return;
            }

            decimal total = _pedido.CalcularTotal();
            if (!decimal.TryParse(txtRecibido.Text, out decimal recibido))
            {
                lblCambio.Text = "Cambio: $0.00";
                return;
            }

            if (recibido < total)
                lblCambio.Text = "Falta: " + (total - recibido).ToString("$0.00");
            else
                lblCambio.Text = "Cambio: " + (recibido - total).ToString("$0.00");
        }

        private void btnCobrar_Click(object? sender, EventArgs e)
        {
            if (_pedido == null)
            {
                MessageBox.Show("Selecciona una mesa con pedido abierto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string metodo = cmbMetodo.SelectedItem?.ToString() ?? "Efectivo";
            decimal total = _pedido.CalcularTotal();
            decimal recibido = total;

            if (metodo == "Efectivo")
            {
                if (!decimal.TryParse(txtRecibido.Text, out recibido) || recibido < total)
                {
                    MessageBox.Show("El efectivo recibido debe ser un número igual o mayor al total.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                _pagoDatos.Cobrar(_pedido.IdPedido, _pedido.IdMesa, metodo, total);

                MessageBox.Show(ArmarRecibo(_pedido, metodo, total, recibido), "Recibo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _pedido = null;
                txtRecibido.Clear();
                CargarMesasOcupadas();
                MostrarPedido();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ArmarRecibo(Pedido pedido, string metodo, decimal total, decimal recibido)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("RESTAURANTE");
            sb.AppendLine("Recibo de pago");
            sb.AppendLine("Pedido N.º " + pedido.IdPedido + "  -  Mesa (id " + pedido.IdMesa + ")");
            sb.AppendLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            sb.AppendLine("------------------------------");
            foreach (DetallePedido d in pedido.Detalles)
            {
                sb.AppendLine(d.Cantidad + " x " + d.NombreProducto + "  " + d.Subtotal.ToString("$0.00"));
            }
            sb.AppendLine("------------------------------");
            sb.AppendLine("TOTAL: " + total.ToString("$0.00"));
            sb.AppendLine("Método de pago: " + metodo);
            if (metodo == "Efectivo")
            {
                sb.AppendLine("Recibido: " + recibido.ToString("$0.00"));
                sb.AppendLine("Cambio: " + (recibido - total).ToString("$0.00"));
            }
            sb.AppendLine("¡Gracias por su visita!");
            return sb.ToString();
        }
    }
}