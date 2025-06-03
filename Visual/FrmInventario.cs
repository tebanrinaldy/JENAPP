using Bll;
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
using Dal;
using DocumentFormat.OpenXml.Office2010.Excel;


namespace Visual
{
    public partial class FrmInventario: FrmBase

    {
        private readonly MovimientoInventarioDAO _movimientoInventarioDAO = new MovimientoInventarioDAO();
        private readonly InventarioLogica _inventarioLogica = new InventarioLogica();
        private readonly ProductoRepository _productoRepository = new ProductoRepository();
        public FrmInventario()
        {
            
            InitializeComponent();
            AplicarEstiloControles(this);
            cmbTipo.Items.AddRange(new string[] { "Entrada", "Salida" });
            cmbTipo.SelectedIndex = 0;
            txtIdProducto.TextChanged += txtIdProducto_TextChanged;
            CargarProductos();
            dgvProductos.CellClick += dgvProductos_CellClick;
        }
        private void txtIdProducto_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtIdProducto.Text, out int idProd))
            {
                try
                {
                    int stock = _movimientoInventarioDAO.ObtenerStockActual(idProd);
                    lblStock.Text = $"Stock actual: {stock}";
                }
                catch
                {
                    lblStock.Text = "Producto no encontrado.";
                }
            }
            else
            {
                lblStock.Text = "Stock actual: —";
            }
        }

        private void CargarProductos() //obtener todos los productos y mostrarlos en el Dvg Jose_Z
        {
            var productos = _productoRepository.ObtenerTodos();
            dgvProductos.DataSource = productos
                .Select(p => new {
                    ID = p.Id,
                    Nombre = p.Nombre,
                    Stock = p.Stock,
                    Precio = p.Precio,
                    Categoria = p.Categoria.Nombre
                })
                .ToList();
        }
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvProductos.Rows[e.RowIndex];
                txtIdProducto.Text = row.Cells["ID"].Value.ToString();
            }
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtIdProducto.Text, out int idProd))
                    throw new Exception("ID de producto inválido.");

                if (!int.TryParse(txtCantidad.Text, out int cantidad))
                    throw new Exception("Cantidad inválida.");

                var movimiento = new MovimientoInventario
                {
                    IdProducto = idProd,
                    Tipo = cmbTipo.SelectedItem.ToString(),
                    Cantidad = cantidad,
                    Fecha = DateTime.Now
                };

                _inventarioLogica.ProcesarMovimiento(movimiento);

                MessageBox.Show("Movimiento registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtIdProducto.Clear();
            txtCantidad.Clear();
            cmbTipo.SelectedIndex = 0;
        }

        private void btnRegistrar_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Click detectado");

            try
            {
                if (!int.TryParse(txtIdProducto.Text, out int idProd))
                    throw new Exception("ID de producto inválido.");

                if (!int.TryParse(txtCantidad.Text, out int cantidad))
                    throw new Exception("Cantidad inválida.");

                var movimiento = new MovimientoInventario
                {
                    IdProducto = idProd,
                    Tipo = cmbTipo.SelectedItem.ToString(),
                    Cantidad = cantidad,
                    Fecha = DateTime.Now
                };

                _inventarioLogica.ProcesarMovimiento(movimiento);

                MessageBox.Show("Movimiento registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

