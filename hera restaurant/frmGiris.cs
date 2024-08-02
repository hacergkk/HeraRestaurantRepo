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
    public partial class frmGiris : Form
    {
        public frmGiris()
        {
            InitializeComponent();
        }      
        private void girisbutonu_Click(object sender, EventArgs e)
        {
            cPersoneller personeller = new cPersoneller();
            bool sonuc = personeller.personelGirisKontrol(txtsifre.Text, cGenel._personelId);
            if (sonuc)
            {
                cPersonelHareketleri ch=new cPersonelHareketleri();
                ch.PersonelId=cGenel._personelId;
                ch.Islem = "Giriş Yaptı";
                ch.Tarih=DateTime.Now; //veritabanına giriş bilgileri kaydedildi
                ch._personelHareketKaydetme(ch);
                this.Hide();//bu formu gizle
                frmMenu menu= new frmMenu();
                menu.Show();
            }
            else MessageBox.Show("Şifreniz Yanlış", "Uyarı !!!", MessageBoxButtons.OK, MessageBoxIcon.Stop); 
        }
        private void frmGiris_Load(object sender, EventArgs e)
        {
            cPersoneller p=new cPersoneller();
            p.personelBilgileriniGetir(cbkullanıcı);
            
        }
        private void cbkullanıcı_SelectedIndexChanged(object sender, EventArgs e)
        {
            cPersoneller p=(cPersoneller)cbkullanıcı.SelectedItem;
            cGenel._personelId = p.PersonelId;
            cGenel._gorevıd = p.PersonelGorevId;//her yerde ulaşabilmek için

        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?","Çıkış",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes) Application.Exit();
        }
    }
}
