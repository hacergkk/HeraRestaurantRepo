using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace hera_restaurant
{
    public partial class frmAyarlar : Form
    {
        public frmAyarlar()
        {
            InitializeComponent();
        }
        private void frmAyarlar_Load(object sender, EventArgs e)
        {
            cPersoneller cp = new cPersoneller();
            cPersonelGorev cpgorev = new cPersonelGorev();
            string gorev = cpgorev.PersonelGorevTanim(cGenel._gorevıd);
            txtPersonelId.Text = Convert.ToString(cp.PersonelId);
            if (gorev == "Müdür")
            {
                cpgorev.PersonelGorevGetir(cbGorevi);
                cp.personelBilgileriniGetirLV(lvPersoneller);
                btnSil.Enabled = true;
                btnBilgiDegistir.Enabled = true;
                btnEkle.Enabled = true;
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox4.Visible = true;
                lblBilgi.Text = "Mevki: Müdür / Yetki Sınırsız / Kullanıcı: " + cp.PersonelBilgiGetirİsim(cGenel._personelId);
            }
            else
            {
                groupBox1.Visible = true;
                groupBox2.Visible = false;
                groupBox4.Visible = false;
                lvPersoneller.Visible = false;
                lblBilgi.Text = "Mevki: Müdür / Yetki Sınırlı / Kullanıcı: " + cp.PersonelBilgiGetirİsim(cGenel._personelId);
            }
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
        private void btnSifreDegistir_Click(object sender, EventArgs e)
        {
            cPersoneller p = new cPersoneller();
            txtPersonelId.Text = Convert.ToString(cGenel._personelId);
            if (txtMevcutSifre.Text.Trim() != "" && p.personelGirisKontrol(txtMevcutSifre.Text, Convert.ToInt32(txtPersonelId.Text)))
            {
                if (txtYeniSifre.Text.Trim() == txtYeniSifreTekrar.Text.Trim())
                {
                    if (txtPersonelId.Text.Trim() != "")
                    {
                        cPersoneller c = new cPersoneller();
                        bool sonuc = c.PersonelSifreDegistir(Convert.ToInt32(txtPersonelId.Text), txtYeniSifre.Text);
                        if (sonuc)
                        {
                            MessageBox.Show("Şifre başarıyla değiştirildi!");
                            txtMevcutSifre.Clear();
                            txtYeniSifre.Clear();
                            txtYeniSifreTekrar.Clear();
                            txtPersonelId.Clear();
                        }
                    }
                }
                else MessageBox.Show("Şifreler aynı değil !!");
            }
            else MessageBox.Show("Mevcut şifrenizi lütfen doğru giriniz!\nLütfen şifre alanını boş bırakmayınız !");
        }

        private void cbGorevi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cPersonelGorev c = (cPersonelGorev)cbGorevi.SelectedItem;
            txtGorevId2.Text = Convert.ToString(c.PersonelGorevId);
        }
        private void btnSil_Click(object sender, EventArgs e)
        {

            if (lvPersoneller.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Silmek istediğinize emin misiniz ?", "DİKKAT !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    cPersoneller c = new cPersoneller();
                    bool sonuc = c.PersonelSilme(Convert.ToInt32(lvPersoneller.SelectedItems[0].Text));
                    if (sonuc)
                    {
                        MessageBox.Show("Silme işlemi başarıyla tamamlandı !");
                        c.personelBilgileriniGetirLV(lvPersoneller);
                    }
                    else MessageBox.Show("Kayıt silinirken bir sorun oluştu!");
                }
                else MessageBox.Show("Lütfen silmek için bir kullanıcı seçiniz!");
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text.Trim() != "" && txtSoyad.Text.Trim() != "" && txtsifre.Text.Trim() != "" && txtSifreTekrar.Text.Trim() != "" && txtGorevId2.Text.Trim() != "")
            {
                if ((txtsifre.Text.Trim() == txtSifreTekrar.Text.Trim()) && (txtsifre.Text.Length > 5 || txtSifreTekrar.Text.Length > 5))
                {   //Şifre veya yeni şifre uzunluğu da 5 haneden büyük olsun
                    cPersoneller c = new cPersoneller();
                    c.PersonelAd = txtAd.Text.Trim();
                    c.PersonelSoyad = txtSoyad.Text.Trim();
                    c.PersonelParola = txtsifre.Text.Trim();
                    c.PersonelGorevId = Convert.ToInt32(txtGorevId2.Text);
                    bool sonuc = c.PersonelEkle(c);
                    if (sonuc)
                    {
                        MessageBox.Show("Personel başarıyla eklenmiştir!");
                        c.personelBilgileriniGetirLV(lvPersoneller);
                    }
                    else MessageBox.Show("Personel eklenirken bir hata oluştu!");
                }
                else MessageBox.Show("Şifreler aynı değil! Lütfen iki kutucuğa da aynı şifreyi yazın ve şfireniz 5 haneden uzun olsun !");
            }
            else MessageBox.Show("Lütfen tüm bilgileri eksiksiz doldurun !");
        }

        private void btnBilgiDegistir_Click(object sender, EventArgs e)
        {                                
            if (lvPersoneller.SelectedItems.Count > 0)
            {
                txtGorevId2.Text = Convert.ToString(lvPersoneller.SelectedItems[0].SubItems[1].Text);
                txtPersonelId2.Text = Convert.ToString(lvPersoneller.SelectedItems[0].SubItems[0].Text);
                if (txtAd.Text != "" && txtSoyad.Text != "" && txtsifre.Text != "" && txtSifreTekrar.Text != "" && txtGorevId2.Text != "")
                {
                    if ((txtsifre.Text.Trim() == txtSifreTekrar.Text.Trim()) && (txtsifre.Text.Length > 5 || txtSifreTekrar.Text.Length > 5))
                    {//Şifre veya yeni şifre uzunluğu da 5 haneden büyük olsun
                        cPersoneller c = new cPersoneller();
                        c.PersonelAd = txtAd.Text.Trim();
                        c.PersonelSoyad = txtSoyad.Text.Trim();
                        c.PersonelParola = txtsifre.Text.Trim();
                        c.PersonelGorevId = Convert.ToInt32(txtGorevId2.Text);
                        bool sonuc = c.PersonelGuncelle(c, Convert.ToInt32(txtPersonelId2.Text));
                        if (sonuc)
                        {
                            MessageBox.Show("Personel başarıyla güncellendi!");
                            c.personelBilgileriniGetirLV(lvPersoneller);
                        }
                        else MessageBox.Show("Personel güncellenirken bir hata oluştu!");
                    }
                    else MessageBox.Show("Şifreler aynı değil! Lütfen iki kutucuğa da aynı şifreyi yazın ve şfireniz 5 haneden uzun olsun !");
                }
                else MessageBox.Show("Lütfen tüm bilgileri eksiksiz doldurun !");
            }
            else MessageBox.Show("Lütfen güncellenecek personeli seçiniz!");
        }       
        private void lvPersoneller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPersoneller.SelectedItems.Count > 0)
            {
                btnSil.Enabled = true;
                txtPersonelId.Text = lvPersoneller.SelectedItems[0].SubItems[0].Text;
                cbGorevi.SelectedIndex = Convert.ToInt32(lvPersoneller.SelectedItems[0].SubItems[1].Text) - 1;
                txtAd.Text = lvPersoneller.SelectedItems[0].SubItems[3].Text;
                txtSoyad.Text = lvPersoneller.SelectedItems[0].SubItems[4].Text;
                txtsifre.Text = "";
                txtSifreTekrar.Text = "";
            }
        }

       
    }
}
