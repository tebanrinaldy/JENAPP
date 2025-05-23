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
                if (control is TextBox txt)
                {
                    Color baseColor = Color.FromArgb(60, 63, 65);
                    Color focusColor = Color.FromArgb(80, 90, 110);

                    txt.BackColor = baseColor;
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                    Timer animTimer = new Timer { Interval = 15 };
                    Color currentColor = baseColor;
                    Color targetColor = baseColor;

                    animTimer.Tick += (s, e) =>
                    {
                        currentColor = InterpolarColor(currentColor, targetColor, 0.1f);
                        txt.BackColor = currentColor;
                        if (ColoresIguales(currentColor, targetColor))
                            animTimer.Stop();
                    };

                    txt.GotFocus += (s, e) =>
                    {
                        targetColor = focusColor;
                        animTimer.Start();
                    };

                    txt.LostFocus += (s, e) =>
                    {
                        targetColor = baseColor;
                        animTimer.Start();
                    };
                }

                if (control.HasChildren)
                    EstilizarTextBoxes(control);
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

                        int targetPaddingLeft = 15;
                        int normalPaddingLeft = 5;
                        int step = 1;

                        Timer animTimer = new Timer { Interval = 10 };
                        bool animandoEntrada = false; // true si animando hover

                        animTimer.Tick += (s, e) =>
                        {
                            var currentPadding = btn.Padding;
                            if (animandoEntrada)
                            {
                                if (currentPadding.Left < targetPaddingLeft)
                                {
                                    btn.Padding = new Padding(currentPadding.Left + step, currentPadding.Top, currentPadding.Right, currentPadding.Bottom);
                                }
                                else
                                {
                                    animTimer.Stop();
                                }
                            }
                            else
                            {
                                if (currentPadding.Left > normalPaddingLeft)
                                {
                                    btn.Padding = new Padding(currentPadding.Left - step, currentPadding.Top, currentPadding.Right, currentPadding.Bottom);
                                }
                                else
                                {
                                    animTimer.Stop();
                                }
                            }
                        };

                        btn.MouseEnter += (s, e) =>
                        {
                            animandoEntrada = true;
                            animTimer.Start();
                        };

                        btn.MouseLeave += (s, e) =>
                        {
                            animandoEntrada = false;
                            animTimer.Start();
                        };
                    }

                    if (control.HasChildren)
                        EstilizarBotones(control);
                }
            }
        

        // Función para interpolar colores suavemente
        private Color InterpolarColor(Color colorActual, Color colorObjetivo, float paso)
        {   
            int r = (int)(colorActual.R + (colorObjetivo.R - colorActual.R) * paso);
            int g = (int)(colorActual.G + (colorObjetivo.G - colorActual.G) * paso);
            int b = (int)(colorActual.B + (colorObjetivo.B - colorActual.B) * paso);
            return Color.FromArgb(r, g, b);
        }

        // Función para comparar colores (puedes ajustar la tolerancia)
        private bool ColoresIguales(Color c1, Color c2)
        {
            return Math.Abs(c1.R - c2.R) < 2 &&
                   Math.Abs(c1.G - c2.G) < 2 &&
                   Math.Abs(c1.B - c2.B) < 2;
        }


        protected void EstilizarComboBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox combo)
                {
                    Color baseColor = Color.FromArgb(60, 63, 65);
                    Color focusColor = Color.FromArgb(80, 90, 110);

                    combo.BackColor = baseColor;
                    combo.ForeColor = Color.White;
                    combo.FlatStyle = FlatStyle.Flat;
                    combo.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                    Timer animTimer = new Timer { Interval = 15 };
                    Color currentColor = baseColor;
                    Color targetColor = baseColor;

                    animTimer.Tick += (s, e) =>
                    {
                        currentColor = InterpolarColor(currentColor, targetColor, 0.1f);
                        combo.BackColor = currentColor;
                        if (ColoresIguales(currentColor, targetColor))
                            animTimer.Stop();
                    };

                    combo.GotFocus += (s, e) =>
                    {
                        targetColor = focusColor;
                        animTimer.Start();
                    };

                    combo.LostFocus += (s, e) =>
                    {
                        targetColor = baseColor;
                        animTimer.Start();
                    };
                }

                if (control.HasChildren)
                    EstilizarComboBoxes(control);
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

