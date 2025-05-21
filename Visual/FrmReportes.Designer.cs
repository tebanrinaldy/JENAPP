namespace Visual
{
    partial class FrmReportes
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ventaRepositoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dvgReportes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ventaRepositoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgReportes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(278, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "REPORTES";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(80)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1300, 71);
            this.label2.TabIndex = 26;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(0, 71);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1300, 94);
            this.label12.TabIndex = 45;
            // 
            // ventaRepositoryBindingSource
            // 
            this.ventaRepositoryBindingSource.DataSource = typeof(Dal.VentaRepository);
            // 
            // dvgReportes
            // 
            this.dvgReportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgReportes.Location = new System.Drawing.Point(0, 165);
            this.dvgReportes.Name = "dvgReportes";
            this.dvgReportes.RowHeadersWidth = 51;
            this.dvgReportes.RowTemplate.Height = 24;
            this.dvgReportes.Size = new System.Drawing.Size(1300, 485);
            this.dvgReportes.TabIndex = 46;
            // 
            // FrmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 650);
            this.Controls.Add(this.dvgReportes);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReportes";
            this.Text = "FrmReportes";
            ((System.ComponentModel.ISupportInitialize)(this.ventaRepositoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgReportes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.BindingSource ventaRepositoryBindingSource;
        private System.Windows.Forms.DataGridView dvgReportes;
    }
}