using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace hera_restaurant
{
    public partial class frmRaporlar : Form
    {
        public frmRaporlar()
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
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }       
        private void istatistik(string gfName, int kategoriId, Color renk)
        {
            chrraporlar.Controls.Clear();
            lvistatistik.Items.Clear();
            gbistatistik.Text = gfName;
            chrraporlar.Palette = ChartColorPalette.None;
            chrraporlar.Series[0].EmptyPointStyle.Color = Color.Transparent;
            chrraporlar.Series[0].Color = renk;
            cUrunler u = new cUrunler();
            u.urunleriListeleIstatistiklereGoreUrunId(lvistatistik, dtbaslangic, dtbitis, kategoriId);
            chrraporlar.Series["Satislar"].Points.Clear();//Chart'ı temizliyoruz
            if (lvistatistik.Items.Count > 0)
            {
                for (int i = 0; i < lvistatistik.Items.Count; i++)
                    chrraporlar.Series["Satislar"].Points.AddXY(lvistatistik.Items[i].SubItems[0].Text, lvistatistik.Items[i].SubItems[1].Text);
            }
            else MessageBox.Show("Gösterilecek bir istatistik yok, lütfen farklı bir ürün/tarih seçiniz!");
        }
        private void btnAnaYemekler1_Click(object sender, EventArgs e)
        {
            istatistik("Anayemekler Grafiği", 1, Color.Gray);
        }
        private void btnPizza2_Click(object sender, EventArgs e)
        {
            istatistik("Pizza Grafiği", 2, Color.Red);
        }
        private void btnBurger3_Click(object sender, EventArgs e)
        {
            istatistik("Burgerler Grafiği", 3, Color.Yellow);
        }
        private void btnMakarna4_Click(object sender, EventArgs e)
        {
            istatistik("Makarnalar Grafiği", 4, Color.Turquoise);
        }
        private void btnSalata5_Click(object sender, EventArgs e)
        {
            istatistik("Salatalar Grafiği", 5, Color.Green);
        }
        private void btnAtistirmalik6_Click(object sender, EventArgs e)
        {
            istatistik("Atıştırmalıklar Grafiği", 6, Color.Purple);
        }
        private void btnTatli7_Click(object sender, EventArgs e)
        {
            istatistik("Tatlılar Grafiği", 7, Color.Chocolate);
        }
        private void btnIcecekler8_Click(object sender, EventArgs e)
        {
            istatistik("İçeçeckler Grafiği", 8, Color.Blue);
        }
        private void btntumurunler_Click(object sender, EventArgs e)
        {
            chrraporlar.Controls.Clear();
            lvistatistik.Items.Clear();
            chrraporlar.Series["Satislar"].Points.Clear();//Chart'ı temizliyoruz
            gbistatistik.Text = "Tüm Ürünlerin Grafiği";
            chrraporlar.Palette = ChartColorPalette.None;
            chrraporlar.Series[0].EmptyPointStyle.Color = Color.Transparent;
            chrraporlar.Series[0].Color = Color.GreenYellow;
            cUrunler u = new cUrunler();
            u.urunleriListeleIstatistiklereGore(lvistatistik, dtbaslangic, dtbitis);
            if (lvistatistik.Items.Count > 0)
            {
                for (int i = 0; i < lvistatistik.Items.Count; i++)
                    chrraporlar.Series["Satislar"].Points.AddXY(lvistatistik.Items[i].SubItems[0].Text, lvistatistik.Items[i].SubItems[1].Text);
            }
            else MessageBox.Show("Gösterilecek bir istatistik yok, lütfen farklı bir ürün/tarih seçiniz!");
        }
        private void frmRaporlar_Load(object sender, EventArgs e)
        {
        }
    }
}
