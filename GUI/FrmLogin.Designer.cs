namespace Visual
{
    partial class FrmLogin
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TextUsuario = new System.Windows.Forms.TextBox();
            this.Line = new System.Windows.Forms.Label();
            this.TextContraseña = new System.Windows.Forms.TextBox();
            this.TextLogin = new System.Windows.Forms.TextBox();
            this.BtnAcceder = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.BtnCerrar = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureUsuario = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 330);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(245, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(514, 1);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // TextUsuario
            // 
            this.TextUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TextUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextUsuario.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextUsuario.ForeColor = System.Drawing.Color.DarkGray;
            this.TextUsuario.Location = new System.Drawing.Point(339, 93);
            this.TextUsuario.Name = "TextUsuario";
            this.TextUsuario.Size = new System.Drawing.Size(404, 25);
            this.TextUsuario.TabIndex = 1;
            this.TextUsuario.Text = "USUARIO:";
            this.TextUsuario.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.TextUsuario.Enter += new System.EventHandler(this.TextUsuario_Enter);
            this.TextUsuario.Leave += new System.EventHandler(this.TextUsuario_Leave);
            // 
            // Line
            // 
            this.Line.BackColor = System.Drawing.Color.DarkGray;
            this.Line.Location = new System.Drawing.Point(254, 217);
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(514, 1);
            this.Line.TabIndex = 4;
            this.Line.Text = "label2";
            // 
            // TextContraseña
            // 
            this.TextContraseña.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TextContraseña.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextContraseña.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextContraseña.ForeColor = System.Drawing.Color.DarkGray;
            this.TextContraseña.Location = new System.Drawing.Point(339, 173);
            this.TextContraseña.Name = "TextContraseña";
            this.TextContraseña.Size = new System.Drawing.Size(404, 25);
            this.TextContraseña.TabIndex = 2;
            this.TextContraseña.Text = "CONTRASEÑA:";
            this.TextContraseña.Enter += new System.EventHandler(this.TextContraseña_Enter);
            this.TextContraseña.Leave += new System.EventHandler(this.TextContraseña_Leave);
            // 
            // TextLogin
            // 
            this.TextLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TextLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextLogin.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextLogin.ForeColor = System.Drawing.Color.DarkGray;
            this.TextLogin.Location = new System.Drawing.Point(451, 12);
            this.TextLogin.Name = "TextLogin";
            this.TextLogin.Size = new System.Drawing.Size(130, 41);
            this.TextLogin.TabIndex = 7;
            this.TextLogin.Text = "LOGIN";
            // 
            // BtnAcceder
            // 
            this.BtnAcceder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.BtnAcceder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BtnAcceder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnAcceder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAcceder.ForeColor = System.Drawing.Color.LightGray;
            this.BtnAcceder.Location = new System.Drawing.Point(366, 246);
            this.BtnAcceder.Name = "BtnAcceder";
            this.BtnAcceder.Size = new System.Drawing.Size(277, 31);
            this.BtnAcceder.TabIndex = 3;
            this.BtnAcceder.Text = "ACCEDER";
            this.BtnAcceder.UseVisualStyleBackColor = false;
            this.BtnAcceder.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Gray;
            this.linkLabel1.Location = new System.Drawing.Point(386, 290);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(227, 16);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "¿HA OLVIDADO LA CONTRASEÑA?";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Visual.Properties.Resources.@__SinFondo;
            this.pictureBox4.Location = new System.Drawing.Point(721, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(22, 24);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // BtnCerrar
            // 
            this.BtnCerrar.Image = global::Visual.Properties.Resources.X_SinFondo;
            this.BtnCerrar.Location = new System.Drawing.Point(748, 0);
            this.BtnCerrar.Name = "BtnCerrar";
            this.BtnCerrar.Size = new System.Drawing.Size(30, 24);
            this.BtnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BtnCerrar.TabIndex = 10;
            this.BtnCerrar.TabStop = false;
            this.BtnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(284, 161);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureUsuario
            // 
            this.pictureUsuario.Image = global::Visual.Properties.Resources.user_SinFondo;
            this.pictureUsuario.Location = new System.Drawing.Point(284, 83);
            this.pictureUsuario.Name = "pictureUsuario";
            this.pictureUsuario.Size = new System.Drawing.Size(39, 35);
            this.pictureUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureUsuario.TabIndex = 3;
            this.pictureUsuario.TabStop = false;
            this.pictureUsuario.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Visual.Properties.Resources.LogoJenapp_SinFondo;
            this.pictureBox2.Location = new System.Drawing.Point(0, 58);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(247, 206);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(780, 330);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.BtnCerrar);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.BtnAcceder);
            this.Controls.Add(this.TextLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TextContraseña);
            this.Controls.Add(this.Line);
            this.Controls.Add(this.pictureUsuario);
            this.Controls.Add(this.TextUsuario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogin";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextUsuario;
        private System.Windows.Forms.PictureBox pictureUsuario;
        private System.Windows.Forms.Label Line;
        private System.Windows.Forms.TextBox TextContraseña;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox TextLogin;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button BtnAcceder;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox BtnCerrar;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}

