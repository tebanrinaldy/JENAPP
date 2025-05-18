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
    public partial class FrmLogin: Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll",EntryPoint ="ReleaseCapture")]
        public static extern void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern void SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

       public void salir()
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void TextUsuario_Enter(object sender, EventArgs e)
        {
            if (TextUsuario.Text == "USUARIO:")
            {
                TextUsuario.Text = "";
                TextUsuario.ForeColor = Color.LightGray;
            }

        }

        private void TextUsuario_Leave(object sender, EventArgs e)
        {
            if (TextUsuario.Text == "")
            {
                TextUsuario.Text = "USUARIO:";
                TextUsuario.ForeColor = Color.DimGray;
            }

        }

        private void TextContraseña_Enter(object sender, EventArgs e)
        {
            if(TextContraseña.Text == "CONTRASEÑA:")
            {
                TextContraseña.Text = "";
                TextContraseña.ForeColor = Color.LightGray;
                TextContraseña.UseSystemPasswordChar = true;
            }
        }

        private void TextContraseña_Leave(object sender, EventArgs e)
        {
            if(TextContraseña.Text == "")
            {
                TextContraseña.Text = "CONTRASEÑA:";
                TextContraseña.ForeColor = Color.DimGray;
                TextContraseña.UseSystemPasswordChar = false;
            }
        }

       
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            salir();
        }
       

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            minimizar();
        }

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
         desplazar();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
         desplazar();
        }
    }
}
