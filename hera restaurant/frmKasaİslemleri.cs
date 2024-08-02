using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hera_restaurant
{
    public partial class frmKasaİslemleri : Form
    {
        public frmKasaİslemleri()
        {
            InitializeComponent();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            frmMenu frm=new frmMenu();
            this.Close();
            frm.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) Application.Exit();
        }

        private void frmKasaİslemleri_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'dataSet1.DataTable2' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.dataTable2TableAdapter.Fill(this.dataSet1.DataTable2);
            // TODO: Bu kod satırı 'dataSet1.DataTable1' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.dataTable1TableAdapter.Fill(this.dataSet1.DataTable1);           
            this.rpvAylik.RefreshReport();
            this.rpvGunluk.RefreshReport();
            rpvGunluk.Visible = false;
        }

        private void btnAylıkRapor_Click(object sender, EventArgs e)
        {
            pcAylık.Visible = true;
            pcGunluk.Visible = false;
            rpvAylik.Visible = true;
            rpvGunluk.Visible = false;
        }

        private void btnZRaporu_Click(object sender, EventArgs e)
        {
            pcAylık.Visible = false;
            pcGunluk.Visible = true;
            rpvAylik.Visible = false;
            rpvGunluk.Visible = true;
        }
    }
}
