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
    public partial class frmMusteriEkleme : Form
    {
        public frmMusteriEkleme()
        {
            InitializeComponent();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            
            frmMenu frm = new frmMenu(); //bunu frm musteri yapmak istiyroum
            this.Close();
            frm.Show();            
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) Application.Exit();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            frmMusteriAra frm = new frmMusteriAra();
            if (txttelefon.Text.Length > 9) 
            {
                if (txtMusteriAd.Text == "" || txtMusteriSoyad.Text == "")
                {
                    MessageBox.Show("Lütfen Ad Soyad Bilgisini Giriniz!");
                }
                else
                {
                    cMusteriler c = new cMusteriler();
                    bool sonuc = c.MusteriVarMi(txttelefon.Text);
                    if (!sonuc) //Müşteri yoksa sonuç != false ise, true değilse
                    {
                        c.Musteriad = txtMusteriAd.Text;
                        c.Musterisoyad = txtMusteriSoyad.Text;
                        c.Telefon = txttelefon.Text;
                        c.Adres = txtAdres.Text;
                        c.Email = txtemail.Text;

                        txtMusteriNo.Text = c.MusteriEkle(c).ToString();
                        if (txtMusteriNo.Text != "")
                        {
                            MessageBox.Show("Müşteri Eklendi.");
                            this.Close();
                            frm.Show();
                        }
                        else MessageBox.Show("HATA ! Müşteri Eklenemedi!");
                    }
                    else MessageBox.Show("Bu isimde bir müşteri bulunmakta!");
                }
            }
            else MessageBox.Show("Lütfen en az 10 haneli telefon numarası giriniz ! Örnek: 5331234567");
        }

        private void btnMusteriSec_Click(object sender, EventArgs e)
        {
            if (cGenel._musteriEkleme == 0)
            {
                frmRezervasyon frm=new frmRezervasyon();
                cGenel._musteriEkleme = 1;
                this.Close();
                frm.Show();
            }
            else if (cGenel._musteriEkleme == 1) cGenel._musteriEkleme = 0;
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            frmMusteriAra frm=new frmMusteriAra();
            if (txttelefon.Text.Length > 6)
            {
                if (txtMusteriAd.Text == "" || txtMusteriSoyad.Text == "") MessageBox.Show("Lütfen Ad Soyad Bilgisini Giriniz!");
                else
                {
                    cMusteriler c = new cMusteriler();
                    c.Musteriad = txtMusteriAd.Text;
                    c.Musterisoyad = txtMusteriSoyad.Text;
                    c.Telefon = txttelefon.Text;
                    c.Adres = txtAdres.Text;
                    c.Email = txtemail.Text;
                    c.MusteriId = Convert.ToInt32(txtMusteriNo.Text);
                    bool sonuc = c.MusteriBilgileriGuncelle(c);
                    if (sonuc)//Müşteri yoksa
                    {

                        if (txtMusteriNo.Text != "")
                        {
                            MessageBox.Show("Müşteri Bilgileri Başarıyla Güncellendi!");
                            this.Close();
                            frm.Show();

                        }
                        else MessageBox.Show("HATA ! Müşteri Bilgileri Güncellenemedi!");
                    }
                    else MessageBox.Show("Bu isimde bir müşteri bulunmakta!");
                }
            }
            else MessageBox.Show("Lütfen en az 10 haneli telefon numarası giriniz ! Örnek: 5331234567");
        }
        private void frmMusteriEkleme_Load(object sender, EventArgs e)
        {
            if (cGenel._musteriId > 0)
            {               
                cMusteriler c = new cMusteriler();
                txtMusteriNo.Text = cGenel._musteriId.ToString();
                c.MusterileriGetirID(Convert.ToInt32(txtMusteriNo.Text), txtMusteriAd, txtMusteriSoyad, txttelefon, txtAdres, txtemail);
            }       
        }
        private void btnAnaMenu_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu(); 
            this.Close();
            frm.Show();
        }

        private void btnGeri_Click_1(object sender, EventArgs e)
        {
            frmMusteriAra frm = new frmMusteriAra();
            this.Close();
            frm.Show();
        }
    }
}
