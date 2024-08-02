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
    public partial class frmMusteriAra : Form
    {
        public frmMusteriAra()
        {
            InitializeComponent();
        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) Application.Exit();
        }

        private void btnYeniMusteri_Click(object sender, EventArgs e)
        {
            frmMusteriEkleme frm = new frmMusteriEkleme();            
            cGenel._musteriEkleme = 1;
            frm.btnGuncelle.Visible = false;
            frm.btnEkle.Visible = true;
            frm.Show();
        }

        private void frmMusteriAra_Load(object sender, EventArgs e)
        {
            cMusteriler c=new cMusteriler();
            c.MusterileriGetir(lvMusteriler);
        }

        private void btnMusteriGuncelle_Click(object sender, EventArgs e)
        {
            if (lvMusteriler.SelectedItems.Count > 0)//seçili müşteri olması
            {
                frmMusteriEkleme frm = new frmMusteriEkleme();
                cGenel._musteriEkleme = 1;
                cGenel._musteriId = Convert.ToInt32(lvMusteriler.SelectedItems[0].SubItems[0].Text);                
                frm.btnEkle.Visible = false;
                frm.btnGuncelle.Visible = true;                
                this.Close();
                frm.Show();
            }
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.MusterileriGetirAD(lvMusteriler, txtAd.Text);
        }
        private void txtSoyad_TextChanged(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.MusterileriGetirSOYAD(lvMusteriler, txtSoyad.Text);
        }
        private void txttelefon_TextChanged(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.MusterileriGetirTELEFON(lvMusteriler, txttelefon.Text);
        }
        private void txtAd_Click(object sender, EventArgs e)
        {
            txtSoyad.Clear();
            txttelefon.Clear();
        }
        private void txtSoyad_Click(object sender, EventArgs e)
        {
            txtAd.Clear();
            txttelefon.Clear();
        }
        private void txttelefon_Click(object sender, EventArgs e)
        {
            txtSoyad.Clear();
            txtAd.Clear();       
        }
    }
}
