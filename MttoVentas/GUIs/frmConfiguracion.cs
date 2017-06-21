using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MttoVentas.Negocio;
using System.IO;

namespace MttoVentas.GUIs
{
    public partial class frmConfiguracion : Form
    {
        private string _path = string.Empty;
        private IConsultasSSNegocio _consultasSSNegocio;
        private IConsultasMySQLNegocio _consultasMySQLNegocio;

        private bool _pruebaCon = false;

        public frmConfiguracion()
        {
            InitializeComponent();

            this.ActiveControl = this.tbServidorS;
            this._pruebaCon = false;

            this.tbUltFolio1.BackColor = this.tbUltFolio1.BackColor;
            this.tbUltFolio1.ForeColor = Color.Black;

            this.tbUltFolio2.BackColor = this.tbUltFolio2.BackColor;
            this.tbUltFolio2.ForeColor = Color.Black;
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName = "config.dat";
                string pathConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Mantenimiento\";

                // si no existe el directorio, lo crea
                bool exists = System.IO.Directory.Exists(pathConfigFile);

                if (!exists) System.IO.Directory.CreateDirectory(pathConfigFile);

                // busca en el directorio si exite el archivo con el nombre dado
                var file = Directory.GetFiles(pathConfigFile, fileName, SearchOption.AllDirectories)
                        .FirstOrDefault();

                this._path = pathConfigFile + fileName;

                if (file != null)
                {
                    // si existe
                    // cargar los datos en los campos
                    FEncrypt.Respuesta result = FEncrypt.EncryptDncrypt.DecryptFile(this._path, "milagros");

                    if (result.status == FEncrypt.Estatus.ERROR)
                        throw new Exception(result.error);

                    if (result.status == FEncrypt.Estatus.OK)
                    {
                        string[] list = result.resultado.Split(new string[] { "$$" }, StringSplitOptions.None);

                        if (list.Count() < 7)
                        {
                            foreach (Control x in this.Controls)
                            {
                                if (x is TextBox)
                                {
                                    ((TextBox)x).Text = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            // SQLSERVER
                            this.tbServidorS.Text = list[0].Substring(2);               // SERVIDOR
                            this.tbBaseDatosS.Text = list[1].Substring(2);              // BASE DE DATOS
                            this.cmbAutenticacionS.SelectedItem = list[2].Substring(2); // AUTENTICACION
                            this.tbUsuarioS.Text = list[3].Substring(2);                // USUARIO
                            this.tbContraseniaS.Text = list[4].Substring(2);            // CONTRASEÑA

                            // MYSQL
                            this.tbServidorMs.Text = list[8].Substring(2);      // SERVIDOR
                            this.tbUsuarioMs.Text = list[9].Substring(2);       // USUARIO
                            this.tbContraseniaMs.Text = list[10].Substring(2);   // CONTRASEÑA
                            this.tbBaseDeDatosMs.Text = list[11].Substring(2);   // BASE 

                            // carga series
                            this._pruebaCon = this.pruebaConexion();

                            if (this._pruebaCon)
                            {
                                List<Modelos.Folios> folios1 = this._consultasSSNegocio.obtFolios();
                                List<Modelos.Folios> folios2 = this._consultasSSNegocio.obtFolios();

                                List<Modelos.Estaciones> estaciones = this._consultasSSNegocio.getEstaciones();

                                this.cmbEstaciones.DataSource = estaciones;
                                this.cmbEstaciones.DisplayMember = "descripcion";
                                this.cmbEstaciones.ValueMember = "idestacion";

                                this.cmbSerie1.DataSource = folios1;
                                this.cmbSerie1.DisplayMember = "serie";
                                this.cmbSerie1.ValueMember = "serie";
                                // this.cmbSerie1.SelectedIndex = -1;

                                this.cmbSerie2.DataSource = folios2;
                                this.cmbSerie2.DisplayMember = "serie";
                                this.cmbSerie2.ValueMember = "serie";
                                // this.cmbSerie2.SelectedIndex = -1;

                                this.cmbSerie1.SelectedValue = list[5].Substring(2);
                                this.cmbSerie2.SelectedValue = list[6].Substring(2);
                                this.cmbEstaciones.SelectedValue = list[7].Substring(2);

                                this.cmbSerie1_SelectionChangeCommitted(null, null);
                                this.cmbSerie2_SelectionChangeCommitted(null, null);

                                // busca cual serie esta activa de las cargadas
                                string serie = this._consultasSSNegocio.buscaSerie(list[7].Substring(2));

                                if (serie.Equals(list[5].Substring(2)))
                                {
                                    this.label3.ForeColor = Color.Green;
                                    this.label4.ForeColor = Color.Green;
                                }

                                if (serie.Equals(list[6].Substring(2)))
                                {
                                    this.label5.ForeColor = Color.Green;
                                    this.label6.ForeColor = Color.Green;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnProbarConnMicrosip_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = this.pruebaConexion();

                if (!result)
                    throw new Exception("Error al conectar");

                // prueba con exito
                this._pruebaCon = result;

                MessageBox.Show("Conexión Exitosa!!!", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Falló la conexión a la base de datos", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool pruebaConexion()
        {
            string tipoAu = (string)this.cmbAutenticacionS.SelectedItem;

            Modelos.ConectionString.connSS = string.Empty;

            if (tipoAu.ToLower().Equals("windows"))
            {
                Modelos.ConectionString.connSS = string.Format(
                "Data Source={0};Initial Catalog={1};Integrated Security=True;",
                this.tbServidorS.Text,
                this.tbBaseDatosS.Text);
            }

            if (tipoAu.ToLower().Equals("sql server"))
            {
                Modelos.ConectionString.connSS = string.Format(
                "Data Source={0};database={1};User Id={2};password={3};",
                this.tbServidorS.Text,
                this.tbBaseDatosS.Text,
                this.tbUsuarioS.Text,
                this.tbContraseniaS.Text);
            }

            this._consultasSSNegocio = new ConsultasSSNegocio();

            bool result = this._consultasSSNegocio.pruebaConn();

            return result;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.tbServidorS.Text))
                    throw new Exception("Llene el campo Servidor");

                if (string.IsNullOrEmpty(this.tbBaseDatosS.Text))
                    throw new Exception("Llene el campo Base de Datos");

                if (this.cmbAutenticacionS.SelectedIndex == -1)
                    throw new Exception("Seleccione el tipo Autenticación");

                string tipo = (string)this.cmbAutenticacionS.SelectedItem;

                if (tipo.ToLower().Equals("sql server"))
                {
                    if (string.IsNullOrEmpty(this.tbUsuarioS.Text))
                        throw new Exception("Llene el campo Usuario");

                    if (string.IsNullOrEmpty(this.tbServidorS.Text))
                        throw new Exception("Llene el campo Contraseña");
                }

                // validaciones
                foreach (Control x in this.groupBox2.Controls)
                {
                    if (x is TextBox)
                    {
                        if (string.IsNullOrEmpty(((TextBox)x).Text))
                            throw new Exception("Campos incompletos, Por favor verifique");
                    }
                }

                // validaciones para folios
                if (this.cmbSerie1.SelectedIndex == -1 || this.cmbSerie2.SelectedIndex == -1)
                    throw new Exception("Seleccione una serie");

                string ultFolio1 = ((Modelos.Folios)this.cmbSerie1.SelectedItem).serie;
                string ultFolio2 = ((Modelos.Folios)this.cmbSerie2.SelectedItem).serie;

                string serie1 = ((Modelos.Folios)this.cmbSerie1.SelectedItem).serie;
                string serie2 = ((Modelos.Folios)this.cmbSerie2.SelectedItem).serie;
                string estacion = ((Modelos.Estaciones)this.cmbEstaciones.SelectedItem).idestacion;

                if (ultFolio1.Equals(ultFolio2))
                    throw new Exception("Seleccione una serie diferente para la segunda parte");

                string s1 = ((Modelos.Folios)this.cmbSerie1.SelectedItem).serie;
                string s2 = ((Modelos.Folios)this.cmbSerie2.SelectedItem).serie;

                // busca cual serie esta activa de las cargadas
                string serie = this._consultasSSNegocio.buscaSerie(estacion);

                if (!s1.Equals(serie) && !s2.Equals(serie))
                    throw new Exception("Una de las dos partes debe ser la serie actual");

                // define texto del archivo
                string cadena = string.Empty;

                // SQLSERVER
                cadena += "S_" + this.tbServidorS.Text + "$$";
                cadena += "B_" + this.tbBaseDatosS.Text + "$$";
                cadena += "A_" + this.cmbAutenticacionS.SelectedItem + "$$";
                cadena += "C_" + this.tbContraseniaS.Text + "$$";
                cadena += "U_" + this.tbUsuarioS.Text + "$$";
                
                // series
                cadena += "1_" + serie1 + "$$";
                cadena += "2_" + serie2 + "$$";
                cadena += "ES" + estacion + "$$";

                // MYSQL
                cadena += "S_" + this.tbServidorMs.Text + "$$";
                cadena += "U_" + this.tbUsuarioMs.Text + "$$";
                cadena += "C_" + this.tbContraseniaMs.Text + "$$";
                cadena += "B_" + this.tbBaseDeDatosMs.Text + "$$";

                // prosigue con la creación del archivo
                FEncrypt.Respuesta result = FEncrypt.EncryptDncrypt.EncryptFile("milagros", cadena, this._path);

                if (result.status == FEncrypt.Estatus.ERROR)
                    throw new Exception(result.error);

                if (result.status == FEncrypt.Estatus.OK)
                {
                    // SQLSERVER
                    string tipoAu = (string)this.cmbAutenticacionS.SelectedItem;

                    if (tipoAu.ToLower().Equals("windows"))
                    {
                        Modelos.ConectionString.connSS = string.Format(
                        "Data Source={0};Initial Catalog={1};Integrated Security=True;",
                        this.tbServidorS.Text,
                        this.tbBaseDatosS.Text);
                    }

                    if (tipoAu.ToLower().Equals("sql server"))
                    {
                        Modelos.ConectionString.connSS = string.Format(
                        "Data Source={0};database={1};User Id={2};password={3};Trusted_Connection=yes;",
                        this.tbServidorS.Text,
                        this.tbBaseDatosS.Text,
                        this.tbUsuarioS.Text,
                        this.tbContraseniaS.Text);
                    }

                    this._bloqueo(tipoAu);

                    // mysql
                    Modelos.ConectionString.connMySQL = string.Format(
                                "Data Source={0};database={1};User Id={2};password={3};",
                                this.tbServidorMs.Text,
                                this.tbBaseDeDatosMs.Text,
                                this.tbUsuarioMs.Text,
                                this.tbContraseniaMs.Text);

                    Modelos.Login.series = new Dictionary<int,string>();

                    Modelos.Login.series.Add(this.label3.ForeColor == Color.Black ? 1 : 0, serie1);
                    Modelos.Login.series.Add(this.label6.ForeColor == Color.Black ? 1 : 0, serie2);

                    Modelos.Login.estacion = estacion;

                    MessageBox.Show("Se cargó correctamente la información", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    this.Close();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmbAutenticacionS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string tipoAu = (string)this.cmbAutenticacionS.SelectedItem;

                this._bloqueo(tipoAu);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void _bloqueo(string tipoAu)
        {

            if (tipoAu.ToLower().Equals("windows"))
            {
                this.tbUsuarioS.Text = string.Empty;
                this.tbContraseniaS.Text = string.Empty;

                this.tbUsuarioS.Enabled = false;
                this.tbContraseniaS.Enabled = false;
            }

            if (tipoAu.ToLower().Equals("sql server"))
            {
                this.tbUsuarioS.Text = string.Empty;
                this.tbContraseniaS.Text = string.Empty;

                this.tbUsuarioS.Enabled = true;
                this.tbContraseniaS.Enabled = true;
            }
        }

        private void btnCambiaTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._pruebaCon)
                    throw new Exception("Realice una prueba de conexion exitosa con el servidor para poder cargar la información");

                List<Modelos.Estaciones> estaciones = this._consultasSSNegocio.getEstaciones();

                this.cmbEstaciones.DataSource = estaciones;
                this.cmbEstaciones.DisplayMember = "descripcion";
                this.cmbEstaciones.ValueMember = "idestacion";

                List<Modelos.Folios> folios1 = this._consultasSSNegocio.obtFolios();
                List<Modelos.Folios> folios2 = this._consultasSSNegocio.obtFolios();

                this.cmbSerie1.DataSource = folios1;
                this.cmbSerie1.DisplayMember = "serie";
                this.cmbSerie1.ValueMember = "serie";
                this.cmbSerie1.SelectedIndex = -1;
                this.tbUltFolio1.Text = string.Empty;

                this.cmbSerie2.DataSource = folios2;
                this.cmbSerie2.DisplayMember = "serie";
                this.cmbSerie2.ValueMember = "serie";
                this.cmbSerie2.SelectedIndex = -1;
                this.tbUltFolio2.Text = string.Empty;

                this.label3.ForeColor = Color.Black;
                this.label4.ForeColor = Color.Black;
                this.label5.ForeColor = Color.Black;
                this.label6.ForeColor = Color.Black;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmbSerie1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbSerie1.SelectedIndex == -1) return;

                string ultFolio = ((Modelos.Folios)this.cmbSerie1.SelectedItem).ultimoFolio;

                this.tbUltFolio1.Text = ultFolio;

                this._seleccionSerie();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmbSerie2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbSerie2.SelectedIndex == -1) return;

                string ultFolio = ((Modelos.Folios)this.cmbSerie2.SelectedItem).ultimoFolio;

                this.tbUltFolio2.Text = ultFolio;

                this._seleccionSerie();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmbEstaciones_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this._seleccionSerie();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void _seleccionSerie()
        {
            string estacion = this.cmbEstaciones.SelectedIndex == -1 ? "00" : ((Modelos.Estaciones)this.cmbEstaciones.SelectedItem).idestacion;
            string serie1 = this.cmbSerie1.SelectedIndex == -1 ? "00" : ((Modelos.Folios)this.cmbSerie1.SelectedItem).serie;
            string serie2 = this.cmbSerie2.SelectedIndex == -1 ? "00" : ((Modelos.Folios)this.cmbSerie2.SelectedItem).serie;

            // busca cual serie esta activa de las cargadas
            string serie = this._consultasSSNegocio.buscaSerie(estacion);

            if (serie.Equals(serie1))
            {
                this.label3.ForeColor = Color.Green;
                this.label4.ForeColor = Color.Green;
            }
            else
            {
                this.label3.ForeColor = Color.Black;
                this.label4.ForeColor = Color.Black;
            }

            if (serie.Equals(serie2))
            {
                this.label5.ForeColor = Color.Green;
                this.label6.ForeColor = Color.Green;
            }
            else
            {
                this.label5.ForeColor = Color.Black;
                this.label6.ForeColor = Color.Black;
            }
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
            try
            {
                Modelos.ConectionString.connMySQL = string.Format(
                            "server={0};User Id={1};password={2};database={3}",
                            this.tbServidorMs.Text,
                            this.tbUsuarioMs.Text,
                            this.tbContraseniaMs.Text,
                            this.tbBaseDeDatosMs.Text);

                this._consultasMySQLNegocio = new ConsultasMySQLNegocio();

                bool pruebaConn = this._consultasMySQLNegocio.pruebaConn();

                if (pruebaConn)
                    MessageBox.Show("Conexión Exitosa!!!", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    throw new Exception("Falló la conexión a la base de datos");
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Falló la conexión a la base de datos", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
