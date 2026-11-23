using Microsoft.Data.SqlClient;
using RestauranteApp.Datos;
using RestauranteApp.Modelos;

namespace RestauranteApp.Formularios
{
    public partial class FrmProductos : Form
    {
        private readonly ProductoDatos _datos = new ProductoDatos();
        private readonly CategoriaDatos _categoriaDatos = new CategoriaDatos();
        private int _idSeleccionado = 0;

        public FrmProductos()
        {
            InitializeComponent();
            Load += FrmProductos_Load;
            btnGuardar.Click += btnGuardar_Click;
            btnNuevo.Click += btnNuevo_Click;
            btnEliminar.Click += btnEliminar_Click;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
        }

        private void FrmProductos_Load(object? sender, EventArgs e)
        {
            CargarCategorias();
            CargarProductos();
            LimpiarFormulario();
        }

        private void CargarCategorias()
        {
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "IdCategoria";
            cmbCategoria.DataSource = _categoriaDatos.ObtenerTodas();
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = _datos.ObtenerTodos();
            dgvProductos.Columns["IdProducto"].Visible = false;
            dgvProductos.Columns["IdCategoria"].Visible = false;
            dgvProductos.Columns["NombreCategoria"].HeaderText = "Categoría";
            dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "$0.00";
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            txtNombre.Clear();
            txtPrecio.Clear();
            cmbCategoria.SelectedIndex = -1;
            dgvProductos.ClearSelection();
            txtNombre.Focus();
        }

        private void dgvProductos_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;
            var p = dgvProductos.SelectedRows[0].DataBoundItem as Producto;
            if (p == null) return;

            _idSeleccionado = p.IdProducto;
            txtNombre.Text = p.Nombre;
            txtPrecio.Text = p.Precio.ToString("0.00");
            cmbCategoria.SelectedValue = p.IdCategoria;
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();

            if (nombre == "")
            {
                MessageBox.Show("Escribe el nombre del producto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("El precio debe ser un número mayor que cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbCategoria.SelectedValue == null)
            {
                MessageBox.Show("Selecciona una categoría.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_datos.ExisteNombre(nombre, _idSeleccionado))
            {
                MessageBox.Show("Ya existe un producto con ese nombre.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var p = new Producto();
                p.IdProducto = _idSeleccionado;
                p.Nombre = nombre;
                p.Precio = precio;
                p.IdCategoria = (int)cmbCategoria.SelectedValue;

                if (_idSeleccionado == 0)
                    _datos.Insertar(p);
                else
                    _datos.Actualizar(p);

                CargarProductos();
                LimpiarFormulario();
                MessageBox.Show("Producto guardado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Selecciona un producto de la tabla.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_datos.TieneVentas(_idSeleccionado))
            {
                MessageBox.Show("No se puede eliminar: el producto ya está en pedidos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var r = MessageBox.Show("¿Eliminar el producto seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            try
            {
                _datos.Eliminar(_idSeleccionado);
                CargarProductos();
                LimpiarFormulario();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}