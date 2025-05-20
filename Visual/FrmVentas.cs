using Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using Bll;





namespace Visual
{
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
            ConfigurarColumnasDataProducto();
            CargarTodosLosProductos();

        }

        CategoriaRepository _categoriaRepository = new CategoriaRepository(" User Id=jenapp;Password=jen123;Data Source=192.168.1.38:1521/XEPDB1");
        ProductoRepository _productoRepository = new ProductoRepository("User Id=jenapp;Password=jen123;Data Source=192.168.1.38:1521/XEPDB1");

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarTodosLosProductos()
        {
            var productos = _productoRepository.ObtenerTodos(); // Método que retorna List<Producto>
            var categorias = _categoriaRepository.ObtenerTodos(); // Necesario para mapear el ID al nombre

            DataProducto.Rows.Clear();

            foreach (var producto in productos)
            {
                string nombreCategoria = categorias
                    .FirstOrDefault(c => c.Id == producto.IdCategoria)?.Nombre ?? "Sin categoría";

                int rowIndex = DataProducto.Rows.Add();
                DataProducto.Rows[rowIndex].Cells["colNombre"].Value = producto.Nombre;
                DataProducto.Rows[rowIndex].Cells["colPrecio"].Value = producto.Precio;
                DataProducto.Rows[rowIndex].Cells["colStock"].Value = producto.Stock;
                DataProducto.Rows[rowIndex].Cells["colCategorias"].Value = nombreCategoria;
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ConfigurarColumnasDataProducto()
        {
            DataProducto.Columns.Clear();

            DataProducto.Columns.Add("colNombre", "Producto");
            DataProducto.Columns.Add("colPrecio", "Precio");
            DataProducto.Columns.Add("colStock", "Stock");

            var colCategorias = new DataGridViewTextBoxColumn
            {
                Name = "colCategorias",
                HeaderText = "Categoría"
            };

            DataProducto.Columns.Add(colCategorias);
        }
    }

     
  
        }
    

