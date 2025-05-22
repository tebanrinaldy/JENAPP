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
        // ← Conexión a Oracle
        private const string connectionString =
           "User Id=jenapp;Password=jen123;Data Source=localhost:1521/XEPDB1";

        private readonly CategoriaRepository _categoriaRepository;

        public FrmCategorias()
        {
            InitializeComponent();
            _categoriaRepository = new CategoriaRepository(connectionString);
           CargarCategorias();  

        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            var categorias = _categoriaRepository.ObtenerTodos();

            listBox1.DataSource = null;
            listBox1.DataSource = categorias;
            listBox1.DisplayMember = "Nombre"; // ← Asegúrate de que Categoria tenga esta propiedad
        }

        private void BtnGuardarProducto_Click(object sender, EventArgs e)
        {
            string nombre = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor ingresa un nombre.");
                return;
            }

            var nuevaCategoria = new Categoria { Nombre = nombre };

            var exito = _categoriaRepository.Agregar(nuevaCategoria);

            if (exito)
            {
                textBox1.Text = nuevaCategoria.Id.ToString(); // Mostrar el ID generado
                MessageBox.Show("Categoría guardada correctamente.");
                CargarCategorias(); // Recargar lista
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Error al guardar en la base de datos.");
            }
        }

        private void cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void minimizar24()
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void minimizar_Click(object sender, EventArgs e)
        {
            minimizar24();
        }


  
        private void btnproductos_Click(object sender, EventArgs e)
        {

            FrmProductos frm = new FrmProductos();
            frm.Show();
            this.Close();

        }
    }
}
/*private void BtnEliminar_Click(object sender, EventArgs e)
{
    if (listBox1.SelectedItem is Categoria categoriaSeleccionada)
    {
        var confirmResult = MessageBox.Show(
            "¿Estás seguro de que deseas eliminar esta categoría?",
            "Confirmar eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);
        if (confirmResult == DialogResult.Yes)
        {
            var exito = _categoriaRepository.Eliminar(categoriaSeleccionada.Id);
            if (exito)
            {
                MessageBox.Show("Categoría eliminada correctamente.");
                CargarCategorias(); // Recargar lista
            }
            else
            {
                MessageBox.Show("Error al eliminar la categoría.");
            }
        }
    }
    else
    {
        MessageBox.Show("Por favor selecciona una categoría para eliminar.");
    }
}
*/