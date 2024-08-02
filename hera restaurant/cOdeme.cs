using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hera_restaurant
{
    internal class cOdeme
    {
        cGenel gnl=new cGenel();
        #region Degiskenler
        private int _OdemeID;
        private int _HesapID;
        private int _OdemeTurId;
        private decimal _AraToplam;
        private decimal _Indirim;
        private decimal _Kdvtutari;
        private decimal _GenelToplam;
        private DateTime _Tarih;
        private int _MusteriId;
        #endregion

        #region Ozellikler
        public int OdemeID { get => _OdemeID; set => _OdemeID = value; }
        public int HesapID { get => _HesapID; set => _HesapID = value; }
        public int OdemeTurId { get => _OdemeTurId; set => _OdemeTurId = value; }
        public decimal AraToplam { get => _AraToplam; set => _AraToplam = value; }
        public decimal Indirim { get => _Indirim; set => _Indirim = value; }
        public decimal Kdvtutari { get => _Kdvtutari; set => _Kdvtutari = value; }
        public decimal GenelToplam { get => _GenelToplam; set => _GenelToplam = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
        public int MusteriId { get => _MusteriId; set => _MusteriId = value; }
        #endregion

        //müsterinin masa hesabını kapatıyoruz
        public bool HesapKapatma(cOdeme hesap)
        {
            bool sonuc = false;
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Insert Into hesap_odemeleri(HESAPID,ODEME_TURU,MUSTERIID,ARA_TOPLAM,KDV_TUTARI,INDIRIM,TOPLAM_TUTAR)" +
                " values (@HESAPID,@ODEME_TURU,@MUSTERIID,@ARA_TOPLAM,@KDV_TUTARI,@INDIRIM,@TOPLAM_TUTAR)", baglantı);
            try
            {
                if (baglantı.State == ConnectionState.Closed)
                {
                    baglantı.Open();
                }
                komut.Parameters.Add("@HESAPID", SqlDbType.Int).Value = hesap.HesapID;
                komut.Parameters.Add("@ODEME_TURU", SqlDbType.Int).Value = hesap.OdemeTurId;
                komut.Parameters.Add("@MUSTERIID", SqlDbType.Int).Value = hesap.MusteriId;
                komut.Parameters.Add("@ARA_TOPLAM", SqlDbType.Money).Value = hesap.AraToplam;
                komut.Parameters.Add("@KDV_TUTARI", SqlDbType.Money).Value = hesap.Kdvtutari;
                komut.Parameters.Add("@INDIRIM", SqlDbType.Money).Value = hesap.Indirim;
                komut.Parameters.Add("@TOPLAM_TUTAR", SqlDbType.Money).Value = hesap.GenelToplam;
                sonuc=Convert.ToBoolean(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
                string konum = "cOdeme";
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
    }
}
