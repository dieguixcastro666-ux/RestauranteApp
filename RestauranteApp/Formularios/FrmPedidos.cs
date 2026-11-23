using Microsoft.Data.SqlClient;
using RestauranteApp.Datos;
using RestauranteApp.Modelos;

namespace RestauranteApp.Formularios
{
    public partial class FrmPedidos : Form
    {
        private readonly MesaDatos _mesaDatos = new MesaDatos();
        private readonly ProductoDatos _productoDatos = new ProductoDatos();
        private readonly PedidoDatos _pedidoDatos = new PedidoDatos();

        // Pedido que se está armando en pantalla (todavía en memoria)
        private Pedido _pedido = new Pedido();

        public FrmPedidos()
        {
            InitializeComponent();
            Load += FrmPedidos_Load;
            btnAgregar.Click += btnAgregar_Click;
            btnQuitar.Click += btnQuitar_Click;
            btnGuardar.Click += btnGuardar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
        }

        private void FrmPedidos_Load(object? sender, EventArgs e)
        {
            CargarMesas();

            cmbProducto.DisplayMember = "Nombre";
            cmbProducto.DataSource = _productoDatos.ObtenerTodos();
            cmbProducto.SelectedIndex = -1;

            nudCantidad.Value = 1;
            MostrarPedido();
        }

        private void CargarMesas()
        {
            // Se desconecta el evento para que no se dispare al recargar la lista
            cmbMesa.SelectedIndexChanged -= cmbMesa_SelectedIndexChanged;
            cmbMesa.DataSource = _mesaDatos.ObtenerTodas();
            cmbMesa.SelectedIndex = -1;
            cmbMesa.SelectedIndexChanged += cmbMesa_SelectedIndexChanged;
        }

        // Al elegir una mesa: si tiene un pedido abierto, se carga; si no, se empieza uno nuevo
        private void cmbMesa_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var mesa = cmbMesa.SelectedItem as Mesa;
            if (mesa == null) return;

            var abierto = _pedidoDatos.ObtenerAbiertoPorMesa(mesa.IdMesa);
            if (abierto != null)
            {
                _pedido = abierto;
            }
            else
            {
                _pedido = new Pedido();
                _pedido.IdMesa = mesa.IdMesa;
            }
            MostrarPedido();
        }

        private void MostrarPedido()
        {
            dgvDetalle.DataSource = null;
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
            }
            dgvDetalle.ClearSelection();

            lblTotal.Text = "Total: " + _pedido.CalcularTotal().ToString("$0.00");
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            var mesa = cmbMesa.SelectedItem as Mesa;
            if (mesa == null)
            {
                MessageBox.Show("Selecciona una mesa.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var producto = cmbProducto.SelectedItem as Producto;
            if (producto == null)
            {
                MessageBox.Show("Selecciona un producto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _pedido.IdMesa = mesa.IdMesa;
                _pedido.AgregarProducto(producto, (int)nudCantidad.Value);
                nudCantidad.Value = 1;
                MostrarPedido();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuitar_Click(object? sender, EventArgs e)
        {
            if (dgvDetalle.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un producto de la tabla.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var detalle = dgvDetalle.SelectedRows[0].DataBoundItem as DetallePedido;
            if (detalle == null) return;

            _pedido.QuitarProducto(detalle.IdProducto);
            MostrarPedido();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (cmbMesa.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una mesa.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_pedido.Detalles.Count == 0)
            {
                MessageBox.Show("Agrega al menos un producto al pedido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _pedidoDatos.Guardar(_pedido);
                MessageBox.Show("Pedido guardado. " + lblTotal.Text, "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarTodo();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarTodo();
        }

        private void LimpiarTodo()
        {
            _pedido = new Pedido();
            CargarMesas();
            cmbProducto.SelectedIndex = -1;
            nudCantidad.Value = 1;
            MostrarPedido();
        }
    }
}