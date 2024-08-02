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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }
        //burada tıklanılan butona göre o forma yönlendirme yapıyor
        private void btnRezervasyon_Click(object sender, EventArgs e)
        {
            frmRezervasyon frm = new frmRezervasyon();
            this.Close();
            frm.Show();
        }
        private void btnMasaSiparis_Click(object sender, EventArgs e)
        {
            frmMasalar frm=new frmMasalar();
            this.Close();
            frm.Show();
        }
        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            frmMusteriAra frm = new frmMusteriAra();
            this.Close();
            frm.Show();
        }
        private void btnKasa_Click(object sender, EventArgs e)
        {
            frmKasaİslemleri frm=new frmKasaİslemleri();
            this.Close();
            frm.Show();
        }        
        private void btnMutfak_Click(object sender, EventArgs e)
        {
            frmMutfak frm=new frmMutfak();
            this.Close();
            frm.Show();
        }
        private void btnRaporlar_Click(object sender, EventArgs e)
        {
            frmRaporlar frm=new frmRaporlar();
            this.Close();
            frm.Show();
        }
        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            frmAyarlar frm=new frmAyarlar();
            this.Close();
            frm.Show();
        }
        private void btnKilitle_Click(object sender, EventArgs e)
        {
            frmGiris frm=new frmGiris();
            frm.Show();
            this.Close();
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) Application.Exit();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
