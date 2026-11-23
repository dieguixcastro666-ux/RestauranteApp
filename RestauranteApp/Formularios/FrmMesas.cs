using Microsoft.Data.SqlClient;
using RestauranteApp.Datos;
using RestauranteApp.Modelos;

namespace RestauranteApp.Formularios
{
    public partial class FrmMesas : Form
    {
        private readonly MesaDatos _datos = new MesaDatos();

        public FrmMesas()
        {
            InitializeComponent();
            Load += FrmMesas_Load;
            btnAgregar.Click += btnAgregar_Click;
            btnLiberar.Click += btnLiberar_Click;
            btnEliminar.Click += btnEliminar_Click;
        }

        private void FrmMesas_Load(object? sender, EventArgs e)
        {
            CargarMesas();
        }

        private void CargarMesas()
        {
            dgvMesas.DataSource = _datos.ObtenerTodas();
            dgvMesas.Columns["IdMesa"].Visible = false;
            dgvMesas.Columns["EstaLibre"].Visible = false;

            // Mesas ocupadas en rojo claro, libres en verde claro
            foreach (DataGridViewRow fila in dgvMesas.Rows)
            {
                var m = fila.DataBoundItem as Mesa;
                if (m == null) continue;
                fila.DefaultCellStyle.BackColor =
                    m.EstaLibre ? Color.Honeydew : Color.MistyRose;
            }
            dgvMesas.ClearSelection();
        }

        private Mesa? ObtenerSeleccionada()
        {
            if (dgvMesas.SelectedRows.Count == 0) return null;
            return dgvMesas.SelectedRows[0].DataBoundItem as Mesa;
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtNumero.Text, out int numero) || numero <= 0)
            {
                MessageBox.Show("El número de mesa debe ser un entero mayor que cero.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_datos.ExisteNumero(numero))
            {
                MessageBox.Show("Ya existe una mesa con ese número.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var m = new Mesa();
                m.Numero = numero;
                _datos.Insertar(m);
                txtNumero.Clear();
                CargarMesas();
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

        private void btnLiberar_Click(object? sender, EventArgs e)
        {
            var m = ObtenerSeleccionada();
            if (m == null)
            {
                MessageBox.Show("Selecciona una mesa de la tabla.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (m.EstaLibre)
            {
                MessageBox.Show("Esa mesa ya está libre.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var r = MessageBox.Show("¿Liberar la " + m + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            try
            {
                _datos.CambiarEstado(m.IdMesa, "Libre");
                CargarMesas();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            var m = ObtenerSeleccionada();
            if (m == null)
            {
                MessageBox.Show("Selecciona una mesa de la tabla.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!m.EstaLibre)
            {
                MessageBox.Show("No se puede eliminar una mesa ocupada.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_datos.TienePedidos(m.IdMesa))
            {
                MessageBox.Show("No se puede eliminar: la mesa tiene pedidos registrados.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var r = MessageBox.Show("¿Eliminar la " + m + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            try
            {
                _datos.Eliminar(m.IdMesa);
                CargarMesas();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}