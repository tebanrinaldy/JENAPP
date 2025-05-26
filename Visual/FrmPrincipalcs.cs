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
    public partial class FrmPrincipalcs: FrmBase
    {
        public FrmPrincipalcs()
        {
            InitializeComponent();
            AplicarEstiloControles(this);
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

        
        private void btnproductos_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmProductos());


        }


        private void btnventas_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmVentas());
        }

    

        private void btnreportes_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmReportes());
        }

        private void MenuVertical_Paint(object sender, PaintEventArgs e)
        {

        }


        private void btncategorias_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmCategorias());
        }
        //err
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

        private void btnInventario_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmInventario());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmFacturas());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
