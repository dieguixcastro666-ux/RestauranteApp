namespace RestauranteApp.Formularios
{
    partial class FrmPrincipal
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
            lblTitulo = new Label();
            btnPedidos = new Button();
            btnCobro = new Button();
            btnMesas = new Button();
            btnProductos = new Button();
            btnCategorias = new Button();
            btnReportes = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(110, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(196, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Sistema de Restaurante";
            // 
            // btnPedidos
            // 
            btnPedidos.Location = new Point(40, 80);
            btnPedidos.Name = "btnPedidos";
            btnPedidos.Size = new Size(200, 50);
            btnPedidos.TabIndex = 1;
            btnPedidos.Text = "Tomar pedido";
            btnPedidos.UseVisualStyleBackColor = true;
            // 
            // btnCobro
            // 
            btnCobro.Location = new Point(260, 80);
            btnCobro.Name = "btnCobro";
            btnCobro.Size = new Size(200, 50);
            btnCobro.TabIndex = 2;
            btnCobro.Text = "Cobrar";
            btnCobro.UseVisualStyleBackColor = true;
            // 
            // btnMesas
            // 
            btnMesas.Location = new Point(40, 150);
            btnMesas.Name = "btnMesas";
            btnMesas.Size = new Size(200, 50);
            btnMesas.TabIndex = 3;
            btnMesas.Text = "Mesas";
            btnMesas.UseVisualStyleBackColor = true;
            // 
            // btnProductos
            // 
            btnProductos.Location = new Point(260, 150);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(200, 50);
            btnProductos.TabIndex = 4;
            btnProductos.Text = "Productos";
            btnProductos.UseVisualStyleBackColor = true;
            // 
            // btnCategorias
            // 
            btnCategorias.Location = new Point(40, 220);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Size = new Size(200, 50);
            btnCategorias.TabIndex = 5;
            btnCategorias.Text = "Categorías";
            btnCategorias.UseVisualStyleBackColor = true;
            // 
            // btnReportes
            // 
            btnReportes.Location = new Point(260, 220);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(200, 50);
            btnReportes.TabIndex = 6;
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(150, 310);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(200, 50);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 374);
            Controls.Add(btnSalir);
            Controls.Add(btnReportes);
            Controls.Add(btnCategorias);
            Controls.Add(btnProductos);
            Controls.Add(btnMesas);
            Controls.Add(btnCobro);
            Controls.Add(btnPedidos);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión de Restaurante";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Button btnPedidos;
        private Button btnCobro;
        private Button btnMesas;
        private Button btnProductos;
        private Button btnCategorias;
        private Button btnReportes;
        private Button btnSalir;
    }
}