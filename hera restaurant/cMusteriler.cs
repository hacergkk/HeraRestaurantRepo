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
    internal class cMusteriler
    {
        cGenel gnl = new cGenel();

        #region Fields
        private int _musteriId;
        private string _musteriad;
        private string _musterisoyad;
        private string _telefon;
        private string _adres;
        private string _email;
        #endregion
        #region Properties
        public int MusteriId { get => _musteriId; set => _musteriId = value; }
        public string Musteriad { get => _musteriad; set => _musteriad = value; }
        public string Musterisoyad { get => _musterisoyad; set => _musterisoyad = value; }
        public string Telefon { get => _telefon; set => _telefon = value; }
        public string Adres { get => _adres; set => _adres = value; }
        public string Email { get => _email; set => _email = value; }
        #endregion
        public bool MusteriVarMi(string tlf)
        {
            bool sonuc = false;
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglantı;
            komut.CommandText = "MusteriVarMi";
            komut.CommandType = CommandType.StoredProcedure; //veritabanına prosedür yazdım
            komut.Parameters.Add("@telefon", SqlDbType.VarChar).Value = tlf;
            komut.Parameters.Add("@sonuc", SqlDbType.Int);
            komut.Parameters["@sonuc"].Direction = ParameterDirection.Output;
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
            }
            try
            {
                komut.ExecuteNonQuery();
                sonuc = Convert.ToBoolean(komut.Parameters["@sonuc"].Value);
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMusteriler";
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
        public int MusteriEkle(cMusteriler m)
        {
            int sonuc = 0;
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Insert Into musteriler(AD,SOYAD,TELEFON,ADRES,EMAIL)" +
                " values(@ad,@soyad,@telefon,@adres, @email); Select SCOPE_IDENTITY()", baglantı);
            //scope_ıdentity eklediğim müşterinin ıdsini almak
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                komut.Parameters.Add("@ad", SqlDbType.VarChar).Value = m._musteriad;
                komut.Parameters.Add("@soyad", SqlDbType.VarChar).Value = m._musterisoyad;
                komut.Parameters.Add("@telefon", SqlDbType.VarChar).Value = m._telefon;
                komut.Parameters.Add("@adres", SqlDbType.VarChar).Value = m._adres;
                komut.Parameters.Add("@email", SqlDbType.VarChar).Value = m._email;
             
                sonuc = Convert.ToInt32(komut.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMusteriler";
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
        }//Veritabanına müşteri ekleme
        public bool MusteriBilgileriGuncelle(cMusteriler m)//Seçili müşteri bilgisini güncelleme
        {
            bool sonuc = false;
            SqlConnection baglantı = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Update musteriler set AD=@ad,SOYAD=@soyad," +
                "TELEFON=@telefon,ADRES=@adres,EMAIL=@email where ID=@musteriId", baglantı);
            try
            {
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();
                komut.Parameters.Add("@ad", SqlDbType.VarChar).Value = m._musteriad;
                komut.Parameters.Add("@soyad", SqlDbType.VarChar).Value = m._musterisoyad;
                komut.Parameters.Add("@telefon", SqlDbType.VarChar).Value = m._telefon;
                komut.Parameters.Add("@adres", SqlDbType.VarChar).Value = m._adres;
                komut.Parameters.Add("@email", SqlDbType.VarChar).Value = m._email;
                komut.Parameters.Add("@musteriId", SqlDbType.VarChar).Value = m._musteriId;
                sonuc = Convert.ToBoolean(komut.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMusteriler";
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
        public void MusterileriGetir(ListView lv)//Veritabanından gelen verileri listview'e ekleme
        {

            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select *from musteriler", baglanti);
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
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["TELEFON"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADRES"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["EMAIL"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMusteriler";
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
        public void MusterileriGetirID(int musteriId, TextBox ad, TextBox soyad, TextBox telefon, TextBox adres, TextBox email)//ID'ye göre müşterileri getir
        {
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select *from musteriler where ID=@musteriId", baglanti);
            SqlDataReader dr = null;
            komut.Parameters.Add("@musteriId", SqlDbType.Int).Value = musteriId;
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    ad.Text = dr["AD"].ToString();
                    soyad.Text = dr["SOYAD"].ToString();
                    telefon.Text = dr["TELEFON"].ToString();
                    adres.Text = dr["ADRES"].ToString();
                    email.Text = dr["EMAIL"].ToString();
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMusteriler";
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
        public void MusterileriGetirAD(ListView lv, string musteriAd)//Ad'a göre müşteri getir
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select *from musteriler where AD " +
                "like @musteriAd + '%' ", baglanti);
            SqlDataReader dr = null;
            komut.Parameters.Add("@musteriAd", SqlDbType.VarChar).Value = musteriAd;
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
                    lv.Items.Add(Convert.ToInt32(dr["ID"]).ToString());
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["TELEFON"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADRES"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["EMAIL"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMusteriler";
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
        public void MusterileriGetirSOYAD(ListView lv, string musteriSoyad)//Soyad'a göre müşteri getir
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select *from musteriler where SOYAD like @musteriSoyad + '%' ", baglanti);
            SqlDataReader dr = null;
            komut.Parameters.Add("@musteriSoyad", SqlDbType.VarChar).Value = musteriSoyad;
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
                    lv.Items.Add(Convert.ToInt32(dr["ID"]).ToString());
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["TELEFON"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADRES"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["EMAIL"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMusteriler";
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
        public void MusterileriGetirTELEFON(ListView lv, string musteriTelefon)//Telefona göre müşteri getir
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select *from musteriler where TELEFON like @musteriTelefon + '%' ", baglanti);
            SqlDataReader dr = null;
            komut.Parameters.Add("@musteriTelefon", SqlDbType.VarChar).Value = musteriTelefon;
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
                    lv.Items.Add(Convert.ToInt32(dr["ID"]).ToString());
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["TELEFON"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADRES"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["EMAIL"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMusteriler";
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
        public void MusterileriGetirADRES(ListView lv, string musteriAdres)//Adrese göre müşteri getir
        {
            lv.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("Select *from musteriler where ADRES like @musteriAdres + '%' ", baglanti);
            SqlDataReader dr = null;
            komut.Parameters.Add("@musteriAdres", SqlDbType.VarChar).Value = musteriAdres;
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
                    lv.Items.Add(Convert.ToInt32(dr["ID"]).ToString());
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["TELEFON"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADRES"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["EMAIL"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cMusteriler";
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
