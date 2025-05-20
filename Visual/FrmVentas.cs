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
            CargarCategoriasEnComboBox();
            DataProducto.EditingControlShowing += DataProducto_EditingControlShowing;
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


        private void CargarCategoriasEnComboBox()
        {
            var categorias = _categoriaRepository.ObtenerTodos();

            var colCategorias = DataProducto.Columns["colCategorias"] as DataGridViewComboBoxColumn;

            if (colCategorias != null)
            {
                colCategorias.DataSource = categorias;
                colCategorias.DisplayMember = "Nombre";
                colCategorias.ValueMember = "Id";
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

            var colCategorias = new DataGridViewComboBoxColumn
            {
                Name = "colCategorias",
                HeaderText = "Categoría",
                DataPropertyName = "IdCategoria"  // opcional
            };

            DataProducto.Columns.Add(colCategorias);
        }

        private void CategoriaSeleccionadaChanged(object sender, EventArgs e)
        {
            if (DataProducto.CurrentCell == null)
                return;

            ComboBox cb = sender as ComboBox;
            if (cb?.SelectedItem is Categoria categoriaSeleccionada)
            {
                var producto = _productoRepository.ObtenerPorId(categoriaSeleccionada.Id);

                if (producto != null)
                {
                    int fila = DataProducto.CurrentCell.RowIndex;

                    DataProducto.Rows[fila].Cells["colNombre"].Value = producto.Nombre;
                    DataProducto.Rows[fila].Cells["colPrecio"].Value = producto.Precio;
                    DataProducto.Rows[fila].Cells["colStock"].Value = producto.Stock;
                }
            }
        }
        private void DataProducto_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DataProducto.CurrentCell.ColumnIndex == DataProducto.Columns["colCategorias"].Index &&
                e.Control is ComboBox cb)
            {
                cb.SelectedIndexChanged -= CategoriaSeleccionadaChanged; // evitar múltiples suscripciones
                cb.SelectedIndexChanged += CategoriaSeleccionadaChanged;
            }
        }
    }
}
