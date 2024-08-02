using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace hera_restaurant
{
    internal class cUrunCesitleri
    {
        cGenel gnl=new cGenel();
        #region değişkenler
        private int _UrunTurNo;
        private string _KategoriAd;
        private string _Aciklama;
        #endregion

        #region özellikler
        public int UrunTurNo { get => _UrunTurNo; set => _UrunTurNo = value; }
        public string KategoriAd { get => _KategoriAd; set => _KategoriAd = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; } 
        #endregion
        public void UrunTipiBulma(ListView Cesitler,Button btn)
        {
            Cesitler.Items.Clear();
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select URUNAD,FIYAT,urunler.ID From kategoriler Inner Join urunler on kategoriler.ID=Urunler.KATEGORIID Where urunler.KATEGORIID=@KATEGORIID", baglantı);
            //kategorieler ve urunler arasındaki bağlantıyı kurduk
            string aa = btn.Name;
            int uzunluk = aa.Length;
            komut.Parameters.Add("@KATEGORIID", SqlDbType.Int).Value = aa.Substring(uzunluk-1,1);//burasının paremetrelerinden emin değilim 

            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
            }
            SqlDataReader dr = komut.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                Cesitler.Items.Add(dr["URUNAD"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["ID"].ToString());
                i++;

            }
            dr.Close();
            baglantı.Dispose();
            baglantı.Close();
            
        } 
        public void UrunArama(ListView Cesitler,int txt)
        {
            Cesitler.Items.Clear();
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select* from urunler Where ID=@ID", baglantı);
            //kategorieler ve urunler arasındaki bağlantıyı kurduk

            komut.Parameters.Add("@ID", SqlDbType.Int).Value = txt; 

            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
            }
            SqlDataReader dr = komut.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                Cesitler.Items.Add(dr["URUNAD"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["ID"].ToString());
                i++;

            }
            dr.Close();
            baglantı.Dispose();
            baglantı.Close();
            
        }

        public void urunCesitleriniGetirCB(ComboBox cb)//Ürün çeşitlerini getir Combobox
        {
            cb.Controls.Clear();
            cb.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select * FROM kategoriler Where DURUM=0", baglanti);
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
                    cUrunCesitleri uc = new cUrunCesitleri();
                    uc._UrunTurNo = Convert.ToInt32(dr["ID"]);
                    uc._KategoriAd = (dr["KATEGORI"]).ToString();
                    uc._Aciklama = (dr["ACIKLAMA"]).ToString();
                    cb.Items.Add(uc);//Overload işlemi yapıyoruz
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunCesitleri";
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
        public void urunCesitleriniGetirLV(ListView lv)//Ürün çeşitlerini getir Listview
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select * FROM kategoriler WHERE DURUM=0", baglanti);
            SqlDataReader dr = null;
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                dr = komut.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORI"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ACIKLAMA"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunCesitleri";
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
        public void urunCesitleriniAra(ListView lv, string arama)//Ürün çeşitlerini getir Listview Arama
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select * FROM kategoriler WHERE (DURUM=0) AND (KATEGORI like '%' + @arama+'%')", baglanti);
            komut.Parameters.Add("arama", SqlDbType.VarChar).Value = arama;
            SqlDataReader dr = null;
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                dr = komut.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORI"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ACIKLAMA"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunCesitleri";
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
        public int urunCesitleriniEkle(cUrunCesitleri u)//Ürün çeşitleri ekleme
        {
            int sonuc = 0;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Insert Into kategoriler (KATEGORI,ACIKLAMA) values(@KATEGORI,@ACIKLAMA)", baglanti);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.Parameters.Add("@KATEGORI", SqlDbType.VarChar).Value = u._KategoriAd;
                komut.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = u._Aciklama;
                sonuc = Convert.ToInt32(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunCesitleri";
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
        public int urunCesitleriniGuncelle(cUrunCesitleri u)//Ürün çeşitleri güncelleme
        {
            int sonuc = 0;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Update kategoriler set KATEGORI=@KATEGORI,ACIKLAMA=@ACIKLAMA where ID=@KATEGORIID", baglanti);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.Parameters.Add("@KATEGORIID", SqlDbType.Int).Value = u._UrunTurNo;
                komut.Parameters.Add("@KATEGORI", SqlDbType.VarChar).Value = u._KategoriAd;
                komut.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = u._Aciklama;
                sonuc = Convert.ToInt32(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunCesitleri";
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
        public int urunCesitleriniSil(int id)//Ürün çeşitleri silme
        {
            int sonuc = 0;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Update kategoriler set DURUM=1 where ID=@id", baglanti);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.Parameters.Add("@id", SqlDbType.Int).Value = id;
                sonuc = Convert.ToInt32(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunCesitleri";
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
        public override string ToString()
        {
            return KategoriAd;
        }
    }
}
