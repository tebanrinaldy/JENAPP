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
    public partial class FrmBase : Form
    {
        public FrmBase()
        {
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.Font = new Font("Segoe UI", 10);
            this.ForeColor = Color.White;

            // Aplicar estilo automáticamente al iniciar
            AplicarEstiloControles(this);
        }

        #region Estilo moderno para controles

        public void AplicarEstiloControles(Control parent)
        {
            EstilizarTextBoxes(parent);
            EstilizarBotones(parent);
            EstilizarComboBoxes(parent);
            EstilizarListBoxes(parent);
            EstilizarDataGrids(parent);
        }

        protected void EstilizarTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.FromArgb(60, 63, 65);
                    textBox.ForeColor = Color.White;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }

                if (control.HasChildren)
                {
                    EstilizarTextBoxes(control);
                }
            }
        }

        protected void EstilizarBotones(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.FromArgb(58, 123, 213);
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    btn.Cursor = Cursors.Hand;
                }

                if (control.HasChildren)
                {
                    EstilizarBotones(control);
                }
            }
        }

        protected void EstilizarComboBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox combo)
                {
                    combo.BackColor = Color.FromArgb(60, 63, 65);
                    combo.ForeColor = Color.White;
                    combo.FlatStyle = FlatStyle.Flat;
                    combo.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }

                if (control.HasChildren)
                {
                    EstilizarComboBoxes(control);
                }
            }
        }

        protected void EstilizarListBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ListBox listBox)
                {
                    listBox.BackColor = Color.FromArgb(60, 63, 65);
                    listBox.ForeColor = Color.White;
                    listBox.BorderStyle = BorderStyle.FixedSingle;
                    listBox.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }

                if (control.HasChildren)
                {
                    EstilizarListBoxes(control);
                }
            }
        }
        protected void EstilizarDataGrids(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.FromArgb(60, 63, 65);
                    dgv.GridColor = Color.FromArgb(80, 80, 80);
                    dgv.BorderStyle = BorderStyle.None;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dgv.DefaultCellStyle.BackColor = Color.FromArgb(60, 63, 65);
                    dgv.DefaultCellStyle.ForeColor = Color.White;
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(33, 150, 243);
                    dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgv.RowHeadersVisible = false;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }

                if (control.HasChildren)
                {
                    EstilizarDataGrids(control);
                }
            }
        }

        #endregion
    }
}

