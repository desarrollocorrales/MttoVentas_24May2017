namespace MttoVentas.GUIs
{
    partial class frmFolios
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSerie = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pTurno = new System.Windows.Forms.Panel();
            this.dtpTDDel = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pPeriodo = new System.Windows.Forms.Panel();
            this.dtpPTAl = new System.Windows.Forms.DateTimePicker();
            this.dtpPDAl = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpPTDel = new System.Windows.Forms.DateTimePicker();
            this.dtpPDDel = new System.Windows.Forms.DateTimePicker();
            this.rbPeriodo = new System.Windows.Forms.RadioButton();
            this.rbTurno = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnCerrrar = new System.Windows.Forms.Button();
            this.tbNCM = new System.Windows.Forms.TextBox();
            this.tbNCT = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbDP = new System.Windows.Forms.TextBox();
            this.tbNAE = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbEC = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbEN = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbD = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbEA = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbIN = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbIA = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.tbCancelar = new System.Windows.Forms.Button();
            this.dgvResultado = new System.Windows.Forms.DataGridView();
            this.cbMDTodos = new System.Windows.Forms.CheckBox();
            this.cuentasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.pTurno.SuspendLayout();
            this.pPeriodo.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cuentasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSerie);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pTurno);
            this.groupBox1.Controls.Add(this.pPeriodo);
            this.groupBox1.Controls.Add(this.rbPeriodo);
            this.groupBox1.Controls.Add(this.rbTurno);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Búsqueda";
            // 
            // cmbSerie
            // 
            this.cmbSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerie.FormattingEnabled = true;
            this.cmbSerie.Items.AddRange(new object[] {
            "TODOS"});
            this.cmbSerie.Location = new System.Drawing.Point(61, 106);
            this.cmbSerie.Name = "cmbSerie";
            this.cmbSerie.Size = new System.Drawing.Size(63, 21);
            this.cmbSerie.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Serie";
            // 
            // pTurno
            // 
            this.pTurno.BackColor = System.Drawing.Color.Transparent;
            this.pTurno.Controls.Add(this.dtpTDDel);
            this.pTurno.Controls.Add(this.label2);
            this.pTurno.Controls.Add(this.label1);
            this.pTurno.Location = new System.Drawing.Point(6, 43);
            this.pTurno.Name = "pTurno";
            this.pTurno.Size = new System.Drawing.Size(222, 53);
            this.pTurno.TabIndex = 2;
            this.pTurno.Visible = false;
            // 
            // dtpTDDel
            // 
            this.dtpTDDel.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTDDel.Location = new System.Drawing.Point(35, 3);
            this.dtpTDDel.Name = "dtpTDDel";
            this.dtpTDDel.Size = new System.Drawing.Size(93, 21);
            this.dtpTDDel.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "06:00:00 - 05:59:59";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Del:";
            // 
            // pPeriodo
            // 
            this.pPeriodo.BackColor = System.Drawing.Color.Transparent;
            this.pPeriodo.Controls.Add(this.dtpPTAl);
            this.pPeriodo.Controls.Add(this.dtpPDAl);
            this.pPeriodo.Controls.Add(this.label4);
            this.pPeriodo.Controls.Add(this.label3);
            this.pPeriodo.Controls.Add(this.dtpPTDel);
            this.pPeriodo.Controls.Add(this.dtpPDDel);
            this.pPeriodo.Location = new System.Drawing.Point(6, 43);
            this.pPeriodo.Name = "pPeriodo";
            this.pPeriodo.Size = new System.Drawing.Size(222, 53);
            this.pPeriodo.TabIndex = 0;
            this.pPeriodo.Visible = false;
            // 
            // dtpPTAl
            // 
            this.dtpPTAl.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpPTAl.Location = new System.Drawing.Point(134, 27);
            this.dtpPTAl.Name = "dtpPTAl";
            this.dtpPTAl.ShowUpDown = true;
            this.dtpPTAl.Size = new System.Drawing.Size(83, 21);
            this.dtpPTAl.TabIndex = 5;
            // 
            // dtpPDAl
            // 
            this.dtpPDAl.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPDAl.Location = new System.Drawing.Point(35, 27);
            this.dtpPDAl.Name = "dtpPDAl";
            this.dtpPDAl.Size = new System.Drawing.Size(93, 21);
            this.dtpPDAl.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Al:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Del:";
            // 
            // dtpPTDel
            // 
            this.dtpPTDel.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpPTDel.Location = new System.Drawing.Point(134, 3);
            this.dtpPTDel.Name = "dtpPTDel";
            this.dtpPTDel.ShowUpDown = true;
            this.dtpPTDel.Size = new System.Drawing.Size(83, 21);
            this.dtpPTDel.TabIndex = 1;
            // 
            // dtpPDDel
            // 
            this.dtpPDDel.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPDDel.Location = new System.Drawing.Point(35, 3);
            this.dtpPDDel.Name = "dtpPDDel";
            this.dtpPDDel.Size = new System.Drawing.Size(93, 21);
            this.dtpPDDel.TabIndex = 0;
            // 
            // rbPeriodo
            // 
            this.rbPeriodo.AutoSize = true;
            this.rbPeriodo.Location = new System.Drawing.Point(125, 20);
            this.rbPeriodo.Name = "rbPeriodo";
            this.rbPeriodo.Size = new System.Drawing.Size(61, 17);
            this.rbPeriodo.TabIndex = 1;
            this.rbPeriodo.Text = "Periodo";
            this.rbPeriodo.UseVisualStyleBackColor = true;
            this.rbPeriodo.CheckedChanged += new System.EventHandler(this.rbPeriodo_CheckedChanged);
            // 
            // rbTurno
            // 
            this.rbTurno.AutoSize = true;
            this.rbTurno.Location = new System.Drawing.Point(57, 20);
            this.rbTurno.Name = "rbTurno";
            this.rbTurno.Size = new System.Drawing.Size(53, 17);
            this.rbTurno.TabIndex = 0;
            this.rbTurno.Text = "Turno";
            this.rbTurno.UseVisualStyleBackColor = true;
            this.rbTurno.CheckedChanged += new System.EventHandler(this.rbTurno_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(144, 104);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnCerrrar
            // 
            this.btnCerrrar.Location = new System.Drawing.Point(913, 12);
            this.btnCerrrar.Name = "btnCerrrar";
            this.btnCerrrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrrar.TabIndex = 7;
            this.btnCerrrar.Text = "Cerrar";
            this.btnCerrrar.UseVisualStyleBackColor = true;
            this.btnCerrrar.Click += new System.EventHandler(this.btnCerrrar_Click);
            // 
            // tbNCM
            // 
            this.tbNCM.BackColor = System.Drawing.Color.White;
            this.tbNCM.ForeColor = System.Drawing.Color.Black;
            this.tbNCM.Location = new System.Drawing.Point(179, 41);
            this.tbNCM.Name = "tbNCM";
            this.tbNCM.ReadOnly = true;
            this.tbNCM.Size = new System.Drawing.Size(49, 21);
            this.tbNCM.TabIndex = 11;
            this.tbNCM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbNCT
            // 
            this.tbNCT.BackColor = System.Drawing.Color.White;
            this.tbNCT.ForeColor = System.Drawing.Color.Black;
            this.tbNCT.Location = new System.Drawing.Point(179, 16);
            this.tbNCT.Name = "tbNCT";
            this.tbNCT.ReadOnly = true;
            this.tbNCT.Size = new System.Drawing.Size(49, 21);
            this.tbNCT.TabIndex = 10;
            this.tbNCT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Número de cuentas a modificar:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Número de cuentas total:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tbDP);
            this.panel1.Controls.Add(this.tbNAE);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.tbEC);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.tbEN);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.tbD);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.tbEA);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.tbIN);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.tbIA);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tbNCM);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tbNCT);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Location = new System.Drawing.Point(12, 527);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 104);
            this.panel1.TabIndex = 12;
            // 
            // tbDP
            // 
            this.tbDP.BackColor = System.Drawing.Color.White;
            this.tbDP.ForeColor = System.Drawing.Color.Black;
            this.tbDP.Location = new System.Drawing.Point(470, 70);
            this.tbDP.Name = "tbDP";
            this.tbDP.ReadOnly = true;
            this.tbDP.Size = new System.Drawing.Size(39, 21);
            this.tbDP.TabIndex = 25;
            this.tbDP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbNAE
            // 
            this.tbNAE.BackColor = System.Drawing.Color.White;
            this.tbNAE.ForeColor = System.Drawing.Color.Black;
            this.tbNAE.Location = new System.Drawing.Point(470, 16);
            this.tbNAE.Name = "tbNAE";
            this.tbNAE.ReadOnly = true;
            this.tbNAE.Size = new System.Drawing.Size(39, 21);
            this.tbNAE.TabIndex = 24;
            this.tbNAE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(539, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Efectivo nuevo:";
            // 
            // tbEC
            // 
            this.tbEC.BackColor = System.Drawing.Color.White;
            this.tbEC.ForeColor = System.Drawing.Color.Black;
            this.tbEC.Location = new System.Drawing.Point(636, 70);
            this.tbEC.Name = "tbEC";
            this.tbEC.ReadOnly = true;
            this.tbEC.Size = new System.Drawing.Size(113, 21);
            this.tbEC.TabIndex = 23;
            this.tbEC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(539, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Efectivo en caja:";
            // 
            // tbEN
            // 
            this.tbEN.BackColor = System.Drawing.Color.White;
            this.tbEN.ForeColor = System.Drawing.Color.Black;
            this.tbEN.Location = new System.Drawing.Point(636, 43);
            this.tbEN.Name = "tbEN";
            this.tbEN.ReadOnly = true;
            this.tbEN.Size = new System.Drawing.Size(113, 21);
            this.tbEN.TabIndex = 22;
            this.tbEN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(539, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Efectivo anterior:";
            // 
            // tbD
            // 
            this.tbD.BackColor = System.Drawing.Color.White;
            this.tbD.ForeColor = System.Drawing.Color.Black;
            this.tbD.Location = new System.Drawing.Point(351, 70);
            this.tbD.Name = "tbD";
            this.tbD.ReadOnly = true;
            this.tbD.Size = new System.Drawing.Size(113, 21);
            this.tbD.TabIndex = 19;
            this.tbD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(255, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Diferencia:";
            // 
            // tbEA
            // 
            this.tbEA.BackColor = System.Drawing.Color.White;
            this.tbEA.ForeColor = System.Drawing.Color.Black;
            this.tbEA.Location = new System.Drawing.Point(636, 16);
            this.tbEA.Name = "tbEA";
            this.tbEA.ReadOnly = true;
            this.tbEA.Size = new System.Drawing.Size(113, 21);
            this.tbEA.TabIndex = 18;
            this.tbEA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(255, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Importe anterior:";
            // 
            // tbIN
            // 
            this.tbIN.BackColor = System.Drawing.Color.White;
            this.tbIN.ForeColor = System.Drawing.Color.Black;
            this.tbIN.Location = new System.Drawing.Point(351, 43);
            this.tbIN.Name = "tbIN";
            this.tbIN.ReadOnly = true;
            this.tbIN.Size = new System.Drawing.Size(113, 21);
            this.tbIN.TabIndex = 15;
            this.tbIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(255, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Importe nuevo:";
            // 
            // tbIA
            // 
            this.tbIA.BackColor = System.Drawing.Color.White;
            this.tbIA.ForeColor = System.Drawing.Color.Black;
            this.tbIA.Location = new System.Drawing.Point(351, 16);
            this.tbIA.Name = "tbIA";
            this.tbIA.ReadOnly = true;
            this.tbIA.Size = new System.Drawing.Size(113, 21);
            this.tbIA.TabIndex = 14;
            this.tbIA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(508, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "%";
            // 
            // btnAplicar
            // 
            this.btnAplicar.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnAplicar.Location = new System.Drawing.Point(782, 527);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(206, 48);
            this.btnAplicar.TabIndex = 13;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // tbCancelar
            // 
            this.tbCancelar.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.tbCancelar.Location = new System.Drawing.Point(782, 583);
            this.tbCancelar.Name = "tbCancelar";
            this.tbCancelar.Size = new System.Drawing.Size(206, 48);
            this.tbCancelar.TabIndex = 14;
            this.tbCancelar.Text = "Cancelar";
            this.tbCancelar.UseVisualStyleBackColor = true;
            this.tbCancelar.Click += new System.EventHandler(this.tbCancelar_Click);
            // 
            // dgvResultado
            // 
            this.dgvResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultado.Location = new System.Drawing.Point(12, 152);
            this.dgvResultado.Name = "dgvResultado";
            this.dgvResultado.Size = new System.Drawing.Size(976, 369);
            this.dgvResultado.TabIndex = 15;
            this.dgvResultado.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dgvResultado.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dgvResultado.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvResultado_CurrentCellDirtyStateChanged);
            // 
            // cbMDTodos
            // 
            this.cbMDTodos.AutoSize = true;
            this.cbMDTodos.Location = new System.Drawing.Point(897, 129);
            this.cbMDTodos.Name = "cbMDTodos";
            this.cbMDTodos.Size = new System.Drawing.Size(91, 17);
            this.cbMDTodos.TabIndex = 16;
            this.cbMDTodos.Text = "Marcar Todos";
            this.cbMDTodos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbMDTodos.UseVisualStyleBackColor = true;
            this.cbMDTodos.CheckedChanged += new System.EventHandler(this.cbMDTodos_CheckedChanged);
            // 
            // cuentasBindingSource
            // 
            this.cuentasBindingSource.DataSource = typeof(MttoVentas.Modelos.Cuentas);
            // 
            // frmFolios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 643);
            this.Controls.Add(this.cbMDTodos);
            this.Controls.Add(this.dgvResultado);
            this.Controls.Add(this.tbCancelar);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCerrrar);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmFolios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eliminación de Folios";
            this.Load += new System.EventHandler(this.frmFolios_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pTurno.ResumeLayout(false);
            this.pTurno.PerformLayout();
            this.pPeriodo.ResumeLayout(false);
            this.pPeriodo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cuentasBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSerie;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pPeriodo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpPTDel;
        private System.Windows.Forms.DateTimePicker dtpPDDel;
        private System.Windows.Forms.Panel pTurno;
        private System.Windows.Forms.DateTimePicker dtpTDDel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbPeriodo;
        private System.Windows.Forms.RadioButton rbTurno;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpPTAl;
        private System.Windows.Forms.DateTimePicker dtpPDAl;
        private System.Windows.Forms.Button btnCerrrar;
        private System.Windows.Forms.BindingSource cuentasBindingSource;
        private System.Windows.Forms.TextBox tbNCM;
        private System.Windows.Forms.TextBox tbNCT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbDP;
        private System.Windows.Forms.TextBox tbNAE;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbEC;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbEN;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbD;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbEA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbIN;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbIA;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button tbCancelar;
        private System.Windows.Forms.DataGridView dgvResultado;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbMDTodos;
    }
}