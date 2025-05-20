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
            _categoriaService = new CategoriaService();
            CargarCategorias();
        }

        private readonly CategoriaService _categoriaService = new CategoriaService();

        private void CargarCategorias()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = _categoriaService.Listar();
            listBox1.DisplayMember = "Nombre"; // Mostrar solo el nombre en el ListBox
        }

        private void BtnGuardarProducto_Click(object sender, EventArgs e)
        {
            string nombre = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor ingresa un nombre.");
                return;
            }

            Categoria nuevaCategoria = new Categoria { Nombre = nombre };

            try
            {
                _categoriaService.Agregar(nuevaCategoria); // Método Agregar no devuelve un valor
                textBox1.Text = nuevaCategoria.Id.ToString();
                MessageBox.Show("Categoría guardada correctamente.");
                textBox2.Clear();
                CargarCategorias(); // Opcional
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar en la base de datos: {ex.Message}");
            }
        }
        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
        }
    }
}
