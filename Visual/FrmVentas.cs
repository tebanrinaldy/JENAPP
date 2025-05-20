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
using Entity.Entity;





namespace Visual
{
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
            ConfigurarColumnasDataProducto();
            ConfigurarColumnasVenta();
            CargarTodosLosProductos();

            DataProducto.CellDoubleClick += DataProducto_CellDoubleClick;
        }

        CategoriaRepository _categoriaRepository = new CategoriaRepository(" User Id=jenapp;Password=jen123;Data Source=192.168.1.38:1521/XEPDB1");
        ProductoRepository _productoRepository = new ProductoRepository("User Id=jenapp;Password=jen123;Data Source=192.168.1.38:1521/XEPDB1");
        VentaRepository ventaRepository = new VentaRepository("User Id=jenapp;Password=jen123;Data Source=192.168.1.38:1521/XEPDB1");

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

        private void ConfigurarColumnasVenta()
        {
            dgvVentas.Columns.Clear();
            dgvVentas.Columns.Add("colProducto", "Producto");
            dgvVentas.Columns.Add("colPrecio", "Precio");
            dgvVentas.Columns.Add("colCantidad", "Cantidad");
            dgvVentas.Columns.Add("colSubtotal", "Subtotal");
        }


        private void DataProducto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var filaSeleccionada = DataProducto.Rows[e.RowIndex];

                string nombre = filaSeleccionada.Cells["colNombre"].Value?.ToString();
                decimal precio = Convert.ToDecimal(filaSeleccionada.Cells["colPrecio"].Value);
                int cantidad = 1;
                decimal subtotal = precio * cantidad;

                // Agregar a dgvVentas
                int filaVenta = dgvVentas.Rows.Add();
                dgvVentas.Rows[filaVenta].Cells["colProducto"].Value = nombre;
                dgvVentas.Rows[filaVenta].Cells["colPrecio"].Value = precio;
                dgvVentas.Rows[filaVenta].Cells["colCantidad"].Value = cantidad;
                dgvVentas.Rows[filaVenta].Cells["colSubtotal"].Value = subtotal;
            }
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow fila in dgvVentas.Rows)
            {
                if (fila.Cells["colSubtotal"].Value != null &&
                    decimal.TryParse(fila.Cells["colSubtotal"].Value.ToString(), out decimal subtotal))
                {
                    total += subtotal;
                }
            }

            lblTotal.Text = $"Total: ${total:F2}";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
         
            if (dgvVentas.CurrentRow != null)
            {
                dgvVentas.Rows.Remove(dgvVentas.CurrentRow);
                CalcularTotal(); // Recalcula el total después de eliminar
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos para vender.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Crear el objeto venta y asignar datos  
                Venta nuevaVenta = new Venta
                {
                    FechaVenta = DateTime.Now,
                    Total = dgvVentas.Rows.Cast<DataGridViewRow>()
                                .Sum(row => Convert.ToDecimal(row.Cells["colSubtotal"].Value)),

                    CedulaCliente = txtCedula.Text.Trim(),
                    NombreCliente = txtNombreCliente.Text.Trim(),
                    TelefonoCliente = txtTelefono.Text.Trim(),

                    DetalleVentas = new List<DetalleVenta>()
                };

                // Validar datos básicos 
                if (string.IsNullOrEmpty(nuevaVenta.CedulaCliente) ||
                    string.IsNullOrEmpty(nuevaVenta.NombreCliente))
                {
                    MessageBox.Show("Debe ingresar la cédula y nombre del cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Agregar detalles de venta  
                foreach (DataGridViewRow row in dgvVentas.Rows)
                {
                    if (row.IsNewRow) continue;

                    DetalleVenta detalle = new DetalleVenta
                    {
                        NombreProducto = row.Cells["colProducto"].Value.ToString(),
                        PrecioUnitario = Convert.ToDecimal(row.Cells["colPrecio"].Value),
                        Cantidad = Convert.ToInt32(row.Cells["colCantidad"].Value),
                        Subtotal = Convert.ToDecimal(row.Cells["colSubtotal"].Value)
                    };

                    nuevaVenta.DetalleVentas.Add(detalle);
                }

                // Guardar usando el repositorio  
                bool guardado = ventaRepository.Agregar(nuevaVenta); // Cambiado para usar el método Agregar correctamente  

                if (guardado)
                {
                    MessageBox.Show("Venta registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvVentas.Rows.Clear();
                    lblTotal.Text = "Total: $0.00";

                    // Opcional: limpiar campos cliente  
                    txtCedula.Clear();
                    txtNombreCliente.Clear();
                    txtTelefono.Clear();
                }
                else
                {
                    MessageBox.Show("Error al registrar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
        
    

