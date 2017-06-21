using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MttoVentas.Negocio;

namespace MttoVentas.GUIs
{
    public partial class frmRefolio : Form
    {
        public string _serie;
        public string _nvoFolio;
        private IConsultasSSNegocio _consultasSSNegocio;

        private bool _closeButton = false;

        public frmRefolio()
        {
            InitializeComponent();

            this._consultasSSNegocio = new ConsultasSSNegocio();
        }

        private void frmRefolio_Load(object sender, EventArgs e)
        {
            try
            {
                // obtiene el ultimo folio
                string folio = this._consultasSSNegocio.getUltFolio(this._serie);

                this.tbFolioNvo.Text = folio;
                this.ActiveControl = tbFolioNvo;
                this.tbFolioNvo.SelectAll();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal costo = 0;

                if (!decimal.TryParse(this.tbFolioNvo.Text, out costo))
                    throw new Exception("Folio no válido, asegúrese que el valor sea numérico.");

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this._closeButton = true;
                this._nvoFolio = this.tbFolioNvo.Text;
                this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Si cancela la operación no se efectuarán los cambios ya definidos\n" + 
                "¿Desea continuar?", "Mantenimiento de Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this._closeButton = true;
                this.Close();
            }
        }

        private void frmRefolio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this._closeButton)
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Si cancela la operación no se efectuarán los cambios ya definidos\n" +
                    "¿Desea continuar?", "Mantenimiento de Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (dialogResult == DialogResult.Yes)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();

                    e.Cancel = true;
                }
                else
                    e.Cancel = false;
            }
        }

        private void tbFolioNvo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnAceptar_Click(null, null);
            }
        }
    }
}
