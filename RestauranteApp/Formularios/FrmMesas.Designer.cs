namespace RestauranteApp.Formularios
{
    partial class FrmMesas
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
            txtNumero = new TextBox();
            btnAgregar = new Button();
            btnLiberar = new Button();
            btnEliminar = new Button();
            dgvMesas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvMesas).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(153, 25);
            label1.TabIndex = 0;
            label1.Text = "Número de mesa:";
            // 
            // txtNumero
            // 
            txtNumero.Location = new Point(162, 17);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(80, 31);
            txtNumero.TabIndex = 1;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(248, 15);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(100, 34);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnLiberar
            // 
            btnLiberar.Location = new Point(354, 15);
            btnLiberar.Name = "btnLiberar";
            btnLiberar.Size = new Size(100, 34);
            btnLiberar.TabIndex = 3;
            btnLiberar.Text = "Liberar";
            btnLiberar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(460, 15);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 34);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvMesas
            // 
            dgvMesas.AllowUserToAddRows = false;
            dgvMesas.AllowUserToDeleteRows = false;
            dgvMesas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMesas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMesas.Location = new Point(12, 60);
            dgvMesas.MultiSelect = false;
            dgvMesas.Name = "dgvMesas";
            dgvMesas.ReadOnly = true;
            dgvMesas.RowHeadersVisible = false;
            dgvMesas.RowHeadersWidth = 62;
            dgvMesas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMesas.Size = new Size(560, 330);
            dgvMesas.TabIndex = 5;
            // 
            // FrmMesas
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 394);
            Controls.Add(dgvMesas);
            Controls.Add(btnEliminar);
            Controls.Add(btnLiberar);
            Controls.Add(btnAgregar);
            Controls.Add(txtNumero);
            Controls.Add(label1);
            Name = "FrmMesas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mesas";
            Load += FrmMesas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMesas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtNumero;
        private Button btnAgregar;
        private Button btnLiberar;
        private Button btnEliminar;
        private DataGridView dgvMesas;
    }
}