namespace Visual
{
    partial class FrmProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductos));
            this.BtnGuardarProducto = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.cerrar = new System.Windows.Forms.PictureBox();
            this.minimizar = new System.Windows.Forms.PictureBox();
            this.BtnMinimizar = new System.Windows.Forms.PictureBox();
            this.BtnCerrar = new System.Windows.Forms.PictureBox();
            this.ListaProducto = new System.Windows.Forms.ListBox();
            this.comboBoxCategoria = new System.Windows.Forms.ComboBox();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnGuardarProducto
            // 
            this.BtnGuardarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.BtnGuardarProducto.FlatAppearance.BorderSize = 0;
            this.BtnGuardarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGuardarProducto.Location = new System.Drawing.Point(520, 493);
            this.BtnGuardarProducto.Name = "BtnGuardarProducto";
            this.BtnGuardarProducto.Size = new System.Drawing.Size(204, 35);
            this.BtnGuardarProducto.TabIndex = 15;
            this.BtnGuardarProducto.Text = "Guardar";
            this.BtnGuardarProducto.UseVisualStyleBackColor = false;
            this.BtnGuardarProducto.Click += new System.EventHandler(this.BtnGuardarProducto_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(50, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "NOMBRE";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(255, 173);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(259, 22);
            this.txtNombre.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(418, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "PRODUCTOS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(50, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 23);
            this.label4.TabIndex = 14;
            this.label4.Text = "CATEGORIA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(50, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 23);
            this.label5.TabIndex = 19;
            this.label5.Text = "PRECIO";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(255, 265);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(259, 22);
            this.txtPrecio.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(50, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 23);
            this.label6.TabIndex = 21;
            this.label6.Text = "STOCK";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(255, 310);
            this.txtStock.Multiline = true;
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(259, 22);
            this.txtStock.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(50, 343);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 23);
            this.label7.TabIndex = 23;
            this.label7.Text = "DESCRIPCION";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(255, 346);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(259, 110);
            this.txtDescripcion.TabIndex = 22;
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.Navy;
            this.BarraTitulo.Controls.Add(this.cerrar);
            this.BarraTitulo.Controls.Add(this.minimizar);
            this.BarraTitulo.Controls.Add(this.BtnMinimizar);
            this.BarraTitulo.Controls.Add(this.BtnCerrar);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(1080, 35);
            this.BarraTitulo.TabIndex = 24;
            // 
            // cerrar
            // 
            this.cerrar.Image = global::Visual.Properties.Resources.X_SinFondo;
            this.cerrar.Location = new System.Drawing.Point(1042, -2);
            this.cerrar.Name = "cerrar";
            this.cerrar.Size = new System.Drawing.Size(35, 40);
            this.cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cerrar.TabIndex = 25;
            this.cerrar.TabStop = false;
            this.cerrar.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // minimizar
            // 
            this.minimizar.Image = global::Visual.Properties.Resources._Blanco;
            this.minimizar.Location = new System.Drawing.Point(975, -2);
            this.minimizar.Name = "minimizar";
            this.minimizar.Size = new System.Drawing.Size(35, 40);
            this.minimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minimizar.TabIndex = 2;
            this.minimizar.TabStop = false;
            this.minimizar.Click += new System.EventHandler(this.minimizar_Click);
            // 
            // BtnMinimizar
            // 
            this.BtnMinimizar.Image = global::Visual.Properties.Resources._Blanco;
            this.BtnMinimizar.Location = new System.Drawing.Point(1206, -2);
            this.BtnMinimizar.Name = "BtnMinimizar";
            this.BtnMinimizar.Size = new System.Drawing.Size(35, 40);
            this.BtnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BtnMinimizar.TabIndex = 1;
            this.BtnMinimizar.TabStop = false;
            // 
            // BtnCerrar
            // 
            this.BtnCerrar.Image = global::Visual.Properties.Resources.X_SinFondo;
            this.BtnCerrar.Location = new System.Drawing.Point(1264, -2);
            this.BtnCerrar.Name = "BtnCerrar";
            this.BtnCerrar.Size = new System.Drawing.Size(35, 40);
            this.BtnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BtnCerrar.TabIndex = 0;
            this.BtnCerrar.TabStop = false;
            // 
            // ListaProducto
            // 
            this.ListaProducto.FormattingEnabled = true;
            this.ListaProducto.ItemHeight = 16;
            this.ListaProducto.Location = new System.Drawing.Point(586, 126);
            this.ListaProducto.Name = "ListaProducto";
            this.ListaProducto.Size = new System.Drawing.Size(469, 260);
            this.ListaProducto.TabIndex = 25;
            this.ListaProducto.SelectedIndexChanged += new System.EventHandler(this.ListaProducto_SelectedIndexChanged);
            // 
            // comboBoxCategoria
            // 
            this.comboBoxCategoria.FormattingEnabled = true;
            this.comboBoxCategoria.Location = new System.Drawing.Point(255, 224);
            this.comboBoxCategoria.Name = "comboBoxCategoria";
            this.comboBoxCategoria.Size = new System.Drawing.Size(259, 24);
            this.comboBoxCategoria.TabIndex = 26;
            this.comboBoxCategoria.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategoria_SelectedIndexChanged);
            // 
            // FrmProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1080, 615);
            this.Controls.Add(this.comboBoxCategoria);
            this.Controls.Add(this.ListaProducto);
            this.Controls.Add(this.BarraTitulo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.BtnGuardarProducto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "   ";
            this.Load += new System.EventHandler(this.FrmProductos_Load);
            this.BarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCerrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnGuardarProducto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox BtnMinimizar;
        private System.Windows.Forms.PictureBox BtnCerrar;
        private System.Windows.Forms.PictureBox cerrar;
        private System.Windows.Forms.PictureBox minimizar;
        private System.Windows.Forms.ListBox ListaProducto;
        private System.Windows.Forms.ComboBox comboBoxCategoria;
    }
}