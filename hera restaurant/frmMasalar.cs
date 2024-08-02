using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;


namespace hera_restaurant
{
    public partial class frmMasalar : Form
    {
        public frmMasalar()
        {
            InitializeComponent();
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) 
                Application.Exit();
        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }
        private void btnMasa1_Click(object sender, EventArgs e)
        {   //tüm masalar için bu metot çalışır
            Button tıklanılan_buton = sender as Button;
            frmSiparis frm = new frmSiparis();
            cGenel._butonisim = tıklanılan_buton.Name;
            this.Close();
            frm.Show();
        }              
        cGenel gnl = new cGenel();
        private void frmMasalar_Load(object sender, EventArgs e)
        {
            
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select DURUM,ID from Masalar", baglanti);
            SqlDataReader dr;           
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read()) 
            {
                foreach (Control item in this.Controls) 
                {
                    if (item is Button )
                    {   //burada masanın boş-rezerve-dolu durumuna göre masa butonunun arka plan resmini değiştiriyor
                        if (item.Name == "btnMasa" + dr["ID"].ToString() && dr["DURUM"].ToString() == "1")
                            item.BackgroundImage = (System.Drawing.Image)(Properties.Resources.masa_boş1);
                        else if (item.Name == "btnMasa" + dr["ID"].ToString() && dr["DURUM"].ToString() == "2")
                            item.BackgroundImage = (System.Drawing.Image)(Properties.Resources.masa_rezerve3);
                        else if (item.Name == "btnMasa" + dr["ID"].ToString() && dr["DURUM"].ToString() == "3")
                            item.BackgroundImage = (System.Drawing.Image)(Properties.Resources.masa_dolu1);
                    }
                }
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
