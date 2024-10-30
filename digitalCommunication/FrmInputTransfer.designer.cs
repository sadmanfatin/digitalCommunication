namespace digitalCommunication
{
    partial class FrmInputTransfer
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpIputdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.btnInputTrnsFer = new System.Windows.Forms.Button();
            this.dgvORCL = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTrackingNo = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ORACLE = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnOutputSearch = new System.Windows.Forms.Button();
            this.cmbHour = new System.Windows.Forms.ComboBox();
            this.btnOutputTrns = new System.Windows.Forms.Button();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnFinSearch = new System.Windows.Forms.Button();
            this.btnFinTrns = new System.Windows.Forms.Button();
            this.btnPolySearch = new System.Windows.Forms.Button();
            this.btnTrnsPoly = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnSearchSewRej = new System.Windows.Forms.Button();
            this.btnTRNSewRej = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btnSearchFinRej = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvORCL)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.ORACLE.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // dtpIputdate
            // 
            this.dtpIputdate.CustomFormat = "dd-MMM-yyyy";
            this.dtpIputdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIputdate.Location = new System.Drawing.Point(135, 25);
            this.dtpIputdate.Name = "dtpIputdate";
            this.dtpIputdate.Size = new System.Drawing.Size(200, 22);
            this.dtpIputdate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(91, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Unit";
            // 
            // cmbUnit
            // 
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Items.AddRange(new object[] {
            "JAL-1",
            "JAL-2",
            "JAL-3",
            "FFL-1",
            "FFL-2",
            "FFL-3",
            "JFL-1",
            "JFL-2",
            "JKL-1",
            "JKL-2",
            "JKL-3",
            "JKL-4",
            "JKL-5",
            "DBL",
            "JKL-U2-1",
            "JKL-U2-2",
            "JKL-U2-3",
            "JKL-U2-4",
            "JKL-U2-5",
            "MFL",
            "MFL-1",
            "MFL-2",
            "MFL-3",
            "MFL-4",
            "FFL2-1",
            "FFL2-2",
            "FFL2-3",
            "FFL2-4",
            "FFL2-5",
            "FFL2-PADMA",
            "FFL2-MEGHNA",
            "FFL2-JAMUNA",
            "FFL2-KARNAPHULI",
            "FFL2-MADHUMATI"});
            this.cmbUnit.Location = new System.Drawing.Point(135, 60);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(200, 24);
            this.cmbUnit.TabIndex = 3;
            // 
            // btnInputTrnsFer
            // 
            this.btnInputTrnsFer.BackColor = System.Drawing.Color.LightGray;
            this.btnInputTrnsFer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInputTrnsFer.Location = new System.Drawing.Point(170, 17);
            this.btnInputTrnsFer.Name = "btnInputTrnsFer";
            this.btnInputTrnsFer.Size = new System.Drawing.Size(154, 38);
            this.btnInputTrnsFer.TabIndex = 4;
            this.btnInputTrnsFer.Text = "Transfer Input";
            this.btnInputTrnsFer.UseVisualStyleBackColor = false;
            this.btnInputTrnsFer.Click += new System.EventHandler(this.btnInputTrnsFer_Click);
            // 
            // dgvORCL
            // 
            this.dgvORCL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvORCL.Location = new System.Drawing.Point(5, 19);
            this.dgvORCL.Name = "dgvORCL";
            this.dgvORCL.RowTemplate.Height = 24;
            this.dgvORCL.Size = new System.Drawing.Size(1341, 492);
            this.dgvORCL.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.LightGray;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(18, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(129, 38);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search Input";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tracking No";
            // 
            // txtTrackingNo
            // 
            this.txtTrackingNo.Location = new System.Drawing.Point(136, 90);
            this.txtTrackingNo.Name = "txtTrackingNo";
            this.txtTrackingNo.Size = new System.Drawing.Size(199, 22);
            this.txtTrackingNo.TabIndex = 8;
            this.txtTrackingNo.Text = "0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ORACLE);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 154);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1358, 554);
            this.tabControl1.TabIndex = 9;
            // 
            // ORACLE
            // 
            this.ORACLE.Controls.Add(this.dgvORCL);
            this.ORACLE.Location = new System.Drawing.Point(4, 25);
            this.ORACLE.Name = "ORACLE";
            this.ORACLE.Padding = new System.Windows.Forms.Padding(3);
            this.ORACLE.Size = new System.Drawing.Size(1350, 525);
            this.ORACLE.TabIndex = 0;
            this.ORACLE.Text = "ORACLE";
            this.ORACLE.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1350, 525);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MIS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnOutputSearch
            // 
            this.btnOutputSearch.BackColor = System.Drawing.Color.LightGray;
            this.btnOutputSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutputSearch.Location = new System.Drawing.Point(6, 15);
            this.btnOutputSearch.Name = "btnOutputSearch";
            this.btnOutputSearch.Size = new System.Drawing.Size(155, 38);
            this.btnOutputSearch.TabIndex = 10;
            this.btnOutputSearch.Text = "Search Output";
            this.btnOutputSearch.UseVisualStyleBackColor = false;
            this.btnOutputSearch.Click += new System.EventHandler(this.btnOutputSearch_Click);
            // 
            // cmbHour
            // 
            this.cmbHour.FormattingEnabled = true;
            this.cmbHour.Items.AddRange(new object[] {
            "9",
            "10",
            "11",
            "12",
            "13",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "1",
            "2",
            "3",
            "4"});
            this.cmbHour.Location = new System.Drawing.Point(410, 60);
            this.cmbHour.Name = "cmbHour";
            this.cmbHour.Size = new System.Drawing.Size(121, 24);
            this.cmbHour.TabIndex = 11;
            // 
            // btnOutputTrns
            // 
            this.btnOutputTrns.BackColor = System.Drawing.Color.LightGray;
            this.btnOutputTrns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutputTrns.Location = new System.Drawing.Point(179, 18);
            this.btnOutputTrns.Name = "btnOutputTrns";
            this.btnOutputTrns.Size = new System.Drawing.Size(155, 33);
            this.btnOutputTrns.TabIndex = 12;
            this.btnOutputTrns.Text = "Transfer Output";
            this.btnOutputTrns.UseVisualStyleBackColor = false;
            this.btnOutputTrns.Click += new System.EventHandler(this.btnOutputTrns_Click);
            // 
            // txtLine
            // 
            this.txtLine.Location = new System.Drawing.Point(410, 90);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(121, 22);
            this.txtLine.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(348, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "Line";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(348, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "Hour";
            // 
            // BtnFinSearch
            // 
            this.BtnFinSearch.BackColor = System.Drawing.Color.LightGray;
            this.BtnFinSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFinSearch.Location = new System.Drawing.Point(6, 13);
            this.BtnFinSearch.Name = "BtnFinSearch";
            this.BtnFinSearch.Size = new System.Drawing.Size(155, 39);
            this.BtnFinSearch.TabIndex = 16;
            this.BtnFinSearch.Text = "Search Finishing";
            this.BtnFinSearch.UseVisualStyleBackColor = false;
            this.BtnFinSearch.Click += new System.EventHandler(this.BtnFinSearch_Click);
            // 
            // btnFinTrns
            // 
            this.btnFinTrns.BackColor = System.Drawing.Color.LightGray;
            this.btnFinTrns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinTrns.Location = new System.Drawing.Point(167, 13);
            this.btnFinTrns.Name = "btnFinTrns";
            this.btnFinTrns.Size = new System.Drawing.Size(155, 39);
            this.btnFinTrns.TabIndex = 17;
            this.btnFinTrns.Text = "Transfer Finishing";
            this.btnFinTrns.UseVisualStyleBackColor = false;
            this.btnFinTrns.Click += new System.EventHandler(this.btnFinTrns_Click);
            // 
            // btnPolySearch
            // 
            this.btnPolySearch.BackColor = System.Drawing.Color.LightGray;
            this.btnPolySearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPolySearch.Location = new System.Drawing.Point(6, 16);
            this.btnPolySearch.Name = "btnPolySearch";
            this.btnPolySearch.Size = new System.Drawing.Size(155, 38);
            this.btnPolySearch.TabIndex = 18;
            this.btnPolySearch.Text = "Search Poly";
            this.btnPolySearch.UseVisualStyleBackColor = false;
            this.btnPolySearch.Click += new System.EventHandler(this.btnPolySearch_Click);
            // 
            // btnTrnsPoly
            // 
            this.btnTrnsPoly.BackColor = System.Drawing.Color.LightGray;
            this.btnTrnsPoly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrnsPoly.Location = new System.Drawing.Point(167, 16);
            this.btnTrnsPoly.Name = "btnTrnsPoly";
            this.btnTrnsPoly.Size = new System.Drawing.Size(155, 38);
            this.btnTrnsPoly.TabIndex = 19;
            this.btnTrnsPoly.Text = "Transfer Poly";
            this.btnTrnsPoly.UseVisualStyleBackColor = false;
            this.btnTrnsPoly.Click += new System.EventHandler(this.btnTrnsPoly_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(626, 29);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.Padding = new System.Drawing.Point(8, 4);
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(645, 119);
            this.tabControl2.TabIndex = 30;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.btnInputTrnsFer);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(637, 82);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Input";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnOutputSearch);
            this.tabPage3.Controls.Add(this.btnOutputTrns);
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(637, 82);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Output";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.BtnFinSearch);
            this.tabPage4.Controls.Add(this.btnFinTrns);
            this.tabPage4.Location = new System.Drawing.Point(4, 33);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(637, 82);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Finishing";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnPolySearch);
            this.tabPage5.Controls.Add(this.btnTrnsPoly);
            this.tabPage5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage5.Location = new System.Drawing.Point(4, 33);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(637, 82);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.Text = "Poly";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnSearchSewRej);
            this.tabPage6.Controls.Add(this.btnTRNSewRej);
            this.tabPage6.Location = new System.Drawing.Point(4, 33);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(637, 82);
            this.tabPage6.TabIndex = 4;
            this.tabPage6.Text = "Sewing Rejection";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btnSearchSewRej
            // 
            this.btnSearchSewRej.BackColor = System.Drawing.Color.LightGray;
            this.btnSearchSewRej.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchSewRej.Location = new System.Drawing.Point(107, 22);
            this.btnSearchSewRej.Name = "btnSearchSewRej";
            this.btnSearchSewRej.Size = new System.Drawing.Size(155, 38);
            this.btnSearchSewRej.TabIndex = 20;
            this.btnSearchSewRej.Text = "Search Sew Rej";
            this.btnSearchSewRej.UseVisualStyleBackColor = false;
            this.btnSearchSewRej.Click += new System.EventHandler(this.btnSearchSewRej_Click);
            // 
            // btnTRNSewRej
            // 
            this.btnTRNSewRej.BackColor = System.Drawing.Color.LightGray;
            this.btnTRNSewRej.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTRNSewRej.Location = new System.Drawing.Point(268, 22);
            this.btnTRNSewRej.Name = "btnTRNSewRej";
            this.btnTRNSewRej.Size = new System.Drawing.Size(155, 38);
            this.btnTRNSewRej.TabIndex = 21;
            this.btnTRNSewRej.Text = "Transfer Sew Rej";
            this.btnTRNSewRej.UseVisualStyleBackColor = false;
            this.btnTRNSewRej.Click += new System.EventHandler(this.btnTRNSewRej_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btnSearchFinRej);
            this.tabPage7.Controls.Add(this.button4);
            this.tabPage7.Location = new System.Drawing.Point(4, 33);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(637, 82);
            this.tabPage7.TabIndex = 5;
            this.tabPage7.Text = "FIN Rejection";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btnSearchFinRej
            // 
            this.btnSearchFinRej.BackColor = System.Drawing.Color.LightGray;
            this.btnSearchFinRej.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchFinRej.Location = new System.Drawing.Point(133, 22);
            this.btnSearchFinRej.Name = "btnSearchFinRej";
            this.btnSearchFinRej.Size = new System.Drawing.Size(155, 38);
            this.btnSearchFinRej.TabIndex = 20;
            this.btnSearchFinRej.Text = "Search FIN Rej";
            this.btnSearchFinRej.UseVisualStyleBackColor = false;
            this.btnSearchFinRej.Click += new System.EventHandler(this.btnSearchFinRej_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LightGray;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(294, 22);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(155, 38);
            this.button4.TabIndex = 21;
            this.button4.Text = "Transfer Fin Rej";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1287, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmInputTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 728);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.cmbHour);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtTrackingNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpIputdate);
            this.Controls.Add(this.label1);
            this.Name = "FrmInputTransfer";
            this.Text = "FrmInputTransfer";
            this.Load += new System.EventHandler(this.FrmInputTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvORCL)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ORACLE.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpIputdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Button btnInputTrnsFer;
        private System.Windows.Forms.DataGridView dgvORCL;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTrackingNo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ORACLE;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnOutputSearch;
        private System.Windows.Forms.ComboBox cmbHour;
        private System.Windows.Forms.Button btnOutputTrns;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnFinSearch;
        private System.Windows.Forms.Button btnFinTrns;
        private System.Windows.Forms.Button btnPolySearch;
        private System.Windows.Forms.Button btnTrnsPoly;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button btnSearchSewRej;
        private System.Windows.Forms.Button btnTRNSewRej;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button btnSearchFinRej;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
    }
}