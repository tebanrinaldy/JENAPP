using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dal;
using Bll;

namespace Visual
{
    public partial class FrmCategorias : Form
    {
        public FrmCategorias()
        {
            InitializeComponent();
        }

        private readonly CategoriaService _categoriaService = new CategoriaService();


        private void BtnGuardarProducto_Click(object sender, EventArgs e)
        {
          
           
        }
    }
}
