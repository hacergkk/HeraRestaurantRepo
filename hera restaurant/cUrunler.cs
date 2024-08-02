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
    internal class cUrunler
    {
        cGenel gnl = new cGenel();
        #region Fields
        private int _urunId;
        private int _urunTurNo;
        private string _urunAd;
        private decimal _fiyat;
        private string _aciklama;
        #endregion
        #region Properties
        public int UrunId { get => _urunId; set => _urunId = value; }
        public int UrunTurNo { get => _urunTurNo; set => _urunTurNo = value; }
        public string UrunAd { get => _urunAd; set => _urunAd = value; }
        public decimal Fiyat { get => _fiyat; set => _fiyat = value; }
        public string Aciklama { get => _aciklama; set => _aciklama = value; }
        #endregion
        public void urunleriListeleUrunAd(ListView lv, string urunAdi)//Ürün adına göre ürün listele
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select urunler.*, KATEGORI From urunler INNER JOIN kategoriler on kategoriler.ID=urunler.KATEGORIID WHERE (urunler.DURUM=0) AND (URUNAD like '%' + @urunAdi + '%') ", baglanti);
            SqlDataReader dr = null;
            komut.Parameters.Add("@urunAdi", SqlDbType.VarChar).Value = urunAdi;
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
                    lv.Items[sayac].SubItems.Add(dr["KATEGORIID"].ToString()); //list viewe veri tabanındaki tüm ürünleri getirdik
                    lv.Items[sayac].SubItems.Add(dr["KATEGORI"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ACIKLAMA"].ToString());
                    lv.Items[sayac].SubItems.Add(string.Format("{0:0#00.0}", dr["FIYAT"].ToString()));
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunler";
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
        public int urunEkle(cUrunler u)//Ürün ekleme
        {
            int sonuc = 0;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Insert Into urunler (KATEGORIID,URUNAD,ACIKLAMA,FIYAT) values(@KATEGORIID,@URUNAD,@ACIKLAMA,@FIYAT)", baglanti);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.Parameters.Add("@URUNAD", SqlDbType.VarChar).Value = u._urunAd;
                komut.Parameters.Add("@KATEGORIID", SqlDbType.Int).Value = u._urunTurNo;
                komut.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = u._aciklama;
                komut.Parameters.Add("@FIYAT", SqlDbType.Money).Value = u._fiyat;
                sonuc = Convert.ToInt32(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunler";
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
        public void urunleriListele(ListView lv)//Ürünleri ve kategorileri listeleyecek
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select urunler.*, KATEGORI From urunler INNER JOIN kategoriler on kategoriler.ID=urunler.KATEGORIID WHERE urunler.DURUM=0", baglanti);
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
                    lv.Items[sayac].SubItems.Add(dr["KATEGORIID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORI"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNAD"].ToString());                    
                    lv.Items[sayac].SubItems.Add(string.Format("{0:0#00.0}", dr["FIYAT"].ToString()));
                    lv.Items[sayac].SubItems.Add(dr["ACIKLAMA"].ToString()); 
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunler";
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
        public int urunleriGuncelle(cUrunler u)//Ürünleri güncelleme
        {
            int sonuc = 0;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Update urunler set URUNAD=@URUNAD,KATEGORIID=@KATEGORIID,ACIKLAMA=@ACIKLAMA,FIYAT=@FIYAT where ID=@urunId", baglanti);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.Parameters.Add("@urunId", SqlDbType.Int).Value = u._urunId;
                komut.Parameters.Add("@URUNAD", SqlDbType.VarChar).Value = u._urunAd;
                komut.Parameters.Add("@KATEGORIID", SqlDbType.Int).Value = u._urunTurNo;
                komut.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = u._aciklama;
                komut.Parameters.Add("@FIYAT", SqlDbType.Money).Value = u._fiyat;
                sonuc = Convert.ToInt32(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunler";
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
        public int urunSil(cUrunler u, int kategoriId)//Ürünleri silme
        {
            int sonuc = 0;
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            string sql = "Update urunler set DURUM=1 where ";
            if (kategoriId == 0)
            {
                sql += "KATEGORIID=@urunId";//Kategoriye ait ürünleri de sil
            }
            else
            {
                sql += "ID=@urunId";
            }
            SqlCommand cmd = new SqlCommand(sql, baglanti);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                cmd.Parameters.Add("@urunId", SqlDbType.Int).Value = u._urunId;
                sonuc = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunler";
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
        public void urunleriListeleUrunId(ListView lv, int urunId)//Ürün ID'sine göre ürün listele
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select urunler.*, KATEGORI From urunler INNER JOIN kategoriler on kategoriler.ID=urunler.KATEGORIID WHERE urunler.DURUM=0 AND urunler.KATEGORIID=@urunId", baglanti);
            SqlDataReader dr = null;
            komut.Parameters.Add("@urunId", SqlDbType.Int).Value = urunId;
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
                    lv.Items[sayac].SubItems.Add(dr["KATEGORIID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORI"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(string.Format("{0:0#00.0}", dr["FIYAT"].ToString()));
                    lv.Items[sayac].SubItems.Add(dr["ACIKLAMA"].ToString());                   
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunler";
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
        public void urunleriListeleIstatistiklereGore(ListView lv, DateTimePicker Baslangic, DateTimePicker Bitis)//İki tarih arası tüm ürünleri getirir
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("SELECT TOP 10 dbo.urunler.URUNAD, SUM(dbo.satislar.ADET) as adeti FROM dbo.kategoriler INNER JOIN dbo.urunler ON dbo.kategoriler.ID=dbo.urunler.KATEGORIID INNER JOIN dbo.satislar ON dbo.urunler.ID=dbo.satislar.URUNID INNER JOIN dbo.hesap ON dbo.satislar.HESAPID=dbo.hesap.ID WHERE (CONVERT(datetime,TARIH,104) BETWEEN CONVERT (datetime,@Baslangic,104) AND CONVERT(datetime,@Bitis,104)) GROUP BY dbo.urunler.URUNAD ORDER BY adeti DESC", baglanti);
            SqlDataReader dr = null;
            komut.Parameters.Add("@Baslangic", SqlDbType.VarChar).Value = Baslangic.Value.ToShortDateString();
            komut.Parameters.Add("@Bitis", SqlDbType.VarChar).Value = Bitis.Value.ToShortDateString();
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
                    lv.Items.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["adeti"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunler";
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
        
        public void urunleriListeleIstatistiklereGoreUrunId(ListView lv, DateTimePicker Baslangic, DateTimePicker Bitis, int urunKategoriId)
        { //İki tarih arası belirli kategoride ki ürünleri getirir
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("SELECT TOP 10 dbo.urunler.URUNAD, SUM(dbo.satislar.ADET) as adeti FROM" +
                " dbo.kategoriler INNER JOIN dbo.urunler ON dbo.kategoriler.ID=dbo.urunler.KATEGORIID INNER JOIN" +
                " dbo.satislar ON dbo.urunler.ID=dbo.satislar.URUNID INNER JOIN dbo.hesap ON " +
                "dbo.satislar.HESAPID=dbo.hesap.ID WHERE (CONVERT(datetime,TARIH,104) BETWEEN CONVERT " +
                "(datetime,@Baslangic,104) AND CONVERT(datetime,@Bitis,104)) AND (dbo.urunler.KATEGORIID=@urunKategoriId)" +
                " GROUP BY dbo.urunler.URUNAD ORDER BY adeti DESC", baglanti);
            SqlDataReader dr = null;
            komut.Parameters.Add("@Baslangic", SqlDbType.VarChar).Value = Baslangic.Value.ToShortDateString();
            komut.Parameters.Add("@Bitis", SqlDbType.VarChar).Value = Bitis.Value.ToShortDateString();
            komut.Parameters.Add("@urunKategoriId", SqlDbType.Int).Value = urunKategoriId;
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
                    lv.Items.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["adeti"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cUrunler";
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

    }
}
