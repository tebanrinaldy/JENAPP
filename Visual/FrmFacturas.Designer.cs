namespace Visual
{
    partial class FrmFacturas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvVentasDia;
        private System.Windows.Forms.Button btnGenerarFactura;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (disposing && (components != null))
                    components.Dispose();
                base.Dispose(disposing);
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvVentasDia = new System.Windows.Forms.DataGridView();
            this.btnGenerarFactura = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentasDia)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(500, 30);
            this.lblTitulo.Text = "Ventas del Día";
            // 
            // dgvVentasDia
            // 
            this.dgvVentasDia.AllowUserToAddRows = false;
            this.dgvVentasDia.AllowUserToDeleteRows = false;
            this.dgvVentasDia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVentasDia.Location = new System.Drawing.Point(12, 50);
            this.dgvVentasDia.Name = "dgvVentasDia";
            this.dgvVentasDia.ReadOnly = true;
            this.dgvVentasDia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVentasDia.Size = new System.Drawing.Size(600, 300);
            // 
            // btnGenerarFactura
            // 
            this.btnGenerarFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGenerarFactura.Location = new System.Drawing.Point(12, 370);
            this.btnGenerarFactura.Name = "btnGenerarFactura";
            this.btnGenerarFactura.Size = new System.Drawing.Size(180, 40);
            this.btnGenerarFactura.Text = "Generar Factura PDF";
            this.btnGenerarFactura.UseVisualStyleBackColor = true;
            this.btnGenerarFactura.Click += new System.EventHandler(this.btnGenerarFactura_Click);
            // 
            // FrmFacturas
            // 
            this.ClientSize = new System.Drawing.Size(630, 430);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgvVentasDia);
            this.Controls.Add(this.btnGenerarFactura);
            this.Name = "FrmFacturas";
            this.Text = "Facturación";
            this.Load += new System.EventHandler(this.FrmFacturas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentasDia)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}