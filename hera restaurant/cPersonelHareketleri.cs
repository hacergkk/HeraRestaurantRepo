using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;

namespace hera_restaurant
{
    internal class cPersonelHareketleri
    {
        #region ozellikler
        private int _ID;
        private int _PersonelId;
        private string _Islem;
        private DateTime _Tarih;
        #endregion

        #region alanlar
        public int ID { get => _ID; set => _ID = value; }
        public int PersonelId { get => _PersonelId; set => _PersonelId = value; }
        public string Islem { get => _Islem; set => _Islem = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
        #endregion
        public bool _personelHareketKaydetme(cPersonelHareketleri ph)
        {
            cGenel gnl=new cGenel();
            bool sonuc = false;
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Insert Into personel_hareketleri(PERSONELID,ISLEM,TARIH)Values(@personelId,@islem,@tarih)", baglantı);
            try
            {
                if(baglantı.State==ConnectionState.Closed)
                {
                    baglantı.Open();
                }
                komut.Parameters.Add("@personelId",SqlDbType.Int).Value = ph._PersonelId;
                komut.Parameters.Add("@islem",SqlDbType.VarChar).Value = ph._Islem;
                komut.Parameters.Add("@tarih",SqlDbType.DateTime).Value = ph._Tarih;
                sonuc=Convert.ToBoolean(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cPersonelHareketleri";
                SqlCommand komut2 = new SqlCommand("Insert Into Errors(Error,Location) values(@Hata,@Yer)", baglantı);
                komut2.Parameters.Add("@Hata", SqlDbType.NVarChar).Value = hata;
                komut2.Parameters.Add("@Yer", SqlDbType.NVarChar).Value = konum;
                komut2.ExecuteNonQuery();
            }

            return sonuc;
        }
    }
}
