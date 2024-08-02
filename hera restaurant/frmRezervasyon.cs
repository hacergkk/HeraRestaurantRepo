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
    public partial class frmRezervasyon : Form
    {
        public frmRezervasyon()
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
        private void frmRezervasyon_Load(object sender, EventArgs e)
        {
            cMusteriler m =new cMusteriler();
            m.MusterileriGetir(lvMusteriler);
            cMasalar masalar = new cMasalar();
            masalar.masaKapasiteDurumGetir(cbmasa);
            dtTarih.MinDate = DateTime.Today;
            dtTarih.Format = DateTimePickerFormat.Time;
        }
        private void txtMusteriAd_TextChanged(object sender, EventArgs e)
        {
            cMusteriler m = new cMusteriler();
            m.MusterileriGetirAD(lvMusteriler, txtMusteriAd.Text);
        }
        private void txtTelefon_TextChanged(object sender, EventArgs e)
        {
            cMusteriler m = new cMusteriler();
            m.MusterileriGetirTELEFON(lvMusteriler, txtTelefon.Text);
        }
        private void txtAdres_TextChanged(object sender, EventArgs e)
        {
            cMusteriler m = new cMusteriler();
            m.MusterileriGetirADRES(lvMusteriler, txtAdres.Text);
        }
        void Temizle()//Textleri temizler
        {
            txtAdres.Clear();
            txtkisisayisi.Clear();
            txtmasa.Clear();
            txtMusteriAd.Clear();
            txtAdres.Clear();
        }
        private void btnRezervasyonAc_Click(object sender, EventArgs e)
        {
            cRezervasyon r = new cRezervasyon();
            if (lvMusteriler.SelectedItems.Count > 0)
            {
                bool sonuc = r.rezervasyonAcikMiKontrol(Convert.ToInt32(lvMusteriler.SelectedItems[0].SubItems[0].Text));
                if (!sonuc)
                {
                    if (txtTarih.Text != "")
                    {
                        cMasalar masa = new cMasalar();
                        if (masa.MasaDurumunuGetir(Convert.ToInt32(txtmasano.Text), 1))
                        {
                            cHesap hesap = new cHesap();
                            hesap.Tarih = Convert.ToDateTime(txtTarih.Text);
                            hesap.PersonelId = cGenel._personelId;
                           // hesap.ServisTurNo = 1;
                            hesap.MasaId = Convert.ToInt32(txtmasano.Text);
                            r.MusteriId = Convert.ToInt32(Convert.ToInt32(lvMusteriler.SelectedItems[0].SubItems[0].Text));
                            r.MasaId = Convert.ToInt32(txtmasano.Text);
                            r.Tarih = Convert.ToDateTime(txtTarih.Text);
                            r.MusteriSayısı = Convert.ToInt32(txtkisisayisi.Text);
                            r.Aciklama = txtAciklama.Text;
                            r.HesapId = hesap.rezervasyonHesapAc(hesap);//Hesabı açıyoruz
                            //frmMasalar ms=new frmMasalar();
                            
                            sonuc = r.rezervasyonAc(r);//Rezervasyonu açıyoruz                                                        
                            masa.MasayaYeniDurumAtama(txtmasano.Text, 2);
                            if (sonuc)
                            {
                                MessageBox.Show("Rezervasyon başarıyla açılmıştır!");
                                Temizle();
                            }
                            else MessageBox.Show("Rezervasyon açılırken bir hata oluştu!");
                        }
                        else MessageBox.Show("Rezervasyon yapılan masa şuan DOLU !");
                    }
                    else MessageBox.Show("Lütfen tarih seçiniz !");
                }
                else MessageBox.Show("Bu müşteri adına açık bir rezervasyon bulunmaktadır !");
            }
            else MessageBox.Show("Lütfen rezervasyon için müşteri seçin !");
        }

        private void dtTarih_MouseEnter(object sender, EventArgs e) //imleç üzerine geldiğinde
        {
            dtTarih.Width = 200;//Genişliğini ayarlar
        }
        private void dtTarih_Enter(object sender, EventArgs e) //tıklanıldığında 
        {
            dtTarih.Width = 200;
        }      

        private void dtTarih_ValueChanged(object sender, EventArgs e)
        {
            txtTarih.Text = dtTarih.Value.ToString();
        }
        private void cbkisisayisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtkisisayisi.Text = cbkisisayisi.SelectedItem.ToString();            
        }
        private void cbmasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbkisisayisi.Enabled = true;
            txtmasa.Text = cbmasa.SelectedItem.ToString();
            cMasalar kapasitesi = (cMasalar)cbmasa.SelectedItem;
            int kapasite = kapasitesi.KAPASITE;
            txtmasano.Text = Convert.ToString(kapasitesi.ID);//masanın numarası ıd'dir
            cbkisisayisi.Items.Clear();
            for (int i = 0; i < kapasite; i++) cbkisisayisi.Items.Add(i + 1);
            txtkisisayisi.Clear();
        }

        private void cbmasa_MouseEnter(object sender, EventArgs e)
        {
            cbmasa.Width = 200;
        }       
        private void cbkisisayisi_MouseEnter(object sender, EventArgs e)
        {
            cbkisisayisi.Width = 100;
        }
        private void frmRezervasyon_MouseEnter(object sender, EventArgs e)
        {
            dtTarih.Width = 20;
            cbmasa.Width = 20;
            cbkisisayisi.Width = 20;
        }
        private void btnYeniMusteri_Click(object sender, EventArgs e)
        {
            frmMusteriEkleme frm = new frmMusteriEkleme();
            cGenel._musteriEkleme = 0;
            frm.btnGuncelle.Visible = false;
            frm.btnEkle.Visible = true;
            this.Close();
            frm.Show();
        }
        private void btnMusteriGuncelle_Click(object sender, EventArgs e)
        {
            if (lvMusteriler.SelectedItems.Count > 0)
            {
                frmMusteriEkleme musteri = new frmMusteriEkleme();
                cGenel._musteriEkleme = 0;
                cGenel._musteriId = Convert.ToInt32(lvMusteriler.SelectedItems[0].SubItems[0].Text);
                musteri.btnGuncelle.Visible = true;
                musteri.btnEkle.Visible = false;
                this.Close();
                musteri.Show();
            }
        }
    }
}
