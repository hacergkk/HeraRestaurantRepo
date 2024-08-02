namespace hera_restaurant
{
    partial class frmKasaİslemleri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKasaİslemleri));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dataTable1BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new hera_restaurant.DataSet1();
            this.dataTable2BindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.btnCikis = new System.Windows.Forms.Button();
            this.btnGeri = new System.Windows.Forms.Button();
            this.btnAylıkRapor = new System.Windows.Forms.Button();
            this.btnZRaporu = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.rpvAylik = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pcAylık = new System.Windows.Forms.PictureBox();
            this.rpvGunluk = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataTable1TableAdapter = new hera_restaurant.DataSet1TableAdapters.DataTable1TableAdapter();
            this.DataTable2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataTable2BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataTable2TableAdapter = new hera_restaurant.DataSet1TableAdapters.DataTable2TableAdapter();
            this.pcGunluk = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2BindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcAylık)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2BindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcGunluk)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTable1BindingSource1
            // 
            this.dataTable1BindingSource1.DataMember = "DataTable1";
            this.dataTable1BindingSource1.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataTable2BindingSource2
            // 
            this.dataTable2BindingSource2.DataMember = "DataTable2";
            this.dataTable2BindingSource2.DataSource = this.dataSet1;
            // 
            // btnCikis
            // 
            this.btnCikis.BackColor = System.Drawing.Color.Transparent;
            this.btnCikis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCikis.BackgroundImage")));
            this.btnCikis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCikis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCikis.FlatAppearance.BorderSize = 0;
            this.btnCikis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCikis.ForeColor = System.Drawing.Color.Transparent;
            this.btnCikis.Location = new System.Drawing.Point(1716, 930);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(99, 84);
            this.btnCikis.TabIndex = 19;
            this.btnCikis.UseVisualStyleBackColor = false;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // btnGeri
            // 
            this.btnGeri.BackColor = System.Drawing.Color.Transparent;
            this.btnGeri.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGeri.BackgroundImage")));
            this.btnGeri.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGeri.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGeri.FlatAppearance.BorderSize = 0;
            this.btnGeri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeri.ForeColor = System.Drawing.Color.Transparent;
            this.btnGeri.Location = new System.Drawing.Point(1610, 930);
            this.btnGeri.Name = "btnGeri";
            this.btnGeri.Size = new System.Drawing.Size(92, 84);
            this.btnGeri.TabIndex = 20;
            this.btnGeri.UseVisualStyleBackColor = false;
            this.btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
            // 
            // btnAylıkRapor
            // 
            this.btnAylıkRapor.BackColor = System.Drawing.Color.Transparent;
            this.btnAylıkRapor.BackgroundImage = global::hera_restaurant.Resource1.aylık_rapor_2;
            this.btnAylıkRapor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAylıkRapor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAylıkRapor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAylıkRapor.Location = new System.Drawing.Point(304, 225);
            this.btnAylıkRapor.Name = "btnAylıkRapor";
            this.btnAylıkRapor.Size = new System.Drawing.Size(189, 101);
            this.btnAylıkRapor.TabIndex = 21;
            this.btnAylıkRapor.UseVisualStyleBackColor = false;
            this.btnAylıkRapor.Click += new System.EventHandler(this.btnAylıkRapor_Click);
            // 
            // btnZRaporu
            // 
            this.btnZRaporu.BackColor = System.Drawing.Color.Transparent;
            this.btnZRaporu.BackgroundImage = global::hera_restaurant.Resource1.z_raporu;
            this.btnZRaporu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZRaporu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZRaporu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZRaporu.Location = new System.Drawing.Point(304, 366);
            this.btnZRaporu.Name = "btnZRaporu";
            this.btnZRaporu.Size = new System.Drawing.Size(189, 97);
            this.btnZRaporu.TabIndex = 21;
            this.btnZRaporu.UseVisualStyleBackColor = false;
            this.btnZRaporu.Click += new System.EventHandler(this.btnZRaporu_Click);
            // 
            // rpvAylik
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.dataTable1BindingSource1;
            this.rpvAylik.LocalReport.DataSources.Add(reportDataSource1);
            this.rpvAylik.LocalReport.ReportEmbeddedResource = "hera_restaurant.Report1.rdlc";
            this.rpvAylik.Location = new System.Drawing.Point(544, 163);
            this.rpvAylik.Name = "rpvAylik";
            this.rpvAylik.ServerReport.BearerToken = null;
            this.rpvAylik.Size = new System.Drawing.Size(720, 500);
            this.rpvAylik.TabIndex = 22;
            // 
            // pcAylık
            // 
            this.pcAylık.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pcAylık.BackgroundImage")));
            this.pcAylık.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pcAylık.Location = new System.Drawing.Point(544, 80);
            this.pcAylık.Name = "pcAylık";
            this.pcAylık.Size = new System.Drawing.Size(172, 55);
            this.pcAylık.TabIndex = 25;
            this.pcAylık.TabStop = false;
            // 
            // rpvGunluk
            // 
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.dataTable2BindingSource2;
            this.rpvGunluk.LocalReport.DataSources.Add(reportDataSource2);
            this.rpvGunluk.LocalReport.ReportEmbeddedResource = "hera_restaurant.Report2.rdlc";
            this.rpvGunluk.Location = new System.Drawing.Point(544, 209);
            this.rpvGunluk.Name = "rpvGunluk";
            this.rpvGunluk.ServerReport.BearerToken = null;
            this.rpvGunluk.Size = new System.Drawing.Size(720, 500);
            this.rpvGunluk.TabIndex = 22;
            // 
            // dataSet1BindingSource
            // 
            this.dataSet1BindingSource.DataSource = this.dataSet1;
            this.dataSet1BindingSource.Position = 0;
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.dataSet1;
            // 
            // dataTable1TableAdapter
            // 
            this.dataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // DataTable2BindingSource
            // 
            this.DataTable2BindingSource.DataMember = "DataTable2";
            this.DataTable2BindingSource.DataSource = this.dataSet1;
            // 
            // dataTable2BindingSource1
            // 
            this.dataTable2BindingSource1.DataMember = "DataTable2";
            this.dataTable2BindingSource1.DataSource = this.dataSet1;
            // 
            // dataTable2TableAdapter
            // 
            this.dataTable2TableAdapter.ClearBeforeFill = true;
            // 
            // pcGunluk
            // 
            this.pcGunluk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pcGunluk.BackgroundImage")));
            this.pcGunluk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pcGunluk.Location = new System.Drawing.Point(558, 80);
            this.pcGunluk.Name = "pcGunluk";
            this.pcGunluk.Size = new System.Drawing.Size(181, 55);
            this.pcGunluk.TabIndex = 25;
            this.pcGunluk.TabStop = false;
            this.pcGunluk.Visible = false;
            // 
            // frmKasaİslemleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.pcGunluk);
            this.Controls.Add(this.pcAylık);
            this.Controls.Add(this.rpvGunluk);
            this.Controls.Add(this.rpvAylik);
            this.Controls.Add(this.btnZRaporu);
            this.Controls.Add(this.btnAylıkRapor);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnGeri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmKasaİslemleri";
            this.Text = "frmKasaİslemleri";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmKasaİslemleri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2BindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcAylık)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2BindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcGunluk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCikis;
        private System.Windows.Forms.Button btnGeri;
        private System.Windows.Forms.Button btnAylıkRapor;
        private System.Windows.Forms.Button btnZRaporu;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Microsoft.Reporting.WinForms.ReportViewer rpvAylik;
        private System.Windows.Forms.BindingSource dataSet1BindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private System.Windows.Forms.BindingSource dataTable1BindingSource1;
        private DataSet1TableAdapters.DataTable1TableAdapter dataTable1TableAdapter;
        private System.Windows.Forms.PictureBox pcAylık;
        private Microsoft.Reporting.WinForms.ReportViewer rpvGunluk;
        private System.Windows.Forms.BindingSource DataTable2BindingSource;
        private System.Windows.Forms.BindingSource dataTable2BindingSource1;
        private DataSet1TableAdapters.DataTable2TableAdapter dataTable2TableAdapter;
        private System.Windows.Forms.BindingSource dataTable2BindingSource2;
        private System.Windows.Forms.PictureBox pcGunluk;
    }
}