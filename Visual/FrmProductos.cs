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
    public partial class FrmProductos : FrmBase
    {
        private readonly ProductoRepository _productoRepository = new ProductoRepository();
        private readonly CategoriaRepository _categoriaRepository = new CategoriaRepository();
        public FrmProductos()
        {
            InitializeComponent();
            AplicarEstiloControles(this);
        }
       
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
            ListaProducto.SelectedIndexChanged += ListaProducto_SelectedIndexChanged;


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
            if (ListaProducto.SelectedItem is Producto producto)
            {
                txtNombre.Text = producto.Nombre;
                txtDescripcion.Text = producto.Descripcion;
                txtPrecio.Text = producto.Precio.ToString("F2");
                txtStock.Text = producto.Stock.ToString();
                comboBoxCategoria.SelectedValue = producto.IdCategoria; // asignacion de categoria seleccionada
            }
        }
        private void CargarCategorias()
        {
            var categorias = _categoriaRepository.ObtenerTodos();

            comboBoxCategoria.DataSource = null;
            comboBoxCategoria.DataSource = categorias;
            comboBoxCategoria.DisplayMember = "Nombre";
            comboBoxCategoria.ValueMember = "Id"; // TENER CUIDADO CON LOS NOMBRES DECLARADOS jose_z
        }

        private void CargarProductos()
        {
            var productos = _productoRepository.ObtenerTodos(); 

            ListaProducto.DataSource = null;
            ListaProducto.DataSource = productos;
            ListaProducto.DisplayMember = "Nombre"; 
           
        }
        private void comboBoxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
       
        }


        private void BtnActualizarProducto_Click(object sender, EventArgs e)
        {
            if (ListaProducto.SelectedItem is Producto producto)
            {
                try
                {
                    // Leer valores actualizados
                    producto.Nombre = txtNombre.Text.Trim();
                    producto.Descripcion = txtDescripcion.Text.Trim();
                    producto.Precio = decimal.Parse(txtPrecio.Text.Trim());
                    producto.Stock = int.Parse(txtStock.Text.Trim());
                    producto.IdCategoria = (int)comboBoxCategoria.SelectedValue;

                    bool exito = _productoRepository.Actualizar(producto);

                    if (exito)
                    {
                        MessageBox.Show("Producto actualizado correctamente.");
                        CargarProductos(); // Refresca lista
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el producto.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la lista para actualizar.");
            }
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizarProducto_Click_1(object sender, EventArgs e)
        {
            if (ListaProducto.SelectedItem is Producto producto)
            {
                try
                {
                    // Leer valores actualizados
                    producto.Nombre = txtNombre.Text.Trim();
                    producto.Descripcion = txtDescripcion.Text.Trim();
                    producto.Precio = decimal.Parse(txtPrecio.Text.Trim());
                    producto.Stock = int.Parse(txtStock.Text.Trim());
                    producto.IdCategoria = (int)comboBoxCategoria.SelectedValue;

                    bool exito = _productoRepository.Actualizar(producto);

                    if (exito)
                    {
                        MessageBox.Show("Producto actualizado correctamente.");
                        CargarProductos(); // Refresca la lista para evitar duplicados jose_z
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el producto.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la lista para actualizar.");
            }

        }


        private void btnEliminarProductos_Click(object sender, EventArgs e)
        {
            if (ListaProducto.SelectedItem is Producto productoSeleccionado)
            {
                var confirmar = MessageBox.Show($"¿Está seguro de eliminar el producto \"{productoSeleccionado.Nombre}\"?",
                                                "Confirmar eliminación",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);

                if (confirmar == DialogResult.Yes)
                {
                    try
                    {
                        bool eliminado = _productoRepository.Eliminar(productoSeleccionado.Id);

                        if (eliminado)
                        {
                            MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProductos();  
                            LimpiarCampos();   
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto de la lista para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

