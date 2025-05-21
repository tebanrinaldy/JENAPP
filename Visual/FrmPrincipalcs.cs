using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Visual
{
    public partial class FrmPrincipalcs: Form
    {
        public FrmPrincipalcs()
        {
            InitializeComponent();
        }
    public void cerrar()
        {
            Application.Exit();
        }
        public void minimizar()
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void desplazar()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            minimizar();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            cerrar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern void SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
        private void Abrirformularioproductos(object formproductos)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formproductos as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(fh);
            panelContenedor.Tag = fh;
         
            
      

        }

        private void btnproductos_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmProductos());


        }
        private void Abrirformularioventas(object formventas)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formventas as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(fh);
            panelContenedor.Tag = fh;
            
      

        }

        private void btnventas_Click(object sender, EventArgs e)
        {
            Abrirformularioventas(new FrmVentas());
            FrmVentas frm = new FrmVentas();
            frm.Show();
            frm.FormClosed += (s, args) => this.Show(); // Vuelve a mostrar FrmPrincipal al cerrar FrmVentas
            this.Hide(); // Oculta FrmPrincipal al abrir FrmVentas  
        }

        private void Abrirformularioreportes(object formreportes)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formreportes as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(fh);
            panelContenedor.Tag = fh;
            fh.Show();
        }

        private void btnreportes_Click(object sender, EventArgs e)
        {
            Abrirformularioreportes(new FrmReportes());
        }

        private void MenuVertical_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Abrirformulariocategorias(object formcategorias)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formcategorias as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(fh);
            panelContenedor.Tag = fh;

        }
        private void btncategorias_Click(object sender, EventArgs e)
        {
            Abrirformulariocategorias(new FrmCategorias());
            FrmCategorias frm = new FrmCategorias();
            frm.Show();
            frm.FormClosed += (s, args) => this.Show(); // Vuelve a mostrar FrmPrincipal al cerrar FrmCategorias
            this.Hide(); // Oculta FrmPrincipal al abrir FrmCategorias
        }

        private void AbrirFormularioEnPanel(Form formulario)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);

            formulario.TopLevel = false;
            formulario.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(formulario);
            this.panelContenedor.Tag = formulario;
            formulario.Show();
        }



        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
