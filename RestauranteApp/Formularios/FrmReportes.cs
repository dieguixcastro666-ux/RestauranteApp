using Microsoft.Data.SqlClient;
using RestauranteApp.Datos;

namespace RestauranteApp.Formularios
{
    public partial class FrmReportes : Form
    {
        private readonly ReporteDatos _datos = new ReporteDatos();

        public FrmReportes()
        {
            InitializeComponent();
            Load += FrmReportes_Load;
            btnVentas.Click += btnVentas_Click;
            btnMasVendidos.Click += btnMasVendidos_Click;
        }

        private void FrmReportes_Load(object? sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Today;
            MostrarVentas();
        }

        private void btnVentas_Click(object? sender, EventArgs e)
        {
            MostrarVentas();
        }

        private void btnMasVendidos_Click(object? sender, EventArgs e)
        {
            MostrarMasVendidos();
        }

        private void MostrarVentas()
        {
            try
            {
                dgvReporte.DataSource = _datos.VentasPorFecha(dtpFecha.Value);

                if (dgvReporte.Columns.Contains("Monto"))
                    dgvReporte.Columns["Monto"].DefaultCellStyle.Format = "$0.00";
                if (dgvReporte.Columns.Contains("Fecha"))
                    dgvReporte.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                decimal total = _datos.TotalVentas(dtpFecha.Value);
                lblTotal.Text = "Total del día: " + total.ToString("$0.00");
                dgvReporte.ClearSelection();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarMasVendidos()
        {
            try
            {
                dgvReporte.DataSource = _datos.ProductosMasVendidos();

                if (dgvReporte.Columns.Contains("Total"))
                    dgvReporte.Columns["Total"].DefaultCellStyle.Format = "$0.00";

                lblTotal.Text = "Productos más vendidos";
                dgvReporte.ClearSelection();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}