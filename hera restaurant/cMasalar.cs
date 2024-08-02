using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hera_restaurant
{
    internal class cMasalar
    {
        #region değişkenler
        private int _ID;
        private int _KAPASITE;
        private int _DURUM;
        private int _ONAY;
        private string _MASABILGI;
        #endregion

        #region özellikler
        //CTRL+R+E İLE KISACA ÖZELLİK EKLEYEBİLİİRİZ
        public int ID { get => _ID; set => _ID = value; }
        public int KAPASITE { get => _KAPASITE; set => _KAPASITE = value; }
        public int DURUM { get => _DURUM; set => _DURUM = value; }
        public int ONAY { get => _ONAY; set => _ONAY = value; }
        public string MASABILGI { get => _MASABILGI; set => _MASABILGI = value; }
        #endregion

        cGenel gnl=new cGenel();
       
        public int MasaNumarasıBulma(string masa)
        {
            if (masa == null) return 0 ;
            string aa = masa;
            int uzunluk = aa.Length;
            if (uzunluk == 8)
            {
                return Convert.ToInt32(aa.Substring(uzunluk - 1, 1));
            }
            else
            {
                return Convert.ToInt32(aa.Substring(uzunluk - 2, 2));
            }

        }
        public bool MasaDurumunuGetir(int ButonIsmi, int durum)
        {
            bool sonuc=false;
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select durum From Masalar Where ID=@MasaId and DURUM=@durumm", baglantı);
            komut.Parameters.Add("@MasaId", SqlDbType.Int).Value = ButonIsmi;
            komut.Parameters.Add("@durumm", SqlDbType.Int).Value = durum;
            try
            {
                if (baglantı.State == ConnectionState.Closed)
                { //bağlantı açık mı değil mi diye kontrol ediyoruz
                    baglantı.Open();
                }               
                sonuc = Convert.ToBoolean(komut.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMasalar";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglantı);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                baglantı.Dispose();
                baglantı.Close();//bağlantı kapatılır
            }
            return sonuc;

        }
        public void MasayaYeniDurumAtama(string Butonİsmi, int durum)
        {
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Update masalar Set DURUM=@Durum where ID=@MasaNo", baglantı);
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
            }
            string masaNo = "";
            string aa = Butonİsmi;
            int uzunluk = aa.Length;
            komut.Parameters.Add("@Durum", SqlDbType.Int).Value = durum;           
            if (uzunluk>8)
            {
                masaNo = aa.Substring(uzunluk - 2, 2);
                                             
            }
            else if (uzunluk==2)
            {
                masaNo = aa;
            }
            else 
            {
                masaNo = aa.Substring(uzunluk - 1, 1);
            }

            komut.Parameters.Add("@MasaNo", SqlDbType.Int).Value = masaNo;
            komut.ExecuteNonQuery();
            baglantı.Dispose ();
            baglantı.Close();
        }
        public void masaKapasiteDurumGetir(ComboBox cb)
        {
            cb.Items.Clear();
            //string durum = "";
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("SELECT * FROM masalar ", baglanti);
            SqlDataReader dr = null;
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cMasalar c = new cMasalar();
                    //if (c._DURUM == 3)
                    //{
                    //    durum = "DOLU";
                    //}
                    //else if (c._DURUM == 2)
                    //{
                    //    durum = "REZERVE";
                    //}
                    c._KAPASITE = Convert.ToInt32(dr["KAPASITE"]);
                    c.MASABILGI = "Masa No: " + dr["ID"].ToString() + " Kapasitesi: " + dr["KAPASITE"].ToString();
                    c._ID = Convert.ToInt32(dr["ID"]);
                    cb.Items.Add(c);
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMasalar";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglanti);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                dr.Close();
                baglanti.Dispose();
                baglanti.Close();
            }

        }
        public override string ToString()
        {
            return MASABILGI;
        }
    }
}
