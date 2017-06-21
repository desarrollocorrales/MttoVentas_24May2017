using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using MttoVentas.GUIs;
using System.IO;
using MttoVentas.Negocio;

namespace MttoVentas
{
    public partial class FormPrincipal : Form
    {
        private bool _defConfig;
        private bool _ventasReal;

        private IConsultasSSNegocio _consultasSSNegocio;
        private IConsultasMySQLNegocio _consultasMySQLNegocio;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                // valida si ya tiene alguna clave guardada para el archivo
                string cveActual = Properties.Settings.Default.accesoConfig;

                if (string.IsNullOrEmpty(cveActual))
                {
                    string acceso = Modelos.Utilerias.Transform("p4ssw0rd");

                    Properties.Settings.Default.accesoConfig = acceso;
                    Properties.Settings.Default.Save();
                }

                string fileName = "config.dat";
                string pathConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Mantenimiento\";

                // si no existe el directorio, lo crea
                bool exists = System.IO.Directory.Exists(pathConfigFile);

                if (!exists) System.IO.Directory.CreateDirectory(pathConfigFile);

                // busca en el directorio si exite el archivo con el nombre dado
                var file = Directory.GetFiles(pathConfigFile, fileName, SearchOption.AllDirectories)
                        .FirstOrDefault();

                if (file == null)
                {
                    // no existe
                    // abrir el formulario para llenar la configuracion de conexion 
                    frmConfiguracion form = new frmConfiguracion();
                    var resultado = form.ShowDialog();

                    if (resultado != System.Windows.Forms.DialogResult.OK)
                    {
                        this._defConfig = false;
                        throw new Exception("No se ha definido la configuración");
                    }
                }

                file = Directory.GetFiles(pathConfigFile, fileName, SearchOption.AllDirectories)
                        .FirstOrDefault();

                // si existe
                // obtener la cadena de conexion del archivo
                FEncrypt.Respuesta result = FEncrypt.EncryptDncrypt.DecryptFile(file, "milagros");

                if (result.status == FEncrypt.Estatus.ERROR)
                    throw new Exception(result.error);

                string serie1 = string.Empty, serie2 = string.Empty, serie = string.Empty;

                if (result.status == FEncrypt.Estatus.OK)
                {
                    string[] list = result.resultado.Split(new string[] { "$$" }, StringSplitOptions.None);

                    // SQLSERVER
                    string servidorS = list[0].Substring(2);    // servidor sqlserver
                    string baseDatosS = list[1].Substring(2);   // base de datos sqlserver
                    string tipoAu = list[2].Substring(2);       // tipo de autenticacion sqlserver
                    string usuarioS = list[4].Substring(2);     // usuario  sqlserver
                    string contraS = list[3].Substring(2);      // contraseña  sqlserver

                    if (tipoAu.ToLower().Equals("windows"))
                    {
                        Modelos.ConectionString.connSS = string.Format(
                        "Data Source={0};Initial Catalog={1};Integrated Security=True;",
                        servidorS,
                        baseDatosS);
                    }

                    if (tipoAu.ToLower().Equals("sql server"))
                    {
                        Modelos.ConectionString.connSS = string.Format(
                        "Data Source={0};database={1};User Id={2};password={3};Trusted_Connection=yes;",
                        servidorS,
                        baseDatosS,
                        usuarioS,
                        contraS);
                    }

                    Modelos.Login.estacion = list[7].Substring(2);
                    serie1 = list[5].Substring(2);
                    serie2 = list[6].Substring(2);

                    
                    // MySQL
                    string servidorMs = list[8].Substring(2);   // servidor mysql
                    string usuarioMs = list[9].Substring(2);    // usuario mysql
                    string contraMs = list[10].Substring(2);     // contraseña mysql
                    string baseDatosMs = list[11].Substring(2);  // base de datos mysql

                    Modelos.ConectionString.connMySQL = string.Format(
                                "Data Source={0};database={1};User Id={2};password={3};",
                                servidorMs,
                                baseDatosMs,
                                usuarioMs,
                                contraMs);
                    
                }

                this._consultasSSNegocio = new ConsultasSSNegocio();

                // busca cual serie esta activa de las cargadas
                serie = this._consultasSSNegocio.buscaSerie(Modelos.Login.estacion);

                Modelos.Login.series = new Dictionary<int, string>();

                Modelos.Login.series.Add(!serie1.Equals(serie) ? 1 : 0, serie1);
                Modelos.Login.series.Add(!serie2.Equals(serie) ? 1 : 0, serie2);

                // carga informacion
                this._cargaInfo();

                this._defConfig = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            Process myProcess = new Process();

            myProcess.StartInfo.UseShellExecute = false;
            
            // You can start any process, HelloWorld is a do-nothing example.
            myProcess.StartInfo.FileName = @"C:\Program Files (x86)\Softrestaurant8.0.0Pro\";
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
            */

            this.Close();
        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAcceso formA = new frmAcceso();

            var respuesta = formA.ShowDialog();

            if (respuesta == System.Windows.Forms.DialogResult.OK)
            {
                frmConfiguracion form = new frmConfiguracion();
                var resultado = form.ShowDialog();

                if (resultado == System.Windows.Forms.DialogResult.OK)
                    this.FormPrincipal_Load(null, null);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _cargaInfo()
        {

            // consultar turnos
            List<Modelos.Turnos> turnos = this._consultasSSNegocio.getTurnosAbiertos();

            if (turnos.Count == 0)
            {
                this.lbTurno.Text = "CERRADO";
                this.lbTurno.ForeColor = Color.Red;

                this.lbVentas.Text = string.Empty;
            }
            else
            {
                this.lbTurno.Text = "ABIERTO";
                this.lbTurno.ForeColor = Color.Green;

                // buscar si ya tiene ventas realizadas
                this._ventasReal = this._consultasSSNegocio.buscaVentas(turnos[0].idTurno);

                if (this._ventasReal)
                {
                    this.lbVentas.Text = "CON VENTAS";
                    this.lbVentas.ForeColor = Color.Red;
                }
                else
                {
                    this.lbVentas.Text = "SIN VENTAS";
                    this.lbVentas.ForeColor = Color.Green;
                }
            }
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._defConfig)
                    throw new Exception("No se ha definido la configuración");

                this._cargaInfo();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCambiaTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._defConfig)
                    throw new Exception("No se ha definido la configuración");

                this._cargaInfo();

                if (this.lbTurno.Text.Equals("CERRADO"))
                    throw new Exception("No hay turno abierto");

                if (this._ventasReal)
                    throw new Exception("No se permite cambiar el turno, tiene ventas realizadas");

                // cambiar turnos
                string serie = Modelos.Login.series[1];
                string estacion = Modelos.Login.estacion;

                bool resultado = this._consultasSSNegocio.cambiaTurno(serie, estacion);

                if (resultado)
                {
                    MessageBox.Show("Turno cambiado con exito!!!\nLa aplicación se cerrará en caso de tenerla abierta", "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // cambia el valor activo de las series
                    Dictionary<int, string> espejo = new Dictionary<int, string>();

                    foreach (KeyValuePair<int, string> pair in Modelos.Login.series)
                    {
                        espejo.Add(pair.Key == 0 ? 1 : 0, pair.Value);
                    }

                    Modelos.Login.series.Clear();
                    Modelos.Login.series = espejo;

                    // se cierra la aplicacion
                    Process[] proceso = Process.GetProcessesByName("softrestaurant");

                    if (proceso.Count() != 0)
                        proceso[0].Kill();
                    /*
                    // se inicia la aplicacion
                    Process.Start(@"C:\Program Files (x86)\Softrestaurant8.0.0Pro\softrestaurant.exe");
                    */
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void elimFoliosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._defConfig)
                    throw new Exception("No se ha definido la configuración");

                frmFolios form = new frmFolios();

                form.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Mantenimiento de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
