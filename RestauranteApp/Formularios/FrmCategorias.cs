using Microsoft.Data.SqlClient;
using RestauranteApp.Datos;
using RestauranteApp.Modelos;

namespace RestauranteApp.Formularios
{
    public partial class FrmCategorias : Form
    {
        private CategoriaDatos _datos = new CategoriaDatos();
        private int _idSeleccionado = 0;

        public FrmCategorias()
        {
            InitializeComponent();

            // Conectamos los eventos de los controles con sus métodos
            Load += FrmCategorias_Load;
            btnGuardar.Click += btnGuardar_Click;
            btnNuevo.Click += btnNuevo_Click;
            btnEliminar.Click += btnEliminar_Click;
            dgvCategorias.SelectionChanged += dgvCategorias_SelectionChanged;
        }

        private void FrmCategorias_Load(object? sender, EventArgs e)
        {
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            try
            {
                dgvCategorias.DataSource = _datos.ObtenerTodas();
                dgvCategorias.Columns["IdCategoria"].HeaderText = "Código";
                LimpiarFormulario();
            }
            catch (SqlException)
            {
                MessageBox.Show("No se pudo conectar con la base de datos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            txtNombre.Clear();
            dgvCategorias.ClearSelection();
            txtNombre.Focus();
        }

        private void dgvCategorias_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0)
                return;

            Categoria? c = dgvCategorias.SelectedRows[0].DataBoundItem as Categoria;
            if (c == null)
                return;

            _idSeleccionado = c.IdCategoria;
            txtNombre.Text = c.Nombre;
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();

                if (nombre == "")
                {
                    MessageBox.Show("Escriba el nombre de la categoría.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (_datos.ExisteNombre(nombre, _idSeleccionado))
                {
                    MessageBox.Show("Ya existe una categoría con ese nombre.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Categoria c = new Categoria(_idSeleccionado, nombre);

                if (_idSeleccionado == 0)
                {
                    _datos.Insertar(c);
                    MessageBox.Show("Categoría guardada.");
                }
                else
                {
                    _datos.Actualizar(c);
                    MessageBox.Show("Categoría actualizada.");
                }

                CargarCategorias();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException)
            {
                MessageBox.Show("Error al guardar en la base de datos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione una categoría de la tabla.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_datos.TieneProductos(_idSeleccionado))
                {
                    MessageBox.Show("No se puede eliminar: la categoría tiene productos.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult r = MessageBox.Show("¿Eliminar la categoría seleccionada?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    _datos.Eliminar(_idSeleccionado);
                    CargarCategorias();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Error al eliminar en la base de datos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}