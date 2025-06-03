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
using System.Net.Mail;
using Bll.Bll;

namespace Visual
{
    public partial class FrmFacturas: FrmBase
    {

        private readonly VentaRepository _ventaRepository = new VentaRepository();
        private readonly VentaService _ventaService;
        private List<Venta> ventasDelDia;


        public FrmFacturas()
        {
            InitializeComponent();
            _ventaService = new VentaService(_ventaRepository);

            AplicarEstiloControles(this);   
        }

        private void FrmFacturas_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;
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

            ventasDelDia = _ventaRepository.ObtenerTodos()
     .Where(v => v.FechaVenta.Date == DateTime.Today.Date &&
                 !string.IsNullOrWhiteSpace(v.TelefonoCliente))
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
            Venta venta = _ventaRepository.ObtenerPorId(idVenta);

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

                // 📨 Enviar por correo si hay correo
                if (!string.IsNullOrWhiteSpace(txtCorreoCliente.Text))
                {
                    EnviarFacturaPorCorreo(saveFile.FileName, txtCorreoCliente.Text.Trim());
                }
            }
        }

        private void GenerarFacturaPDF(Venta venta, string ruta)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4, 40f, 40f, 40f, 60f); // margen inferior para pie
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
            doc.Open();

            // Fuentes y colores
            var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
            var fontSub = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.DARK_GRAY);
            var fontBody = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
            var fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK);
            var fontFooter = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9, BaseColor.GRAY);
            BaseColor headerColor = new BaseColor(230, 230, 230);
               
            // ✅ Logo
            string rutaLogo = Path.Combine(Application.StartupPath, "Resources", "LogoJenapp.png");
            if (File.Exists(rutaLogo))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(rutaLogo);
                logo.ScaleAbsolute(70f, 70f);
                logo.Alignment = Element.ALIGN_LEFT;
                doc.Add(logo);
            }

            // Título
            Paragraph titulo = new Paragraph("Factura de Venta", fontTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 10f
            };
            doc.Add(titulo);

            // Info del cliente
            doc.Add(new Paragraph($"Fecha: {venta.FechaVenta:dd/MM/yyyy HH:mm}", fontBody));
            doc.Add(new Paragraph($"Cliente: {venta.NombreCliente}", fontBody));
            doc.Add(new Paragraph($"Cédula: {venta.CedulaCliente}", fontBody));
            string telefono = string.IsNullOrWhiteSpace(venta.TelefonoCliente) ? "No registrado" : venta.TelefonoCliente;
            doc.Add(new Paragraph($"Teléfono: {telefono}", fontBody));
            doc.Add(new Paragraph(" "));

            // Tabla de productos
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 45f, 15f, 15f, 25f });

            string[] headers = { "Producto", "Precio", "Cantidad", "Subtotal" };
            foreach (string h in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(h, fontHeader))
                {
                    BackgroundColor = headerColor,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5f
                };
                table.AddCell(cell);
            }

            foreach (var d in venta.DetalleVentas)
            {
                table.AddCell(new Phrase(d.NombreProducto, fontBody));
                table.AddCell(new Phrase($"${d.PrecioUnitario:F2}", fontBody));
                table.AddCell(new Phrase(d.Cantidad.ToString(), fontBody));
                table.AddCell(new Phrase($"${(d.PrecioUnitario * d.Cantidad):F2}", fontBody));
            }

            PdfPCell cellTotal = new PdfPCell(new Phrase($"Total: ${venta.Total:F2}", fontSub))
            {
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                BackgroundColor = new BaseColor(245, 245, 245),
                PaddingTop = 5f,
                PaddingBottom = 5f
            };
            table.AddCell(cellTotal);

            doc.Add(table);

            // Pie de página
            Paragraph linea = new Paragraph("_______________________________________________", fontFooter)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 25f
            };
            doc.Add(linea);

            Paragraph footer = new Paragraph("Sistema de Facturación Jenapp - Contacto: +57 312 338 38 21", fontFooter)
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(footer);

            // ✅ Agradecimiento
            Paragraph gracias = new Paragraph("¡Gracias por su compra!", fontFooter)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 5f
            };
            doc.Add(gracias);

            // ❗ IMPORTANTE: cerrar el documento para evitar archivos dañados
            doc.Close();
        }
        private void EnviarFacturaPorCorreo(string rutaFactura, string correoDestino)
        {
            try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("jenapp2402@gmail.com"); // Reemplaza con tu correo
                correo.To.Add(correoDestino);
                correo.Subject = "Factura de su compra - Jenapp";
                correo.Body = "Adjunto encontrará la factura de su compra.\n\nGracias por confiar en nosotros.";
                correo.Attachments.Add(new Attachment(rutaFactura));

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential("jenapp2402@gmail.com", "skfc nyex apsm bhdk"); // Usa contraseña de aplicación si es Gmail
                smtp.EnableSsl = true;

                smtp.Send(correo);
                MessageBox.Show("Factura enviada por correo.", "Correo enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el correo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnBuscarVentas_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            dgvVentasDia.Rows.Clear();
            dgvVentasDia.Columns.Clear();

            dgvVentasDia.Columns.Add("Id", "ID");
            dgvVentasDia.Columns.Add("NombreCliente", "Cliente");
            dgvVentasDia.Columns.Add("FechaVenta", "Fecha");
            dgvVentasDia.Columns.Add("Total", "Total");

            ventasDelDia = _ventaRepository.ObtenerTodos()
                .Where(v => v.FechaVenta.Date >= desde &&
                            v.FechaVenta.Date <= hasta &&
                            !string.IsNullOrWhiteSpace(v.TelefonoCliente))
                .ToList();

            foreach (var venta in ventasDelDia)
            {
                dgvVentasDia.Rows.Add(venta.Id, venta.NombreCliente,
                    venta.FechaVenta.ToString("dd/MM/yyyy HH:mm"), $"${venta.Total:F2}");
            }
        }
       
    }
}
