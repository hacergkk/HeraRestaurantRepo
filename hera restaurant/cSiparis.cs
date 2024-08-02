using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace hera_restaurant
{
    internal class cSiparis
    {
        #region degiskenler
        cGenel gnl =new cGenel();
        private int _ID;
        private int _hesapID;
        private int _urunID;
        private int _adet;
        private int _masaId;
#endregion
        #region ozellikler
        public int ID { get => _ID; set => _ID = value; }
        public int HesapID { get => _hesapID; set => _hesapID = value; }
        public int UrunID { get => _urunID; set => _urunID = value; }
        public int Adet { get => _adet; set => _adet = value; }
        public int MasaId { get => _masaId; set => _masaId = value; }
       
        #endregion
        public void SiparisBilgileriniGetir(ListView lv, int HesapId)
        {
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select URUNAD,FIYAT,satislar.ID,satislar.URUNID," +
                "satislar.ADET From satislar Inner Join urunler on" +
                " satislar.URUNID=Urunler.ID Where HESAPID=@HesapId", baglantı);
            SqlDataReader dr = null;
            komut.Parameters.Add("@HesapId", SqlDbType.Int).Value = HesapId;
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                dr =komut.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADET"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNID"].ToString());
                    lv.Items[sayac].SubItems.Add(Convert.ToString(Convert.ToDecimal(dr["FIYAT"])*
                        Convert.ToDecimal(dr["ADET"])));
                    lv.Items[sayac].SubItems.Add(dr["ID"].ToString());
                    sayac++;                   
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cSiparis";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglantı);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                 dr.Close();
                baglantı.Dispose();
                baglantı.Close();   
            }

        }
        public bool HesabıVeriTabanınaKaydetme(cSiparis Bilgiler)
        {
            bool sonuc = false;

            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Insert Into satislar(HESAPID,URUNID,ADET,MASAID) values(@HesapId,@UrunId,@Adet,@MasaId)", baglantı);
            try
            {
                if (baglantı.State == ConnectionState.Closed)
                {
                    baglantı.Open();
                }
                komut.Parameters.Add("@HesapId", SqlDbType.Int).Value = Bilgiler._hesapID;
                komut.Parameters.Add("@UrunId", SqlDbType.Int).Value = Bilgiler._urunID;
                komut.Parameters.Add("@Adet", SqlDbType.Int).Value = Bilgiler._adet;
                komut.Parameters.Add("@MasaId", SqlDbType.Int).Value = Bilgiler._masaId;
                sonuc = Convert.ToBoolean(komut.ExecuteNonQuery());

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cSiparis";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglantı);
                komut2.Parameters.Add("@Hata",SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer",SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }
            finally
            {
                baglantı.Dispose();
                baglantı.Close();
            }
            return sonuc;
        }
        public void SiparistenUrunSilme(int satisId)
        {
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Delete From satislar Where ID=@satisId", baglantı);

            komut.Parameters.Add("@satisId", SqlDbType.Int).Value= satisId;
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
            }
            komut.ExecuteNonQuery();
            baglantı.Dispose();
            baglantı.Close();
        }
    }
}
