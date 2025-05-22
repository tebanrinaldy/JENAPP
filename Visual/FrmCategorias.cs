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
    public partial class FrmCategorias : FrmBase
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
            AplicarEstiloControles(this);



            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            BtnActualizar.Click += BtnActualizar_Click;
            BtnEliminarCategoria.Click += BtnEliminarCategoria_Click;


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
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Categoria categoria)
            {
                textBox1.Text = categoria.Id.ToString();
                textBox2.Text = categoria.Nombre;
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

        private void BtnEliminarCategoria_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int id))
            {
                var confirmar = MessageBox.Show("¿Está seguro que desea eliminar esta categoría?",
                                               "Confirmar eliminación",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Warning);

                if (confirmar == DialogResult.Yes)
                {
                    bool exito = _categoriaRepository.Eliminar(id);
                    if (exito)
                    {
                        MessageBox.Show("Categoría eliminada correctamente.");
                        CargarCategorias();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la categoría.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona una categoría válida para eliminar.");
            }
        }
        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int id))
            {
                string nombre = textBox2.Text.Trim();

                if (string.IsNullOrEmpty(nombre))
                {
                    MessageBox.Show("Por favor ingresa un nombre.");
                    return;
                }

                var categoria = new Categoria
                {
                    Id = id,
                    Nombre = nombre
                };

                bool exito = _categoriaRepository.Actualizar(categoria);
                if (exito)
                {
                    MessageBox.Show("Categoría actualizada correctamente.");
                    CargarCategorias();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar la categoría.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una categoría válida para actualizar.");
            }
        }
    }
}
