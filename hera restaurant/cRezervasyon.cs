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
    internal class cRezervasyon
    {
        cGenel gnl=new cGenel();
        #region Degiskenler
        private int _ID;
        private int _MasaId;
        private int _MusteriId;
        private DateTime _Tarih;
        private int _KisiSayisi;
        private string _Aciklama;
        private int _HesapId;
        #endregion

        #region Özellikler
        public int ID { get => _ID; set => _ID = value; }
        public int MasaId { get => _MasaId; set => _MasaId = value; }
        public int MusteriId { get => _MusteriId; set => _MusteriId = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
        public int MusteriSayısı { get => _KisiSayisi; set => _KisiSayisi = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; }
        public int HesapId { get => _HesapId; set => _HesapId = value; } 
        #endregion       
        public int MusteriIdGetirRezervasyondan(int masaId)  //müsteriıd masa nuamrasına göre
        {
            int musteriId = 0;

            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select top 1 MUSTERIID from rezervasyonlar where MASAID=@masaId hesap by MUSTERIID Desc", baglantı);

            try
            {
                if (baglantı.State == ConnectionState.Closed)
                {
                    baglantı.Open();
                }
                komut.Parameters.Add("@masaId", SqlDbType.Int).Value = masaId;
                musteriId = Convert.ToInt32(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cRezervasyon-MusteriIdGetirRezervasyondan";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglantı);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                baglantı.Dispose();
                baglantı.Close();
            }

            return musteriId;
        }
        
        public bool rezervasyonkapatma(int hesapId) //hesap kapatırken rezervasyonlu masayı da kapatmak
        {
            bool sonuc=false;

            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Update rezervasyonlar set durum=0 where HESAPID=@hesapId", baglantı);
            try
            {
                if (baglantı.State == ConnectionState.Closed)
                {
                    baglantı.Open();
                }
                komut.Parameters.Add("@hesapId", SqlDbType.Int).Value = hesapId;
                sonuc = Convert.ToBoolean(komut.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cRezervasyon-rezervasyonkapatma";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglantı);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                baglantı.Dispose();
                baglantı.Close();
            }


            return sonuc;

        }       
        public bool rezervasyonAcikMiKontrol(int musteriNo)//Rezervasyon açık mı kontrolü
        {
            bool result = false;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("SELECT top 1 rezervasyonlar.ID FROM rezervasyonlar WHERE (MUSTERIID=@musteriNo) AND (DURUM=1) ORDER BY ID DESC", baglanti);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.Parameters.Add("musteriNo", SqlDbType.Int).Value = musteriNo;
                result = Convert.ToBoolean(komut.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cRezervasyon-rezervasyonAcikMiKontrol";
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
            return result;
        }

        public bool rezervasyonAc(cRezervasyon r)//Rezervasyon aç
        {
            bool result = false;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("INSERT INTO rezervasyonlar (MUSTERIID,MASAID,HESAPID,KISI_SAYISI,TARIH,ACIKLAMA,DURUM) values (@MUSTERIID,@MASAID,@HESAPID,@KISI_SAYISI,@TARIH,@ACIKLAMA,1)", baglanti);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.Parameters.Add("MUSTERIID", SqlDbType.Int).Value = r._MusteriId;
                komut.Parameters.Add("MASAID", SqlDbType.Int).Value = r._MasaId;
                komut.Parameters.Add("HESAPID", SqlDbType.Int).Value = r._HesapId;
                komut.Parameters.Add("KISI_SAYISI", SqlDbType.Int).Value = r._KisiSayisi;
                komut.Parameters.Add("TARIH", SqlDbType.DateTime).Value = r._Tarih;
                komut.Parameters.Add("ACIKLAMA", SqlDbType.VarChar).Value = r._Aciklama;
                result = Convert.ToBoolean(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cRezervasyon-rezervasyonAc";
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
            return result;
        }
    }
}
