namespace RestauranteApp.Formularios
{
    partial class FrmPedidos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            cmbMesa = new ComboBox();
            label2 = new Label();
            cmbProducto = new ComboBox();
            label3 = new Label();
            nudCantidad = new NumericUpDown();
            btnAgregar = new Button();
            dgvDetalle = new DataGridView();
            btnQuitar = new Button();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(58, 25);
            label1.TabIndex = 0;
            label1.Text = "Mesa:";
            // 
            // cmbMesa
            // 
            cmbMesa.FormattingEnabled = true;
            cmbMesa.Location = new Point(76, 17);
            cmbMesa.Name = "cmbMesa";
            cmbMesa.Size = new Size(120, 33);
            cmbMesa.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(200, 20);
            label2.Name = "label2";
            label2.Size = new Size(89, 25);
            label2.TabIndex = 2;
            label2.Text = "Producto:";
            // 
            // cmbProducto
            // 
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(295, 20);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(120, 33);
            cmbProducto.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(440, 23);
            label3.Name = "label3";
            label3.Size = new Size(87, 25);
            label3.TabIndex = 4;
            label3.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(533, 22);
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(60, 31);
            nudCantidad.TabIndex = 5;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(616, 16);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(100, 34);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // dgvDetalle
            // 
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(12, 60);
            dgvDetalle.MultiSelect = false;
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.RowHeadersWidth = 62;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.Size = new Size(860, 380);
            dgvDetalle.TabIndex = 7;
            // 
            // btnQuitar
            // 
            btnQuitar.Location = new Point(12, 455);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(100, 34);
            btnQuitar.TabIndex = 8;
            btnQuitar.Text = "Quitar";
            btnQuitar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(122, 455);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(140, 34);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar pedido";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(272, 455);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(100, 34);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(640, 458);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(102, 25);
            lblTotal.TabIndex = 11;
            lblTotal.Text = "Total: $0.00";
            // 
            // FrmPedidos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 544);
            Controls.Add(lblTotal);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(btnQuitar);
            Controls.Add(dgvDetalle);
            Controls.Add(btnAgregar);
            Controls.Add(nudCantidad);
            Controls.Add(label3);
            Controls.Add(cmbProducto);
            Controls.Add(label2);
            Controls.Add(cmbMesa);
            Controls.Add(label1);
            Name = "FrmPedidos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pedidos";
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbMesa;
        private Label label2;
        private ComboBox cmbProducto;
        private Label label3;
        private NumericUpDown nudCantidad;
        private Button btnAgregar;
        private DataGridView dgvDetalle;
        private Button btnQuitar;
        private Button btnGuardar;
        private Button btnLimpiar;
        private Label lblTotal;
    }
}