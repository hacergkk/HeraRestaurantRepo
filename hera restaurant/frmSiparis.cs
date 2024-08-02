using System;
using System.Collections;
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
    public partial class frmSiparis : Form
    {
        public frmSiparis()
        {
            InitializeComponent();
        }

        int masaId;
        int hesapId;
        private void frmSiparis_Load(object sender, EventArgs e)
        {
           btn1.Click += new EventHandler(islem);
           btn2.Click += new EventHandler(islem);
           btn3.Click += new EventHandler(islem);
           btn4.Click += new EventHandler(islem);
           btn5.Click += new EventHandler(islem);
           btn6.Click += new EventHandler(islem);
           btn7.Click += new EventHandler(islem);
           btn8.Click += new EventHandler(islem);
           btn9.Click += new EventHandler(islem);
           btn0.Click += new EventHandler(islem);
           btnC.Click += new EventHandler(islem);
           cMasalar masa = new cMasalar();
           masaId = masa.MasaNumarasıBulma(cGenel._butonisim);
           string masanumarası = "masa" + masaId.ToString();
           object objresim = masa_numaraları.ResourceManager.GetObject(masanumarası);
           if (objresim != null && objresim is System.Drawing.Image) 
                pboxMasaNumaralari.BackgroundImage = (System.Drawing.Image)objresim;
           if (masa.MasaDurumunuGetir(masaId, 3) == true)
           {
               cHesap ad = new cHesap();
               hesapId = ad.HesapGetir(masaId);
               cSiparis siparisler = new cSiparis();
               siparisler.SiparisBilgileriniGetir(lvSiparis, hesapId);
           }
            if (masaId == 0) pboxMasaNumaralari.Visible = false;
        }
        #region //hesap işlemi- adet
        void islem(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btn1":
                    txtadet.Text += (1).ToString();
                    break;
                case "btn2":
                    txtadet.Text += (2).ToString();
                    break;
                case "btn3":
                    txtadet.Text += (3).ToString();
                    break;
                case "btn4":
                    txtadet.Text += (4).ToString();
                    break;
                case "btn5":
                    txtadet.Text += (5).ToString();
                    break;
                case "btn6":
                    txtadet.Text += (6).ToString();
                    break;
                case "btn7":
                    txtadet.Text += (7).ToString();
                    break;
                case "btn8":
                    txtadet.Text += (8).ToString();
                    break;
                case "btn9":
                    txtadet.Text += (9).ToString();
                    break;
                case "btn0":
                    txtadet.Text += (0).ToString();
                    break;
                case "btnC":
                    txtadet.Text = null;
                    break;

                default:
                    MessageBox.Show("sayı gir");
                    break;
            }
        }
        #endregion
        #region metotlar
        private void btn1_Click(object sender, EventArgs e)
        {

        } 
        private void btn2_Click(object sender, EventArgs e)
        {

        }
        private void btn3_Click(object sender, EventArgs e)
        {

        }
        private void btn4_Click(object sender, EventArgs e)
        {

        }
        private void btn5_Click(object sender, EventArgs e)
        {

        }
        private void btn6_Click(object sender, EventArgs e)
        {

        }
        private void btn7_Click(object sender, EventArgs e)
        {

        } 
        private void btn8_Click(object sender, EventArgs e)
        {

        }
        private void btn9_Click(object sender, EventArgs e)
        {

        }
        #endregion
        private void btnGeri_Click_1(object sender, EventArgs e)
        {
            frmMasalar frm = new frmMasalar();
            this.Close();
            frm.Show();
        }

        private void btnCikis_Click_1(object sender, EventArgs e)
        {

            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) Application.Exit();
        }
        cUrunCesitleri uc = new cUrunCesitleri();
        #region uruntipibulma
        private void btnAnaYemekler1_Click(object sender, EventArgs e)
        {           
            uc.UrunTipiBulma(lvUrunler, btnAnaYemekler1);
        }

        private void btnIcecekler8_Click(object sender, EventArgs e)
        {
            uc.UrunTipiBulma(lvUrunler, btnIcecekler8);
        }

        private void btnPizza2_Click(object sender, EventArgs e)
        {
            uc.UrunTipiBulma(lvUrunler, btnPizza2);
        }

        private void btnBurger3_Click(object sender, EventArgs e)
        {
            uc.UrunTipiBulma(lvUrunler, btnBurger3);
        }

        private void btnMakarna4_Click(object sender, EventArgs e)
        {
            uc.UrunTipiBulma(lvUrunler, btnMakarna4);
        }

        private void btnSalata5_Click(object sender, EventArgs e)
        {
            uc.UrunTipiBulma(lvUrunler, btnSalata5);
        }

        private void btnAtistirmalik6_Click(object sender, EventArgs e)
        {
            uc.UrunTipiBulma(lvUrunler, btnAtistirmalik6);
        }

        private void btnTatli7_Click(object sender, EventArgs e)
        {
            uc.UrunTipiBulma(lvUrunler, btnTatli7);
        }
        #endregion
        int sayac = 0;
        int sayac2 = 0;
        private void lvUrunler_DoubleClick(object sender, EventArgs e)
        {
            if (txtadet.Text=="") txtadet.Text = "1";//boşsa adet 1dir
            if (lvUrunler.Items.Count>0)
            {
                sayac = lvSiparis.Items.Count;
                lvSiparis.Items.Add(lvUrunler.SelectedItems[0].Text);
                lvSiparis.Items[sayac].SubItems.Add(txtadet.Text); //ürün adetini aldık
                lvSiparis.Items[sayac].SubItems.Add(lvUrunler.SelectedItems[0].SubItems[2].Text); //ürün fiyatını aldık
                lvSiparis.Items[sayac].SubItems.Add((Convert.ToDecimal(lvUrunler.SelectedItems[0].SubItems[1].Text)*
                    Convert.ToDecimal(txtadet.Text)).ToString()); //ürün fiyatını verir
                lvSiparis.Items[sayac].SubItems.Add("0");
                sayac2=lvYeniEklenenler.Items.Count;

                lvSiparis.Items[sayac].SubItems.Add(sayac2.ToString()); 
               
                lvYeniEklenenler.Items.Add(hesapId.ToString());
                lvYeniEklenenler.Items[sayac2].SubItems.Add(lvUrunler.SelectedItems[0].SubItems[2].Text);
                lvYeniEklenenler.Items[sayac2].SubItems.Add(txtadet.Text);
                lvYeniEklenenler.Items[sayac2].SubItems.Add(masaId.ToString());
                lvYeniEklenenler.Items[sayac2].SubItems.Add(sayac2.ToString());
                sayac2++;
                txtadet.Text = "";                          
            }
        }
        ArrayList silinenler=new ArrayList();
        private void btnSiparis_Click(object sender, EventArgs e)
        {
            //1-boş; 2-rezerve; 3-dolu
            if (lvSiparis.Items.Count == 0) MessageBox.Show("Lütfen sipariş için önce ürün ekleyiniz");
            else
            {
                cMasalar masa = new cMasalar();               
                frmMasalar masalaraDonmek = new frmMasalar();
                cHesap yeniHesap = new cHesap();
                cSiparis siparisKaydetme = new cSiparis();
                if (masa.MasaDurumunuGetir(masaId, 1) == true)//masa boş ise
                {
                    yeniHesap.PersonelId = 1;
                    yeniHesap.MasaId = masaId;
                    yeniHesap.Tarih = DateTime.Now;                  
                    yeniHesap.YeniHesapAcma(yeniHesap); //siparis vererek masayı açıyoruz
                    masa.MasayaYeniDurumAtama(cGenel._butonisim, 3);

                    if (lvSiparis.Items.Count > 0) //kayıt sayısı
                    {
                        for (int i = 0; i < lvSiparis.Items.Count; i++)
                        {
                            siparisKaydetme.MasaId = masaId;
                            siparisKaydetme.UrunID = Convert.ToInt32(lvSiparis.Items[i].SubItems[2].Text);
                            siparisKaydetme.HesapID = yeniHesap.HesapGetir(masaId);
                            siparisKaydetme.Adet = Convert.ToInt32(lvSiparis.Items[i].SubItems[1].Text);
                            siparisKaydetme.HesabıVeriTabanınaKaydetme(siparisKaydetme);
                        }
                        this.Close();
                        masalaraDonmek.Show();
                    }
                }
                else if (masa.MasaDurumunuGetir(masaId, 3) == true)
                {
                    if (lvSiparis.Items.Count == 0) MessageBox.Show("Lütfen sipariş için önce ürün ekleyiniz");
                    else 
                    {
                        if (lvYeniEklenenler.Items.Count > 0) //kayıt sayısı
                        {
                            for (int i = 0; i < lvYeniEklenenler.Items.Count; i++)
                            {
                                siparisKaydetme.MasaId = masaId;
                                siparisKaydetme.UrunID = Convert.ToInt32(lvYeniEklenenler.Items[i].SubItems[1].Text);
                                siparisKaydetme.HesapID = yeniHesap.HesapGetir(masaId);
                                siparisKaydetme.Adet = Convert.ToInt32(lvYeniEklenenler.Items[i].SubItems[2].Text);
                                siparisKaydetme.HesabıVeriTabanınaKaydetme(siparisKaydetme);

                            }
                        }
                        if (silinenler.Count > 0)//masa doluyken silme işlemi
                        {
                            foreach (string item in silinenler) siparisKaydetme.SiparistenUrunSilme(Convert.ToInt32(item));
                        }
                        this.Close();
                        masalaraDonmek.Show();
                    }                    
                }
                else if (masa.MasaDurumunuGetir(masaId, 2) == true)//rezerve
                {
                    masa.MasayaYeniDurumAtama(cGenel._butonisim, 3);
                    cRezervasyon c=new cRezervasyon();
                    c.rezervasyonkapatma(yeniHesap.HesapGetir(masaId)); //açılmış olan rezervasyon kapatılır
                    if (lvSiparis.Items.Count > 0)
                    {
                        for (int i = 0; i < lvSiparis.Items.Count; i++)
                        {
                            siparisKaydetme.MasaId = masaId;
                            siparisKaydetme.UrunID = Convert.ToInt32(lvSiparis.Items[i].SubItems[2].Text);
                            siparisKaydetme.HesapID = yeniHesap.HesapGetir(masaId);
                            siparisKaydetme.Adet = Convert.ToInt32(lvSiparis.Items[i].SubItems[1].Text);
                            siparisKaydetme.HesabıVeriTabanınaKaydetme(siparisKaydetme);
                        }
                        this.Close();
                        masalaraDonmek.Show();
                    }
                }                
            }
        }
        private void lvSiparis_DoubleClick(object sender, EventArgs e)
        {
            if(lvSiparis.Items.Count > 0)
            {
                if (lvSiparis.SelectedItems[0].SubItems[4].Text!="0")
                {
                    cSiparis yenihesap = new cSiparis();
                    yenihesap.SiparistenUrunSilme(Convert.ToInt32(lvSiparis.SelectedItems[0].SubItems[4].Text));
                }
                else
                {
                    for (int i = 0; i < lvYeniEklenenler.Items.Count; i++)
                    {
                        if (lvYeniEklenenler.Items[i].SubItems[4].Text== lvSiparis.SelectedItems[0].SubItems[5].Text)
                            lvYeniEklenenler.Items.RemoveAt(i);
                    }
                }
                lvSiparis.Items.RemoveAt(lvSiparis.SelectedItems[0].Index);
            }           
        }
        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            if (txtAra.Text=="") txtAra.Text = "";
            else
            {
                cUrunCesitleri c_urun = new cUrunCesitleri();
                c_urun.UrunArama(lvUrunler, Convert.ToInt32(txtAra.Text));
            }         
        }
        private void btnOdeme_Click(object sender, EventArgs e)
        {
            cGenel._HesapId = hesapId.ToString();
            frmOdeme frm = new frmOdeme();
            this.Close();
            frm.Show();
        }
    }
}
