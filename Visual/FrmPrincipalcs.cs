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
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern void SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
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
    }
}
