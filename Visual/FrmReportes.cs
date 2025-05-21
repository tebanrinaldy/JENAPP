using Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;

namespace Visual
{
    public partial class FrmReportes : Form
    {
       private readonly ReporteRepository _reporteRepository = new ReporteRepository("User Id=jenapp;Password=jen123;Data Source=192.168.1.25:1521/XEPDB1");
        public FrmReportes()
        {
            InitializeComponent();
            this.Load += FrmReportes_Load;
        }
        private void FrmReportes_Load(object sender, EventArgs e)
        {
            try
            {
                List<Reportes> reportes = _reporteRepository.ObtenerTodos();
                dvgReportes.DataSource = reportes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reportes: " + ex.Message);
            }
        }
    }
}
