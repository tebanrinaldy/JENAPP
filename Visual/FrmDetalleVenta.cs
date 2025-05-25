using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dal;
using Entity;
using Entity.Entity;

namespace Visual
{
    public partial class FrmDetalleVenta : Form
    {
        private readonly VentaRepository _ventaRepo = new VentaRepository("User Id=jenapp;Password=jen123;Data Source=localhost:1521/XEPDB1");
        private readonly int _idVenta;

        public FrmDetalleVenta(int idVenta)
        {
            InitializeComponent();
            _idVenta = idVenta;
            CargarDetalleVenta();
        }

        private void CargarDetalleVenta()
        {
            Venta venta = _ventaRepo.ObtenerPorId(_idVenta);
            if (venta == null)
            {
                MessageBox.Show("No se pudo encontrar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Mostrar info del cliente y venta
            lblCliente.Text = $"Cliente: {venta.NombreCliente}";
            lblCedula.Text = $"Cédula: {venta.CedulaCliente}";
            lblTelefono.Text = $"Teléfono: {venta.TelefonoCliente}";
            lblFecha.Text = $"Fecha: {venta.FechaVenta:dd/MM/yyyy HH:mm}";
            lblTotal.Text = $"Total: ${venta.Total:F2}";

            // Cargar los detalles al DataGridView
            dgvDetalle.Columns.Clear();
            dgvDetalle.Columns.Add("colProducto", "Producto");
            dgvDetalle.Columns.Add("colPrecio", "Precio Unitario");
            dgvDetalle.Columns.Add("colCantidad", "Cantidad");
            dgvDetalle.Columns.Add("colSubtotal", "Subtotal");

            foreach (var detalle in venta.DetalleVentas)
            {
                dgvDetalle.Rows.Add(
                    detalle.NombreProducto,
                    detalle.PrecioUnitario.ToString("F2"),
                    detalle.Cantidad,
                    (detalle.PrecioUnitario * detalle.Cantidad).ToString("F2")
                );
            }
        }

    }
}
