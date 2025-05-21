using Dal;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Visual
{
    public partial class FrmProductos : Form
    {
        // ← Conexión a Oracle
        private const string connectionString =
            "User Id=jenapp;Password=jen123;Data Source=192.168.221.244:1521/XEPDB1";
        public FrmProductos()
        {
            InitializeComponent();
         


        }
        CategoriaRepository _categoriaRepository = new CategoriaRepository(" User Id=jenapp;Password=jen123;Data Source=192.168.221.244:1521/XEPDB1");
        ProductoRepository _productoRepository = new ProductoRepository("User Id=jenapp;Password=jen123;Data Source=192.168.221.244:1521/XEPDB1");
        public void Minimizar()
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
      

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnproductos_Enter(object sender, EventArgs e)
        {

        }

        private void btnproductos_MouseEnter(object sender, EventArgs e)
        {

        }

        private void MenuVertical_Paint(object sender, PaintEventArgs e)
        {
        }


        private void pictureBox5_Click(object sender, EventArgs e)//boton cerrar
        {
           this.Close();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarProductos();


        }

        private void btnproductos_Click(object sender, EventArgs e)
        {

        }

        private void minimizar_Click(object sender, EventArgs e)
        {
            Minimizar();
        }



        

        private void btncategorias_Click(object sender, EventArgs e)
        {

            this.Close();
            FrmCategorias frm = new FrmCategorias();
           
            frm.Show();
          
        }

        private void BtnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                // Leer los valores de los TextBox
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                decimal precio = decimal.Parse(txtPrecio.Text.Trim());
                int stock = int.Parse(txtStock.Text.Trim());
               
                int idCategoria = (int)comboBoxCategoria.SelectedValue;
                // Crear objeto Producto
                var producto = new Producto
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Stock = stock,

                    IdCategoria = idCategoria
                };

                // Guardar en base de datos
                bool exito = _productoRepository.Agregar(producto);

                if (exito)
                {
                    MessageBox.Show("Producto guardado correctamente.\nID generado: " + producto.Id);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al guardar el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
            
    }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
           
        }

        private void ListaProducto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CargarCategorias()
        {
            var categorias = _categoriaRepository.ObtenerTodos();

            comboBoxCategoria.DataSource = null;
            comboBoxCategoria.DataSource = categorias;
            comboBoxCategoria.DisplayMember = "Nombre";   // Lo que se muestra
            comboBoxCategoria.ValueMember = "Id";         // El valor real
        }

        private void CargarProductos()
        {
            var productos = _productoRepository.ObtenerTodos(); // Asegúrate que este método exista

            ListaProducto.DataSource = null;
            ListaProducto.DataSource = productos;
            ListaProducto.DisplayMember = "Nombre"; // Puedes cambiar esto para mostrar otra propiedad
           
        }
        private void comboBoxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

