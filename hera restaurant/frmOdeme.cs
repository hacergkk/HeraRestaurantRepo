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
    public partial class frmOdeme : Form
    {
        public frmOdeme()
        {
            InitializeComponent();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            frmMasalar frm= new frmMasalar();
            this.Close();
            frm.Show();
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) Application.Exit();
        }
        int masaId;
        cSiparis cs= new cSiparis();
        int odemeturu=0;
        private void frmOdeme_Load(object sender, EventArgs e)
        {  
            cMasalar masa = new cMasalar();
            masaId = masa.MasaNumarasıBulma(cGenel._butonisim);
            string masanumarası = "masa" + masaId.ToString();
            object objresim = masa_numaraları.ResourceManager.GetObject(masanumarası);
            if (objresim != null && objresim is System.Drawing.Image) pboxMasaNumarası.BackgroundImage = (System.Drawing.Image)objresim;
            pnlIndirim.Visible = false;
            lblHesapId.Text = cGenel._HesapId;
            txtIndirimTutar.TextChanged += new EventHandler(txtIndirimTutar_TextChanged);
            cs.SiparisBilgileriniGetir(lvUrunler, Convert.ToInt32(lblHesapId.Text));
            if (lvUrunler.Items.Count > 0)
            {
                decimal toplam = 0;
                for (int i = 0; i < lvUrunler.Items.Count; i++)
                {
                    toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);//fiyatlarını topluyoruz
                }
                lblToplamTutar.Text = string.Format("{0:0.000}", toplam);
                lblOdenecek.Text = string.Format("{0:0.000}", toplam);
            }
            decimal kdv = Convert.ToDecimal(lblOdenecek.Text) * 18 / 100;
            lblKDV.Text = string.Format("{0:0.000}", kdv);
            if (chkIndirim.Checked)
            {
                pnlIndirim.Visible = true;
                pboxIndirimKoduyazısı.Visible = true;
            }
            else
            {
                pnlIndirim.Visible = false;
                pboxIndirimKoduyazısı.Visible = false;
            }
            txtIndirimTutar.Clear();
        }

        private void txtIndirimTutar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(lblIndirim.Text) < Convert.ToDecimal(lblToplamTutar.Text))
                {
                    try
                    {
                        lblIndirim.Text = string.Format("{0:0.000}", Convert.ToDecimal(txtIndirimTutar.Text));
                    }
                    catch (Exception)
                    {
                        lblIndirim.Text = string.Format("{0:0.000}", 0);
                    }
                }
                else
                {
                    MessageBox.Show("İndirim Tutarı Toplam Tutardan Fazla Olamaz !!");
                    frmOdeme fr=new frmOdeme();//burada bir bug olduğundan bu formu yeniden başlattırıyorum
                    fr.Show();
                    this.Close();
                    
                }
            }
            catch (Exception)
            {
                lblIndirim.Text = string.Format("{0:0.000}", 0);
            }
        }

        private void chkIndirim_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndirim.Checked)
            {
                pnlIndirim.Visible = true;
                pboxIndirimKoduyazısı.Visible = true;              
            }
            else
            {
                pnlIndirim.Visible = false;
                pboxIndirimKoduyazısı.Visible = false;
            }
            txtIndirimTutar.Clear();
        }

        private void lblIndirim_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblIndirim.Text)>=0)
            {
                decimal odenecek;
                lblOdenecek.Text=lblToplamTutar.Text;
                odenecek = Convert.ToDecimal(lblOdenecek.Text)-Convert.ToDecimal(lblIndirim.Text);
                lblOdenecek.Text = string.Format("{0:0.000}", odenecek);
            }
            decimal kdv = Convert.ToDecimal(lblOdenecek.Text) * 18 / 100;
            lblKDV.Text= string.Format("{0:0.000}", kdv);
        }
        cMasalar masalar= new cMasalar();
        cRezervasyon rezerve= new cRezervasyon();
        private void btnHesabıKapat_Click(object sender, EventArgs e)
        {
            int masaıd = masalar.MasaNumarasıBulma(cGenel._butonisim);
            int musteriıd;
            if (masalar.MasaDurumunuGetir(masaId, 2) == true)
            { //Eğer masa rezervasyon ise ona göre işlem yapılacak
                musteriıd = rezerve.MusteriIdGetirRezervasyondan(masaId);
            }
            else musteriıd = 1;
            int odemeturıd = 0;
            if (rbKrediKartı.Checked)
                odemeturıd = 2; //kredi kartı ödemesi ise
            if (rbNakit.Checked)
                odemeturıd = 1; //nakit ödeme ise
            cOdeme odeme = new cOdeme();
            odeme.HesapID = Convert.ToInt32(lblHesapId.Text);
            odeme.OdemeTurId = odemeturıd;
            odeme.MusteriId = musteriıd;
            odeme.AraToplam = Convert.ToDecimal(lblOdenecek.Text);
            odeme.Kdvtutari = Convert.ToDecimal(lblKDV.Text);
            odeme.GenelToplam = Convert.ToDecimal(lblToplamTutar.Text);
            odeme.Indirim = Convert.ToDecimal(lblIndirim.Text);
            //bilgileri aldık
            bool sonuc = odeme.HesapKapatma(odeme);
            if (sonuc)
            {
                MessageBox.Show("Hesap Kapatıldı");
                masalar.MasayaYeniDurumAtama(Convert.ToString(masaıd), 1);
                cRezervasyon c = new cRezervasyon();
                c.rezervasyonkapatma(Convert.ToInt32(lblHesapId.Text)); //rezervasyon kapatıldı

                cHesap hesap = new cHesap();
                hesap.HesapKapatma(Convert.ToInt32(lblHesapId.Text));
                this.Close();
                frmMasalar frm = new frmMasalar();
                frm.Show();

            }
            else MessageBox.Show("Hesap kapatılırken hata oluştu");
        }

        private void btnHesapOzeti_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        Font baslik = new Font("Verdana", 15, FontStyle.Bold);
        Font altbaslik = new Font("Verdana", 12, FontStyle.Regular);
        Font icerik = new Font("Verdana", 10);
        SolidBrush sb=new SolidBrush(Color.Black);
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) 
        { 
            StringFormat st= new StringFormat();
            st.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("Hera Restaurant", baslik, sb, 350, 100, st);
            e.Graphics.DrawString("-------------------", altbaslik, sb, 350, 100, st);
            e.Graphics.DrawString("Ürün Adı                    Adet                   Fiyat", altbaslik, sb, 150, 250, st);
            e.Graphics.DrawString("----------------------------------------------------------", altbaslik, sb, 150,200, st);
            for (int i = 0; i < lvUrunler.Items.Count; i++) //her bir ürün için
            {
                e.Graphics.DrawString(lvUrunler.Items[i].SubItems[0].Text, icerik, sb, 150, 300 + i * 30, st);
                e.Graphics.DrawString(lvUrunler.Items[i].SubItems[1].Text, icerik, sb, 350, 300 + i * 30, st);
                e.Graphics.DrawString(lvUrunler.Items[i].SubItems[3].Text, icerik, sb, 420, 300 + i * 30, st);
            }
            e.Graphics.DrawString("----------------------------------------------------------------------", altbaslik, sb, 150, 300 + 30 * lvUrunler.Items.Count, st);
            e.Graphics.DrawString("İndirim Tutarı   :------------- " + lblIndirim.Text + "₺", altbaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 1), st);
            e.Graphics.DrawString("KDV Tutarı       :------------- " + lblKDV.Text + "₺", altbaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 2), st);
            e.Graphics.DrawString("Toplam Tutar     :------------- " + lblToplamTutar.Text + "₺", altbaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 3), st);
            e.Graphics.DrawString("Ödediğiniz Tutar :------------- " + lblOdenecek.Text + "₺", altbaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 4), st);


        }
    }
}
