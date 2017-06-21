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
    public partial class frmFolios : Form
    {
        private IConsultasSSNegocio _consultasSSNegocio;
        private IConsultasMySQLNegocio _consultasMySQLNegocio;

        public frmFolios()
        {
            InitializeComponent();
        }

        private void frmFolios_Load(object sender, EventArgs e)
        {
            try
            {
                this._consultasMySQLNegocio = new ConsultasMySQLNegocio();
                this._consultasSSNegocio = new ConsultasSSNegocio();

                this.rbTurno.Checked = true;

                // carga series
                List<Modelos.Folios> temp = this._consultasSSNegocio.obtFolios();
                List<Modelos.Folios> folios = new List<Modelos.Folios>();

                folios.Add(new Modelos.Folios { serie = "TODAS", ultimoFolio = "TODAS" });
                folios.AddRange(temp);

                this.cmbSerie.DataSource = folios;
                this.cmbSerie.DisplayMember = "serie";
                this.cmbSerie.ValueMember = "serie";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbTurno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.rbTurno.Checked)
                {
                    this.pTurno.Visible = true;
                    this.pPeriodo.Visible = false;

                    this._reiniciaValores();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.rbPeriodo.Checked)
                {
                    this.pTurno.Visible = false;
                    this.pPeriodo.Visible = true;

                    this._reiniciaValores();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void _reiniciaValores()
        {
            this.dtpPDAl.Value = DateTime.Today;
            this.dtpPDDel.Value = DateTime.Today;
            this.dtpTDDel.Value = DateTime.Today;

            this.dtpPTDel.Value = new DateTime(1990, 01, 01, 0, 0, 0);
            this.dtpPTAl.Value = new DateTime(1990, 01, 01, 23, 59, 59);
        }

        private void btnCerrrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rbTurno.Checked)
                {
                    string fechaIni = this.dtpTDDel.Value.AddHours(6).ToString("yyyy-MM-ddTHH:mm:ss");
                    string fechaFin = this.dtpTDDel.Value.AddDays(1).AddHours(5).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-ddTHH:mm:ss");

                    string serie = ((Modelos.Folios)this.cmbSerie.SelectedItem).serie;

                    List<Modelos.Cuentas> cuentas = this._consultasSSNegocio.getCuentas(fechaIni, fechaFin, serie, Modelos.Login.estacion);

                    if (cuentas.Count == 0)
                    {
                        this._cancela();
                        throw new Exception("Sin resultados");
                    }

                    this.dgvResultado.DataSource = cuentas;

                    this.cbMDTodos.Checked = true;
                }

                if (this.rbPeriodo.Checked)
                {
                    string fechaIni = (this.dtpPDDel.Value.Add(this.dtpPTDel.Value.TimeOfDay)).ToString("yyyy-MM-ddTHH:mm:ss");

                    string fechaFin = (this.dtpPDAl.Value.Add(this.dtpPTAl.Value.TimeOfDay)).ToString("yyyy-MM-ddTHH:mm:ss");

                    string serie = ((Modelos.Folios)this.cmbSerie.SelectedItem).serie;

                    List<Modelos.Cuentas> cuentas = this._consultasSSNegocio.getCuentas(fechaIni, fechaFin, serie, Modelos.Login.estacion);

                    if (cuentas.Count == 0)
                    {
                        this._cancela();
                        throw new Exception("Sin resultados");
                    }

                    this.dgvResultado.DataSource = cuentas;
                    this.cbMDTodos.Checked = true;
                }

                // formato al grid
                this._formatoGrid();

                this._calculos_pUno();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void _formatoGrid()
        {
            this.dgvResultado.Columns["turno"].HeaderText = "Turno";
            this.dgvResultado.Columns["folio"].HeaderText = "";
            this.dgvResultado.Columns["serieFolio"].HeaderText = "";
            this.dgvResultado.Columns["folioCuenta"].HeaderText = "Folio Cuenta";
            this.dgvResultado.Columns["folioNotaConsumo"].HeaderText = "Folio Nota Consumo";
            this.dgvResultado.Columns["fecha"].HeaderText = "Fecha";
            this.dgvResultado.Columns["cancelado"].HeaderText = "Cancelado";
            this.dgvResultado.Columns["facturado"].HeaderText = "Facturado";
            this.dgvResultado.Columns["descuento"].HeaderText = "Descuento %";
            this.dgvResultado.Columns["totalOriginal"].HeaderText = "Total Original";
            this.dgvResultado.Columns["productosEliminados"].HeaderText = "Productos Eliminados";
            this.dgvResultado.Columns["totalDesc"].HeaderText = "Total Descuentos";
            this.dgvResultado.Columns["efectivo"].HeaderText = "Efectivo";
            this.dgvResultado.Columns["tarjeta"].HeaderText = "Tarjeta";
            this.dgvResultado.Columns["vales"].HeaderText = "Vales";
            this.dgvResultado.Columns["otros"].HeaderText = "Otros";
            this.dgvResultado.Columns["eliminar"].HeaderText = "Eliminar";
            this.dgvResultado.Columns["modificar"].HeaderText = "Modificar";

            this.dgvResultado.Columns["folio"].Visible = false;
            this.dgvResultado.Columns["totalDesc"].Visible = false;
            this.dgvResultado.Columns["modificar"].Visible = false;


            this.dgvResultado.Columns["productosEliminados"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvResultado.Columns["totalOriginal"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvResultado.Columns["totalDesc"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvResultado.Columns["folioNotaConsumo"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            this.dgvResultado.Columns["turno"].Width = 60;
            this.dgvResultado.Columns["serieFolio"].Width = 20;
            this.dgvResultado.Columns["folioCuenta"].Width = 60;
            this.dgvResultado.Columns["folioNotaConsumo"].Width = 65;
            this.dgvResultado.Columns["fecha"].Width = 120;
            this.dgvResultado.Columns["cancelado"].Width = 60;
            this.dgvResultado.Columns["facturado"].Width = 60;
            this.dgvResultado.Columns["descuento"].Width = 60;
            this.dgvResultado.Columns["totalOriginal"].Width = 60;
            this.dgvResultado.Columns["productosEliminados"].Width = 60;
            this.dgvResultado.Columns["efectivo"].Width = 60;
            this.dgvResultado.Columns["tarjeta"].Width = 60;
            this.dgvResultado.Columns["vales"].Width = 60;
            this.dgvResultado.Columns["otros"].Width = 60;
            this.dgvResultado.Columns["eliminar"].Width = 50;

            foreach (DataGridViewColumn column in this.dgvResultado.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            this.dgvResultado.Columns["turno"].ReadOnly = true;
            this.dgvResultado.Columns["serieFolio"].ReadOnly = true;
            this.dgvResultado.Columns["folioCuenta"].ReadOnly = true;
            this.dgvResultado.Columns["folioNotaConsumo"].ReadOnly = true;
            this.dgvResultado.Columns["fecha"].ReadOnly = true;
            this.dgvResultado.Columns["cancelado"].ReadOnly = true;
            this.dgvResultado.Columns["facturado"].ReadOnly = true;
            this.dgvResultado.Columns["descuento"].ReadOnly = true;
            this.dgvResultado.Columns["totalOriginal"].ReadOnly = true;
            this.dgvResultado.Columns["productosEliminados"].ReadOnly = true;
            this.dgvResultado.Columns["efectivo"].ReadOnly = true;
            this.dgvResultado.Columns["tarjeta"].ReadOnly = true;
            this.dgvResultado.Columns["vales"].ReadOnly = true;
            this.dgvResultado.Columns["otros"].ReadOnly = true;

            // colorea los lque se puedan eliminar
            if (this.dgvResultado.Rows.Count > 0)
            {
                for (int i = 0; i < this.dgvResultado.Rows.Count; i++)
                {
                    bool cell = (Boolean)this.dgvResultado.Rows[i].Cells["eliminar"].Value;

                    if (cell)
                    {
                        var row = this.dgvResultado.Rows[i];
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        var row = this.dgvResultado.Rows[i];
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }

        }

        private void _calculos_pUno()
        {
            // calculos
            // cuantos resultados arrojo la consulta
            int countReg = this.dgvResultado.RowCount;

            // a modificar
            List<Modelos.Cuentas> ctas = (List<Modelos.Cuentas>)this.dgvResultado.DataSource;
            int seleccionados = ctas.Where(w => w.eliminar == true).Count();

            // importe anterior
            // suma de todos los totales originales
            decimal impAnt = ctas.Sum(s => s.totalOriginal);

            // importe nuevo
            // total original sumado de las casillas sin activar
            decimal impNvo = ctas.Where(w => w.eliminar == false).Sum(s => s.totalOriginal);

            // diferencia
            // total original sumado de las casillas activadas
            decimal dif = ctas.Where(w => w.eliminar == true).Sum(s => s.totalOriginal);

            // procentaje
            decimal porcentaje = seleccionados == 0 ? 0 : ((dif * 100) / impAnt);

            // articulos a eliminar
            decimal artElim = 0;

            // efectivos anterior
            // suma de los efectivos totales
            decimal efecAnt = ctas.Sum(s => s.efectivo);

            // efectivo nuevo
            // total efectivo NO seleccionados
            decimal efecNvo = ctas.Where(w => w.eliminar == false).Sum(s => s.efectivo);

            // efectivo en caja
            // sin uso (al momento)
            decimal efecCaja = 0;

            // asigna Valores
            // this.tbNCT.Text = string.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", countReg);
            /*
            this.tbNCT.Text = Convert.ToString(countReg);
            this.tbNCM.Text = Convert.ToString(seleccionados);
            this.tbIA.Text = Convert.ToString(impAnt);
            this.tbIN.Text = Convert.ToString(impNvo);
            this.tbD.Text = Convert.ToString(dif);
            this.tbNAE.Text = Convert.ToString(artElim);
            this.tbEA.Text = Convert.ToString(efecAnt);
            this.tbEN.Text = Convert.ToString(efecNvo);
            this.tbEC.Text = Convert.ToString(efecCaja);
            */
            this.tbNCT.Text = Convert.ToString(countReg);
            this.tbNCM.Text = Convert.ToString(seleccionados);
            this.tbIA.Text = string.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", impAnt);
            this.tbIN.Text = string.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", impNvo);
            this.tbD.Text = string.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", dif);
            this.tbNAE.Text = Convert.ToString(artElim);
            this.tbDP.Text = Convert.ToString(Math.Round(porcentaje, 2));
            this.tbEA.Text = string.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", efecAnt);
            this.tbEN.Text = string.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", efecNvo);
            this.tbEC.Text = string.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", efecCaja);
        }

        /// <summary>
        /// To handle the DatGridViews CheckedChanged event you must first get the CellContentClick to fire 
        /// (which does not have the CheckBoxes current state!) then call CommitEdit. 
        /// This will in turn fire the CellValueChanged event which you can use to do your work. This is an oversight by Microsoft. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvResultado.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        /// <summary>
        /// Works with the above.
        /// </summary>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    bool cell = (Boolean)this.dgvResultado.Rows[e.RowIndex].Cells["eliminar"].Value;

                    if (cell)
                    {
                        var row = this.dgvResultado.Rows[e.RowIndex];
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        var row = this.dgvResultado.Rows[e.RowIndex];
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }

                this._calculos_pUno();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvResultado_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dgvResultado.IsCurrentCellDirty)
                this.dgvResultado.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void tbCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this._cancela();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void _cancela()
        {
            this.frmFolios_Load(null, null);

            this.dgvResultado.DataSource = null;

            foreach (Control x in this.panel1.Controls.Cast<Control>())
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Text = string.Empty;
                }
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                // validaciones
                if (this.dgvResultado.Rows.Count == 0)
                    throw new Exception("Realice una consulta");

                List<Modelos.Cuentas> ctas = (List<Modelos.Cuentas>)this.dgvResultado.DataSource;
                int seleccionados = ctas.Where(w => w.eliminar == true).Count();

                if (seleccionados == 0)
                    throw new Exception("Seleccione al menos un Folio");

                // mensage de confirmacion
                DialogResult dialogResult = MessageBox.Show(
                    "Se realizarán cambios permanentes\n" +
                    "¿Desea continuar?", "Mantenimiento de Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult != DialogResult.Yes)
                {
                    MessageBox.Show("Operación Cancelada", "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this._cancela();
                    return;
                }

                // obtiene las series diferentes
                var series = from ct in ctas
                             group ct by ct.serieFolio into g
                             select g;

                foreach (var serie in series)
                {
                    // seleccionamos todos los registros por folio
                    List<Modelos.Cuentas> ctasSel = ctas.Where(w => w.serieFolio.Equals(serie.Key)).Select(s => s).ToList();

                    // seleccionamos los folios a eliminar, sin contar los facturados
                    List<long> folios = ctasSel.Where(w => w.eliminar == true && w.facturado == false).Select(s => s.folio).ToList();
                    
                    // folios a eliminar facturados
                    List<long> foliosFact = ctasSel.Where(w => w.eliminar == true && w.facturado == true).Select(s => s.folio).ToList();

                    if (folios.Count == 0)
                        if(foliosFact.Count == 0)
                            continue;

                    // CAMBIAR FOLIOS (refoliar)
                    List<long> refolios = ctasSel.Where(w => !folios.Contains(w.folio)).Select(s => s.folio).OrderBy(o => o).ToList();

                    // quitar los folios a refacturar
                    refolios = refolios.Where(w => !foliosFact.Contains(w)).ToList();

                    // almacena nuevo folio
                    string nvoFolio = string.Empty;

                    if (refolios.Count != 0)
                    {
                        frmRefolio form = new frmRefolio();

                        form._serie = serie.Key;

                        MessageBox.Show(
                            "Defina el folio inicial para renumerar la serie '" + serie.Key +
                            "'. Se muestra cargado un número sugerido en base al último folio a la fecha",
                            "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        var respuesta = form.ShowDialog();

                        if (respuesta == System.Windows.Forms.DialogResult.OK)
                        {
                            nvoFolio = form._nvoFolio;
                        }
                        else
                            throw new Exception("Operación Cancelada");
                    }

                    // obtener los registros a respaldar
                    // SOLO SE REGISTRAN LOS QUE VAN A SUFRIR CAMBIOS, ELIMINACION POR EJEMPLO
                    // CHEQUESPAGOS
                    List<Modelos.TB_Chequespagos> chequesPagos = this._consultasSSNegocio.getChequesPagos(folios);

                    // CHEQDET
                    List<Modelos.TB_Cheqdet> cheqdet = this._consultasSSNegocio.getCheqdet(Modelos.Login.estacion, folios);

                    // CHEQUES (eliminacion)
                    List<Modelos.TB_Cheques> cheques = this._consultasSSNegocio.getCheques(Modelos.Login.estacion, folios);

                    // CHEQUES (cambio folio)
                    List<Modelos.TB_Cheques> chequesF = this._consultasSSNegocio.getCheques(Modelos.Login.estacion, refolios);

                    // CHEQUES (cambio folio y serie, facturados)
                    List<Modelos.TB_Cheques> chequesFF = this._consultasSSNegocio.getCheques(Modelos.Login.estacion, foliosFact);

                    // FOLIOS
                    Modelos.TB_Folios tb_fol = this._consultasSSNegocio.getFolio(serie.Key);

                    /* respaldar folios, guardarlos en base de datos en linea */
                    // obtener ultimo movimiento
                    long mvto = this._consultasMySQLNegocio.getUltMvto();

                    // respaldo
                    this._consultasMySQLNegocio.insertRespCheques(
                        cheques, folios, 
                        chequesF, refolios, 
                        chequesFF, foliosFact,
                        cheqdet, chequesPagos, 
                        tb_fol, mvto + 1);

                    // cambiar facturados
                    
                    bool res = this._consultasSSNegocio.cambiaFolioFacturados(Modelos.Login.series[1], foliosFact, Modelos.Login.estacion);

                    // eliminar folios y refoliar
                    bool resultado = this._consultasSSNegocio.elimFolios(folios, Modelos.Login.estacion, refolios, nvoFolio, serie.Key);

                }

                MessageBox.Show("Proceso Concluido", "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this._cancela();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cbMDTodos_CheckedChanged(object sender, EventArgs e)
        {
            bool check = this.cbMDTodos.Checked;

            if (this.dgvResultado.RowCount == 0) return;

            List<Modelos.Cuentas> ctas = (List<Modelos.Cuentas>)this.dgvResultado.DataSource;

            List<Modelos.Cuentas> nuevas = new List<Modelos.Cuentas>();

            foreach (Modelos.Cuentas c in ctas)
            {
                nuevas.Add(new Modelos.Cuentas
                {
                    cancelado = c.cancelado,
                    descuento = c.descuento,
                    efectivo = c.efectivo,
                    eliminar = check,
                    facturado = c.facturado,
                    fecha = c.fecha,
                    folio = c.folio,
                    folioCuenta = c.folioCuenta,
                    folioNotaConsumo = c.folioNotaConsumo,
                    modificar = c.modificar,
                    otros = c.otros,
                    productosEliminados = c.productosEliminados,
                    serieFolio = c.serieFolio,
                    tarjeta = c.tarjeta,
                    totalDesc = c.totalDesc,
                    totalOriginal = c.totalOriginal,
                    turno = c.turno,
                    vales = c.vales
                });
            }

            this.dgvResultado.DataSource = null;
            this.dgvResultado.DataSource = nuevas;

            // formato al grid
            this._formatoGrid();

            this._calculos_pUno();

        }
    }
}
