namespace MttoVentas.GUIs
{
    partial class frmConfiguracion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbAutenticacionS = new System.Windows.Forms.ComboBox();
            this.tbBaseDatosS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbServidorS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnProbarConnMicrosip = new System.Windows.Forms.Button();
            this.tbContraseniaS = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbUsuarioS = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSerie1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbUltFolio1 = new System.Windows.Forms.TextBox();
            this.tbUltFolio2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSerie2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCargaInfo = new System.Windows.Forms.Button();
            this.cmbEstaciones = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbServidorMs = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbBaseDeDatosMs = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.tbContraseniaMs = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbUsuarioMs = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbAutenticacionS);
            this.groupBox1.Controls.Add(this.tbBaseDatosS);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbServidorS);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnProbarConnMicrosip);
            this.groupBox1.Controls.Add(this.tbContraseniaS);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbUsuarioS);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 303);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ventas";
            // 
            // cmbAutenticacionS
            // 
            this.cmbAutenticacionS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutenticacionS.FormattingEnabled = true;
            this.cmbAutenticacionS.Items.AddRange(new object[] {
            "Windows",
            "SQL Server"});
            this.cmbAutenticacionS.Location = new System.Drawing.Point(13, 133);
            this.cmbAutenticacionS.Name = "cmbAutenticacionS";
            this.cmbAutenticacionS.Size = new System.Drawing.Size(235, 24);
            this.cmbAutenticacionS.TabIndex = 3;
            this.cmbAutenticacionS.SelectionChangeCommitted += new System.EventHandler(this.cmbAutenticacionS_SelectionChangeCommitted);
            // 
            // tbBaseDatosS
            // 
            this.tbBaseDatosS.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbBaseDatosS.Location = new System.Drawing.Point(13, 84);
            this.tbBaseDatosS.Name = "tbBaseDatosS";
            this.tbBaseDatosS.Size = new System.Drawing.Size(235, 24);
            this.tbBaseDatosS.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.label9.Location = new System.Drawing.Point(10, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 14);
            this.label9.TabIndex = 26;
            this.label9.Text = "Base de Datos";
            // 
            // tbServidorS
            // 
            this.tbServidorS.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbServidorS.Location = new System.Drawing.Point(13, 36);
            this.tbServidorS.Name = "tbServidorS";
            this.tbServidorS.Size = new System.Drawing.Size(235, 24);
            this.tbServidorS.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 14);
            this.label1.TabIndex = 19;
            this.label1.Text = "Servidor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.label2.Location = new System.Drawing.Point(10, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 14);
            this.label2.TabIndex = 20;
            this.label2.Text = "Usuario";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.label7.Location = new System.Drawing.Point(10, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 14);
            this.label7.TabIndex = 22;
            this.label7.Text = "Contraseña";
            // 
            // btnProbarConnMicrosip
            // 
            this.btnProbarConnMicrosip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnProbarConnMicrosip.Location = new System.Drawing.Point(75, 269);
            this.btnProbarConnMicrosip.Name = "btnProbarConnMicrosip";
            this.btnProbarConnMicrosip.Size = new System.Drawing.Size(106, 23);
            this.btnProbarConnMicrosip.TabIndex = 6;
            this.btnProbarConnMicrosip.Text = "Probar Conexión";
            this.btnProbarConnMicrosip.UseVisualStyleBackColor = true;
            this.btnProbarConnMicrosip.Click += new System.EventHandler(this.btnProbarConnMicrosip_Click);
            // 
            // tbContraseniaS
            // 
            this.tbContraseniaS.Enabled = false;
            this.tbContraseniaS.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbContraseniaS.Location = new System.Drawing.Point(13, 232);
            this.tbContraseniaS.Name = "tbContraseniaS";
            this.tbContraseniaS.Size = new System.Drawing.Size(235, 24);
            this.tbContraseniaS.TabIndex = 5;
            this.tbContraseniaS.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.label8.Location = new System.Drawing.Point(10, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 14);
            this.label8.TabIndex = 24;
            this.label8.Text = "Autenticación";
            // 
            // tbUsuarioS
            // 
            this.tbUsuarioS.Enabled = false;
            this.tbUsuarioS.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbUsuarioS.Location = new System.Drawing.Point(13, 181);
            this.tbUsuarioS.Name = "tbUsuarioS";
            this.tbUsuarioS.Size = new System.Drawing.Size(235, 24);
            this.tbUsuarioS.TabIndex = 4;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Location = new System.Drawing.Point(454, 547);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(96, 30);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 437);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Serie:";
            // 
            // cmbSerie1
            // 
            this.cmbSerie1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerie1.FormattingEnabled = true;
            this.cmbSerie1.Location = new System.Drawing.Point(12, 453);
            this.cmbSerie1.Name = "cmbSerie1";
            this.cmbSerie1.Size = new System.Drawing.Size(69, 21);
            this.cmbSerie1.TabIndex = 18;
            this.cmbSerie1.SelectionChangeCommitted += new System.EventHandler(this.cmbSerie1_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 437);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Último Folio:";
            // 
            // tbUltFolio1
            // 
            this.tbUltFolio1.BackColor = System.Drawing.Color.White;
            this.tbUltFolio1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbUltFolio1.Location = new System.Drawing.Point(92, 451);
            this.tbUltFolio1.Name = "tbUltFolio1";
            this.tbUltFolio1.ReadOnly = true;
            this.tbUltFolio1.Size = new System.Drawing.Size(177, 24);
            this.tbUltFolio1.TabIndex = 20;
            this.tbUltFolio1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbUltFolio2
            // 
            this.tbUltFolio2.BackColor = System.Drawing.Color.White;
            this.tbUltFolio2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbUltFolio2.Location = new System.Drawing.Point(92, 500);
            this.tbUltFolio2.Name = "tbUltFolio2";
            this.tbUltFolio2.ReadOnly = true;
            this.tbUltFolio2.Size = new System.Drawing.Size(177, 24);
            this.tbUltFolio2.TabIndex = 24;
            this.tbUltFolio2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 484);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Último Folio:";
            // 
            // cmbSerie2
            // 
            this.cmbSerie2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerie2.FormattingEnabled = true;
            this.cmbSerie2.Location = new System.Drawing.Point(12, 502);
            this.cmbSerie2.Name = "cmbSerie2";
            this.cmbSerie2.Size = new System.Drawing.Size(69, 21);
            this.cmbSerie2.TabIndex = 22;
            this.cmbSerie2.SelectionChangeCommitted += new System.EventHandler(this.cmbSerie2_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 486);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Serie:";
            // 
            // btnCargaInfo
            // 
            this.btnCargaInfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnCargaInfo.Location = new System.Drawing.Point(12, 343);
            this.btnCargaInfo.Name = "btnCargaInfo";
            this.btnCargaInfo.Size = new System.Drawing.Size(257, 34);
            this.btnCargaInfo.TabIndex = 25;
            this.btnCargaInfo.Text = "Cargar Información";
            this.btnCargaInfo.UseVisualStyleBackColor = true;
            this.btnCargaInfo.Click += new System.EventHandler(this.btnCambiaTurno_Click);
            // 
            // cmbEstaciones
            // 
            this.cmbEstaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstaciones.FormattingEnabled = true;
            this.cmbEstaciones.Location = new System.Drawing.Point(72, 402);
            this.cmbEstaciones.Name = "cmbEstaciones";
            this.cmbEstaciones.Size = new System.Drawing.Size(197, 21);
            this.cmbEstaciones.TabIndex = 27;
            this.cmbEstaciones.SelectionChangeCommitted += new System.EventHandler(this.cmbEstaciones_SelectionChangeCommitted);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 405);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Estación:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbServidorMs);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.tbBaseDeDatosMs);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.btnTestConn);
            this.groupBox2.Controls.Add(this.tbContraseniaMs);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.tbUsuarioMs);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(275, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 303);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Base de Datos";
            // 
            // tbServidorMs
            // 
            this.tbServidorMs.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbServidorMs.Location = new System.Drawing.Point(19, 36);
            this.tbServidorMs.Name = "tbServidorMs";
            this.tbServidorMs.Size = new System.Drawing.Size(238, 24);
            this.tbServidorMs.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.label11.Location = new System.Drawing.Point(16, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 14);
            this.label11.TabIndex = 3;
            this.label11.Text = "Servidor";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.label12.Location = new System.Drawing.Point(16, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 14);
            this.label12.TabIndex = 4;
            this.label12.Text = "Usuario";
            // 
            // tbBaseDeDatosMs
            // 
            this.tbBaseDeDatosMs.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbBaseDeDatosMs.Location = new System.Drawing.Point(19, 182);
            this.tbBaseDeDatosMs.Name = "tbBaseDeDatosMs";
            this.tbBaseDeDatosMs.Size = new System.Drawing.Size(238, 24);
            this.tbBaseDeDatosMs.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.label13.Location = new System.Drawing.Point(16, 116);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 14);
            this.label13.TabIndex = 5;
            this.label13.Text = "Contraseña";
            // 
            // btnTestConn
            // 
            this.btnTestConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTestConn.Location = new System.Drawing.Point(80, 232);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(116, 23);
            this.btnTestConn.TabIndex = 5;
            this.btnTestConn.Text = "Probar Conexión";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // tbContraseniaMs
            // 
            this.tbContraseniaMs.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbContraseniaMs.Location = new System.Drawing.Point(19, 133);
            this.tbContraseniaMs.Name = "tbContraseniaMs";
            this.tbContraseniaMs.Size = new System.Drawing.Size(238, 24);
            this.tbContraseniaMs.TabIndex = 3;
            this.tbContraseniaMs.UseSystemPasswordChar = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.label14.Location = new System.Drawing.Point(16, 165);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 14);
            this.label14.TabIndex = 6;
            this.label14.Text = "Base de Datos";
            // 
            // tbUsuarioMs
            // 
            this.tbUsuarioMs.Font = new System.Drawing.Font("Tahoma", 10F);
            this.tbUsuarioMs.Location = new System.Drawing.Point(19, 84);
            this.tbUsuarioMs.Name = "tbUsuarioMs";
            this.tbUsuarioMs.Size = new System.Drawing.Size(238, 24);
            this.tbUsuarioMs.TabIndex = 2;
            // 
            // frmConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 589);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmbEstaciones);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnCargaInfo);
            this.Controls.Add(this.tbUltFolio2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbSerie2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbUltFolio1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSerie1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmConfiguracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.frmConfiguracion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbAutenticacionS;
        private System.Windows.Forms.TextBox tbBaseDatosS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbServidorS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnProbarConnMicrosip;
        private System.Windows.Forms.TextBox tbContraseniaS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbUsuarioS;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSerie1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbUltFolio1;
        private System.Windows.Forms.TextBox tbUltFolio2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSerie2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCargaInfo;
        private System.Windows.Forms.ComboBox cmbEstaciones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbServidorMs;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbBaseDeDatosMs;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.TextBox tbContraseniaMs;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbUsuarioMs;

    }
}