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
using Bll.Bll;




namespace Visual
{
    public partial class FrmVentas : FrmBase
    {
        private readonly CategoriaRepository _categoriaRepository = new CategoriaRepository();
        private readonly ProductoRepository _productoRepository = new ProductoRepository();
        private readonly VentaRepository _ventaRepository = new VentaRepository();
        private readonly VentaService _ventaService;
        private readonly InventarioLogica inventarioLogica = new InventarioLogica();
        public FrmVentas()
        {
            InitializeComponent();
            _ventaService = new VentaService(_ventaRepository);

            ConfigurarColumnasDataProducto();
            ConfigurarColumnasVenta();
            CargarTodosLosProductos();
            AplicarEstiloControles(this);

            txtBuscarProducto.TextChanged += txtBuscarProducto_TextChanged;
            DataProducto.CellDoubleClick += DataProducto_CellDoubleClick;
        }

   
        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarTodosLosProductos()
        {
            DataProducto.Rows.Clear();
            DataProducto.AllowUserToAddRows = false;

            var productos = _productoRepository.ObtenerTodos();
            var categorias = _categoriaRepository.ObtenerTodos();

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
            dgvVentas.Columns.Add("IdProducto", "IdProducto"); // <- Agrega esta línea
           // dgvVentas.Columns["IdProducto"].Visible = false;   // <- Oculta la columna
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

                int stock = Convert.ToInt32(filaSeleccionada.Cells["colStock"].Value);
                if (stock <= 0)
                {
                    MessageBox.Show("Este producto no tiene stock disponible.", "Sin stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nombre = filaSeleccionada.Cells["colNombre"].Value?.ToString();
                decimal precio = Convert.ToDecimal(filaSeleccionada.Cells["colPrecio"].Value);
                int cantidad = 1;

                var producto = _productoRepository.ObtenerTodos()
                                .FirstOrDefault(p => p.Nombre == nombre);

                if (producto == null)
                    return;

                foreach (DataGridViewRow filaVenta in dgvVentas.Rows)
                {
                    if (filaVenta.Cells["IdProducto"].Value != null &&
                        filaVenta.Cells["IdProducto"].Value.Equals(producto.Id))
                    {
                        int cantidadActual = Convert.ToInt32(filaVenta.Cells["colCantidad"].Value);
                        cantidadActual += 1;
                        filaVenta.Cells["colCantidad"].Value = cantidadActual;

                        decimal subtotalNuevo = cantidadActual * precio;
                        filaVenta.Cells["colSubtotal"].Value = subtotalNuevo;

                        CalcularTotal();
                        return;
                    }
                }

                int filaNueva = dgvVentas.Rows.Add();
                dgvVentas.Rows[filaNueva].Cells["IdProducto"].Value = producto.Id;
                dgvVentas.Rows[filaNueva].Cells["colProducto"].Value = nombre;
                dgvVentas.Rows[filaNueva].Cells["colPrecio"].Value = precio;
                dgvVentas.Rows[filaNueva].Cells["colCantidad"].Value = cantidad;
                dgvVentas.Rows[filaNueva].Cells["colSubtotal"].Value = precio * cantidad;

                CalcularTotal();
            }
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
                var fila = dgvVentas.CurrentRow;

                if (fila.Cells["colCantidad"].Value != null &&
                    int.TryParse(fila.Cells["colCantidad"].Value.ToString(), out int cantidadActual))
                {
                    if (cantidadActual > 1)
                    {
                        // Restar 1 a la cantidad
                        cantidadActual--;
                        fila.Cells["colCantidad"].Value = cantidadActual;

                        // Recalcular subtotal
                        if (decimal.TryParse(fila.Cells["colPrecio"].Value?.ToString(), out decimal precio))
                        {
                            fila.Cells["colSubtotal"].Value = precio * cantidadActual;
                        }
                    }
                    else
                    {
                        // Si queda 1, eliminar la fila
                        dgvVentas.Rows.Remove(fila);
                    }
                    CalcularTotal(); // Recalcular total después de cualquier cambio jose_z
                }
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
                // Creacion de objeto necesario para guardar la venta jose_z

                Venta nuevaVenta = new Venta
                {
                    FechaVenta = DateTime.Now,
                    Total = dgvVentas.Rows.Cast<DataGridViewRow>()
                                .Sum(row => Convert.ToDecimal(row.Cells["colSubtotal"].Value)),

                    CedulaCliente = txtCedula.Text.Trim(),
                    NombreCliente = txtNombreCliente.Text.Trim(),
                    TelefonoCliente = txtTelefono.Text.Trim(),


                };

                // Validar datos básicos 
                if (string.IsNullOrEmpty(nuevaVenta.CedulaCliente) ||
                    string.IsNullOrEmpty(nuevaVenta.NombreCliente))
                {
                    MessageBox.Show("Debe ingresar la cédula y nombre del cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Agregar detalles de venta  natan_P
                nuevaVenta.DetalleVentas = new List<DetalleVenta>();
                foreach (DataGridViewRow row in dgvVentas.Rows)
                {

                    if (row.IsNewRow) continue;

                    DetalleVenta detalle = new DetalleVenta
                    {
                        ProductoId = Convert.ToInt32(row.Cells["IdProducto"].Value),
                        NombreProducto = row.Cells["colProducto"].Value.ToString(),
                        PrecioUnitario = Convert.ToDecimal(row.Cells["colPrecio"].Value),
                        Cantidad = Convert.ToInt32(row.Cells["colCantidad"].Value),
                        Subtotal = Convert.ToDecimal(row.Cells["colSubtotal"].Value)
                    };

                    nuevaVenta.DetalleVentas.Add(detalle);

                }


                bool guardado = _ventaRepository.Agregar(nuevaVenta);
                if (guardado)
                {
                    //  Descontar stock jose_z
                    foreach (var detalle in nuevaVenta.DetalleVentas)
                    {
                        var movimiento = new MovimientoInventario
                        {
                            IdProducto = detalle.ProductoId,
                            Tipo = "Salida",
                            Cantidad = detalle.Cantidad,
                            Fecha = DateTime.Now
                        };

                        inventarioLogica.ProcesarMovimiento(movimiento);
                    }

                    MessageBox.Show("Venta registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvVentas.Rows.Clear();
                    lblTotal.Text = "Total: $0.00";

                    txtCedula.Clear();
                    txtNombreCliente.Clear();
                    txtTelefono.Clear();

                    CargarTodosLosProductos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscarProducto.Text.Trim().ToLower();
            DataProducto.Rows.Clear();
            DataProducto.AllowUserToAddRows = false;

            var productos = _productoRepository.ObtenerTodos()
                .Where(p => p.Nombre.ToLower().Contains(filtro))
                .ToList();

            var categorias = _categoriaRepository.ObtenerTodos();

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
    }
    
}
        
    

