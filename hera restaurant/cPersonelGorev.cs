using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace hera_restaurant
{
    internal class cPersonelGorev
    {
        cGenel gnl = new cGenel();
        #region Fields
        private int _personelGorevId;
        private string _tanim;
        #endregion
        #region Properties
        public int PersonelGorevId { get => _personelGorevId; set => _personelGorevId = value; }
        public string Tanim { get => _tanim; set => _tanim = value; }
        #endregion
        public void PersonelGorevGetir(ComboBox cb)
        {
            cb.Items.Clear();
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("SELECT *FROM personel_gorevleri", baglanti);//Personel bilgileri getir
            SqlDataReader dr = null;
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                dr = komut.ExecuteReader();
                while (dr.Read())//Combobox'a verileri gönder
                {
                    cPersonelGorev c = new cPersonelGorev();
                    c._personelGorevId = Convert.ToInt32(dr["ID"].ToString());
                    c._tanim = dr["GOREV"].ToString();
                    cb.Items.Add(c);
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                string konum = "cPersonelGorev";
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

        public string PersonelGorevTanim(int personelId)
        {
            string sonuc = "";
            SqlConnection baglanti = new SqlConnection(gnl.baglantiString);
            SqlCommand komut = new SqlCommand("SELECT GOREV FROM personel_gorevleri WHERE ID=@personelId", baglanti);//Personel bilgileri getir
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
                string konum = "cPersonelGorev";
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
        public override string ToString()//Personel görev getir'de ki tanımı ezdik.
        {
            return _tanim;
        }
    }
}
