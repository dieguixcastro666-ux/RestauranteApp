namespace RestauranteApp.Formularios
{
    partial class FrmFacturacion
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
            dgvDetalle = new DataGridView();
            lblTotal = new Label();
            label2 = new Label();
            cmbMetodo = new ComboBox();
            label3 = new Label();
            txtRecibido = new TextBox();
            lblCambio = new Label();
            btnCobrar = new Button();
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
            cmbMesa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMesa.FormattingEnabled = true;
            cmbMesa.Location = new Point(76, 17);
            cmbMesa.Name = "cmbMesa";
            cmbMesa.Size = new Size(160, 33);
            cmbMesa.TabIndex = 1;
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
            dgvDetalle.Size = new Size(560, 330);
            dgvDetalle.TabIndex = 2;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(590, 70);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(102, 25);
            lblTotal.TabIndex = 3;
            lblTotal.Text = "Total: $0.00";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(590, 130);
            label2.Name = "label2";
            label2.Size = new Size(152, 25);
            label2.TabIndex = 4;
            label2.Text = "Método de pago:";
            // 
            // cmbMetodo
            // 
            cmbMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodo.FormattingEnabled = true;
            cmbMetodo.Location = new Point(590, 158);
            cmbMetodo.Name = "cmbMetodo";
            cmbMetodo.Size = new Size(180, 33);
            cmbMetodo.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(590, 200);
            label3.Name = "label3";
            label3.Size = new Size(147, 25);
            label3.TabIndex = 6;
            label3.Text = "Efectivo recibido:";
            // 
            // txtRecibido
            // 
            txtRecibido.Location = new Point(590, 225);
            txtRecibido.Name = "txtRecibido";
            txtRecibido.Size = new Size(180, 31);
            txtRecibido.TabIndex = 7;
            // 
            // lblCambio
            // 
            lblCambio.AutoSize = true;
            lblCambio.Location = new Point(590, 275);
            lblCambio.Name = "lblCambio";
            lblCambio.Size = new Size(127, 25);
            lblCambio.TabIndex = 8;
            lblCambio.Text = "Cambio: $0.00";
            // 
            // btnCobrar
            // 
            btnCobrar.Location = new Point(590, 330);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(180, 40);
            btnCobrar.TabIndex = 9;
            btnCobrar.Text = "Cobrar";
            btnCobrar.UseVisualStyleBackColor = true;
            // 
            // FrmFacturacion
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 394);
            Controls.Add(btnCobrar);
            Controls.Add(lblCambio);
            Controls.Add(txtRecibido);
            Controls.Add(label3);
            Controls.Add(cmbMetodo);
            Controls.Add(label2);
            Controls.Add(lblTotal);
            Controls.Add(dgvDetalle);
            Controls.Add(cmbMesa);
            Controls.Add(label1);
            Name = "FrmFacturacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cobro de pedido";
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbMesa;
        private DataGridView dgvDetalle;
        private Label lblTotal;
        private Label label2;
        private ComboBox cmbMetodo;
        private Label label3;
        private TextBox txtRecibido;
        private Label lblCambio;
        private Button btnCobrar;
    }
}