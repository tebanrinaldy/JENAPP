using Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using ClosedXML.Excel;
using System.Drawing.Drawing2D;

namespace Visual
{
    public partial class FrmReportes : FrmBase
    {
        private readonly VentaRepository _ventaRepository;
        public FrmReportes()
        {
            
            InitializeComponent();
            AplicarEstiloControles(this);
            _ventaRepository = new VentaRepository("User Id=jenapp;Password=jen123;Data Source=localhost:1521/XEPDB1");
        }



        private void FrmReportes_Load(object sender, EventArgs e)
        {

            CargarVentas();
        }

        private void CargarVentas()
        {
            {
                try
                {
                    dgvVentas.Rows.Clear(); // Limpia el contenido anterior

                    var ventas = _ventaRepository.ObtenerTodos();

                    if (ventas.Count == 0)
                    {
                        MessageBox.Show("No hay ventas registradas.");
                        return;
                    }

                    foreach (var venta in ventas)
                    {
                        int index = dgvVentas.Rows.Add();
                        dgvVentas.Rows[index].Cells["colId"].Value = venta.Id;
                        dgvVentas.Rows[index].Cells["colFecha"].Value = venta.FechaVenta.ToShortDateString();
                        dgvVentas.Rows[index].Cells["colTotal"].Value = venta.Total;
                        dgvVentas.Rows[index].Cells["colCedula"].Value = venta.CedulaCliente;
                        dgvVentas.Rows[index].Cells["colNombre"].Value = venta.NombreCliente;
                        dgvVentas.Rows[index].Cells["colTelefono"].Value = venta.TelefonoCliente;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar ventas: " + ex.Message);
                }
            }
        }
     

        
        private void ExportarAExcel(DataGridView dgv)
        {
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Archivo Excel|*.xlsx",
                Title = "Guardar como Excel"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var wb = new XLWorkbook();
                    var ws = wb.Worksheets.Add("Reporte");

                    // Escribir encabezados
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        ws.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                    }

                    // Escribir filas
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            ws.Cell(i + 2, j + 1).Value = dgv.Rows[i].Cells[j].Value?.ToString();
                        }
                    }

                    wb.SaveAs(sfd.FileName);
                    MessageBox.Show("¡Exportado exitosamente!", "Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportarAExcel(dgvVentas);
        } 
    }
}
    
    

