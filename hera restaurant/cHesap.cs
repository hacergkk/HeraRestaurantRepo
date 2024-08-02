using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace hera_restaurant
{
    internal class cHesap
    {
        cGenel gnl = new cGenel();
        #region degiskenler
        private int _ID;
        private decimal _Tutar;
        private DateTime _Tarih;
        private int _PersonelId;
        private int _Durum;
        private int _MasaId;
        #endregion 
        //ctrl k+s ile alan oluşturabiliriz
        #region Ozellikler
        public int ID { get => _ID; set => _ID = value; }
        public decimal Tutar { get => _Tutar; set => _Tutar = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
        public int PersonelId { get => _PersonelId; set => _PersonelId = value; }
        public int Durum { get => _Durum; set => _Durum = value; }
        public int MasaId { get => _MasaId; set => _MasaId = value; }
        #endregion
        public int HesapGetir(int MasaId)
        {
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select top 1 ID From Hesap Where" +
                " MASAID=@MASAID Order by ID desc", baglantı);//tersten sırala ve 1 kayıt getir
            komut.Parameters.Add("@MASAID", SqlDbType.Int).Value = MasaId;
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                MasaId = Convert.ToInt32(komut.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cHesap-HesapGetir";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglantı);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();

            }
            finally
            {
                baglantı.Close();//bağlantı kapatılır
            }
            return MasaId;
        }
        public void YeniHesapAcma(cHesap Bilgiler)
        {
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Insert Into hesap(TARIH,PERSONELID,MASAID,DURUM)" +
                " values (@Tarih,@PersonelId,@MasaId,@Durum)", baglantı);
            try
            {
                if (baglantı.State == ConnectionState.Closed)
                {
                    baglantı.Open();
                }
                //komut.Parameters.Add("@ServisTurNo",SqlDbType.Int).Value = Bilgiler.ServisTurNo;
                komut.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = Bilgiler.Tarih;
                komut.Parameters.Add("@PersonelId", SqlDbType.Int).Value = Bilgiler.PersonelId;
                komut.Parameters.Add("@MasaId", SqlDbType.Int).Value = Bilgiler.MasaId;
                komut.Parameters.Add("@Durum", SqlDbType.Bit).Value = 0;
                Convert.ToBoolean(komut.ExecuteNonQuery());

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cHesap-YeniHesapAcma";
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
        }

        public void HesapKapatma(int hesapId)
        {

            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Update hesap set hesap.DURUM=1 where ID=@hesapId", baglantı);
            try
            {
                if (baglantı.State == ConnectionState.Closed)
                {
                    baglantı.Open();
                }
                komut.Parameters.Add("@hesapId", SqlDbType.Int).Value = hesapId;
                // komut.Parameters.Add("@durum", SqlDbType.Int).Value = durum;
                komut.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cHesap-HesapKapatma";
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

        }
        public int rezervasyonHesapAc(cHesap Bilgiler) //Yeni müşteri yeni hesap açılıyor
        {
            int sonuc = 0;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Insert Into hesap(TARIH,PERSONELID,MASAID,DURUM) values (@Tarih,@PersonelID,@MasaId,0); Select SCOPE_IDENTITY()", baglanti);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = Bilgiler.Tarih;
                komut.Parameters.Add("@PersonelID", SqlDbType.Int).Value = Bilgiler.PersonelId;
                komut.Parameters.Add("@MasaId", SqlDbType.Int).Value = Bilgiler.MasaId;
                sonuc = Convert.ToInt32(komut.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cHesap-rezervasyonHesapAc";
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
