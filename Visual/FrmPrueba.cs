using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL_TXT;
using Entity;
namespace Visual
{
    public partial class FrmPrueba: Form
    {
        public FrmPrueba()
        {
            InitializeComponent();
        }

        public void Prueba_Load(object sender, EventArgs e)
        {
            {
                var repo = new ClienteRepositoryTxt();

                // Guardar un cliente de prueba
               

                var clientes = repo.ObtenerClientes();
                if (clientes.Count > 0)
                {
                    MessageBox.Show($"Cliente: {clientes[0].Nombre} {clientes[0].Apellido}");
                }
                else
                {
                    MessageBox.Show("No hay clientes en el archivo.");
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            var repo = new ClienteRepositoryTxt();
            repo.GuardarCliente(new Cliente
            {
                Id = 1,
                Nombre = "Juan",
                Apellido = "Pérez",

            });
        }
    }
}
    
