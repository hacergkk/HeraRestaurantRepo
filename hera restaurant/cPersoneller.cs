using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace hera_restaurant
{
    internal class cPersoneller
    {
        cGenel gnl=new cGenel();
        #region alanlar

        private int _PersonelId;
        private int _PersonelGorevId;
        private string _PersonelAd;
        private string _PersonelSoyad;
        private string _PersonelParola;
        private string _PersonelKullaniciAdi;
        private bool _PersonelDurum;

        #endregion

        #region ozellikler
        public int PersonelId { get => _PersonelId; set => _PersonelId = value; }
        public int PersonelGorevId { get => _PersonelGorevId; set => _PersonelGorevId = value; }
        public string PersonelAd { get => _PersonelAd; set => _PersonelAd = value; }
        public string PersonelSoyad { get => _PersonelSoyad; set => _PersonelSoyad = value; }
        public string PersonelParola { get => _PersonelParola; set => _PersonelParola = value; }
        public string PersonelKullaniciAdi { get => _PersonelKullaniciAdi; set => _PersonelKullaniciAdi = value; }
        public bool PersonelDurum { get => _PersonelDurum; set => _PersonelDurum = value; }
        
        #endregion
        public bool personelGirisKontrol(string sifre, int KullanıcıId)
        {
            bool sonuc = false;
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select * from Personeller where Id=@Id and PAROLA=@PAROLA", baglantı);
            komut.Parameters.Add("@Id", SqlDbType.VarChar).Value = KullanıcıId;
            komut.Parameters.Add("@PAROLA", SqlDbType.VarChar).Value = sifre;
            //buradaki değişkenleri doğru yazıp yazmadığımdan emin değilim
            try
            {
                if (baglantı.State==ConnectionState.Closed)
                {
                    baglantı.Open();
                }
                sonuc = Convert.ToBoolean(komut.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cPersoneller";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglantı);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
                        
            return sonuc;
        }
        public void personelBilgileriniGetir(ComboBox cb)
        {
            cb.Items.Clear();
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select * from Personeller ", baglantı);
           
            //buradaki değişkenleri doğru yazıp yazmadığımdan emin değilim
            
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
            }
              SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read())
            {
                cPersoneller p=new cPersoneller();
                p._PersonelId = Convert.ToInt32(dr["ID"]);//burada ıd ismi veri tabanı ile aynı olmalı
                p._PersonelGorevId = Convert.ToInt32(dr["GOREVID"]);
                p._PersonelAd = Convert.ToString(dr["AD"]);
                p._PersonelSoyad = Convert.ToString(dr["SOYAD"]);
                p._PersonelParola = Convert.ToString(dr["PAROLA"]);
                p._PersonelKullaniciAdi = Convert.ToString(dr["KULLANICI_ADI"]);
                p._PersonelDurum = Convert.ToBoolean(dr["DURUM"]);
                cb.Items.Add(p);
            }
            dr.Close();
            baglantı.Close();
        }
        public override string ToString()
        {
            return _PersonelAd + " " + _PersonelSoyad;
        }
        public void personelBilgileriniGetirLV(ListView lv)//Personel Bilgisini Listview'e gönder
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);//Veritabanına bağlan
            SqlCommand komut = new SqlCommand("Select personeller.*, personel_gorevleri.GOREV FROM personeller INNER JOIN personel_gorevleri on personel_gorevleri.ID=personeller.GOREVID WHERE Personeller.DURUM=0 ", baglanti);
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlDataReader dr = komut.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                lv.Items.Add(dr["ID"].ToString());
                lv.Items[i].SubItems.Add(dr["GOREVID"].ToString());
                lv.Items[i].SubItems.Add(dr["GOREV"].ToString());
                lv.Items[i].SubItems.Add(dr["AD"].ToString());
                lv.Items[i].SubItems.Add(dr["SOYAD"].ToString());
                lv.Items[i].SubItems.Add(dr["KULLANICI_ADI"].ToString());
                i++;
            }
            dr.Close();
            baglanti.Close();
        }       
        public string PersonelBilgiGetirİsim(int personelId)//Personel ismini getir
        {
            string sonuc = "";
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select AD + SOYAD from Personeller WHERE Personeller.DURUM=0 AND Personeller.ID=@personelId ", baglanti);
            komut.Parameters.Add("@personelId", SqlDbType.Int).Value = personelId;
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                sonuc = komut.ExecuteScalar().ToString();
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cPersoneller";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglanti);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
            }
            finally
            {
                baglanti.Dispose();
                baglanti.Close();

            }
            return sonuc;
        }
        public bool PersonelSifreDegistir(int personelId, string password)
        {
            bool sonuc = false;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);//Veritabanına bağlan
            SqlCommand komut = new SqlCommand("UPDATE personeller SET PAROLA=@password WHERE ID=@personelId", baglanti);
            komut.Parameters.Add("@personelId", SqlDbType.Int).Value = personelId;
            komut.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                sonuc = Convert.ToBoolean(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cPersoneller";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglanti);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Dispose();
                baglanti.Close();
            }
            return sonuc;
        }
        public bool PersonelEkle(cPersoneller cp)//Personel ekle
        {
            bool sonuc = false;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);//Veritabanına bağlan
            SqlCommand komut = new SqlCommand("INSERT INTO personeller (AD,SOYAD,PAROLA,GOREVID) VALUES (@AD,@SOYAD,@PAROLA,@GOREVID) ", baglanti);
            komut.Parameters.Add("@AD", SqlDbType.VarChar).Value = _PersonelAd;
            komut.Parameters.Add("@SOYAD", SqlDbType.VarChar).Value = _PersonelSoyad;
            komut.Parameters.Add("@PAROLA", SqlDbType.VarChar).Value = _PersonelParola;
            komut.Parameters.Add("@GOREVID", SqlDbType.Int).Value = _PersonelGorevId;
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                sonuc = Convert.ToBoolean(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cPersoneller";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglanti);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Dispose();
                baglanti.Close();
            }
            return sonuc;
        }
        public bool PersonelGuncelle(cPersoneller cp, int personelId)//Personel Güncelle
        {
            bool sonuc = false;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("UPDATE personeller SET AD=@AD,SOYAD=@SOYAD,PAROLA=@PAROLA,GOREVID=@GOREVID WHERE ID=@personelId", baglanti);
            komut.Parameters.Add("@personelId", SqlDbType.Int).Value = personelId;
            komut.Parameters.Add("@AD", SqlDbType.VarChar).Value = _PersonelAd;
            komut.Parameters.Add("@SOYAD", SqlDbType.VarChar).Value = _PersonelSoyad;
            komut.Parameters.Add("@PAROLA", SqlDbType.VarChar).Value = _PersonelParola;
            komut.Parameters.Add("@GOREVID", SqlDbType.Int).Value = _PersonelGorevId;
        
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                sonuc = Convert.ToBoolean(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cPersoneller";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglanti);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Dispose();
                baglanti.Close();
            }
            return sonuc;
        }
        public bool PersonelSilme (int personelId)//Personel Silme
        {
            bool sonuc = false;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);//Veritabanına bağlan
            SqlCommand komut = new SqlCommand("UPDATE personeller SET DURUM=1 WHERE ID=@personelId", baglanti);
            komut.Parameters.Add("@personelId", SqlDbType.Int).Value = personelId;
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                sonuc = Convert.ToBoolean(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cPersoneller";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglanti);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Dispose();
                baglanti.Close();
            }
            return sonuc;
        }

    }
}
