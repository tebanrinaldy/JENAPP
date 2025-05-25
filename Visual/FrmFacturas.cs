using Dal;
using Entity;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual
{
    public partial class FrmFacturas: Form
    {
        
        private readonly VentaRepository _ventaRepo = new VentaRepository("User Id=jenapp;Password=jen123;Data Source=localhost:1521/XEPDB1");
        private List<Venta> ventasDelDia;

        public FrmFacturas()
        {
            InitializeComponent();
        }

        private void FrmFacturas_Load(object sender, EventArgs e)
        {
            CargarVentasDelDia();
        }

        private void CargarVentasDelDia()
        {
            dgvVentasDia.Rows.Clear();
            dgvVentasDia.Columns.Clear();

            // Configurar columnas
            dgvVentasDia.Columns.Add("Id", "ID");
            dgvVentasDia.Columns.Add("NombreCliente", "Cliente");
            dgvVentasDia.Columns.Add("FechaVenta", "Fecha");
            dgvVentasDia.Columns.Add("Total", "Total");

            ventasDelDia = _ventaRepo.ObtenerTodos()
                .Where(v => v.FechaVenta.Date == DateTime.Today.Date)
                .ToList();

            foreach (var venta in ventasDelDia)
            {
                dgvVentasDia.Rows.Add(venta.Id, venta.NombreCliente, venta.FechaVenta.ToString("dd/MM/yyyy HH:mm"), $"${venta.Total:F2}");
            }
        }


      private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            if (dgvVentasDia.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una venta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idVenta = Convert.ToInt32(dgvVentasDia.CurrentRow.Cells["Id"].Value);
            Venta venta = _ventaRepo.ObtenerPorId(idVenta);

            if (venta == null)
            {
                MessageBox.Show("No se encontró la venta seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "Guardar Factura",
                FileName = $"Factura_{venta.Id}_{DateTime.Now:yyyyMMddHHmmss}.pdf"
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                GenerarFacturaPDF(venta, saveFile.FileName);
                MessageBox.Show("Factura generada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void GenerarFacturaPDF(Venta venta, string ruta)
        {
            Document doc = new Document(PageSize.A4);
            PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
            doc.Open();

            var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var fontBody = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            // ✅ Insertar el logo
            string rutaLogo = Path.Combine(Application.StartupPath, "Resources", "LogoJenapp.png");
            if (File.Exists(rutaLogo))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(rutaLogo);
                logo.ScaleAbsolute(80f, 80f);
                logo.Alignment = Element.ALIGN_LEFT;
                doc.Add(logo);
            }

            // Encabezado
            Paragraph titulo = new Paragraph("Factura de Venta", fontTitle);
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);

            doc.Add(new Paragraph($"Fecha: {venta.FechaVenta:dd/MM/yyyy HH:mm}", fontBody));
            doc.Add(new Paragraph($"Cliente: {venta.NombreCliente}", fontBody));
            doc.Add(new Paragraph($"Cédula: {venta.CedulaCliente}", fontBody));
            doc.Add(new Paragraph($"Teléfono: {venta.TelefonoCliente}", fontBody));
            doc.Add(new Paragraph(" "));

            // Tabla de detalles
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 40f, 20f, 20f, 20f });

            table.AddCell(new PdfPCell(new Phrase("Producto", fontHeader)));
            table.AddCell(new PdfPCell(new Phrase("Precio", fontHeader)));
            table.AddCell(new PdfPCell(new Phrase("Cantidad", fontHeader)));
            table.AddCell(new PdfPCell(new Phrase("Subtotal", fontHeader)));

            foreach (var d in venta.DetalleVentas)
            {
                table.AddCell(new Phrase(d.NombreProducto, fontBody));
                table.AddCell(new Phrase(d.PrecioUnitario.ToString("F2"), fontBody));
                table.AddCell(new Phrase(d.Cantidad.ToString(), fontBody));
                table.AddCell(new Phrase((d.PrecioUnitario * d.Cantidad).ToString("F2"), fontBody));
            }

            PdfPCell cellTotal = new PdfPCell(new Phrase($"Total: ${venta.Total:F2}", fontHeader));
            cellTotal.Colspan = 4;
            cellTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cellTotal);

            doc.Add(table);
            doc.Close();
        }
    }
}
