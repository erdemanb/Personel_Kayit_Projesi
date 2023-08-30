using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace personel_kayit_projesi_tasarim_ekrani
{
    public partial class FrmIstatistik : Form
    {
        public FrmIstatistik()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("");//buraya veritabanı adresi girilmesi gerekiyor. EN The database address must be entered here. 

        private void FrmIstatistik_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select count(*) From Tbl_Personel",baglanti);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblpersonelsayi.Text = dr1[0].ToString();
            }
            baglanti.Close();

            //evli personel sayısı
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select count (*) from Tbl_Personel where perdurum = 1",baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblevlipersayi.Text = dr2[0].ToString();
            }
            baglanti.Close();

            //bekar personel sayısı
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select count (*) from Tbl_Personel where perdurum = 0", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblbekarpersayi.Text = dr3[0].ToString();
            }
            baglanti.Close();

            //sehir sayısı
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("Select count (distinct (persehir)) from tbl_personel", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblsehirsayisi.Text = dr4[0].ToString();
            }
            baglanti.Close();

            //toplam maas
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("select sum(permaas) from tbl_personel", baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lbltoplammaas.Text = dr5[0].ToString();
            }
            baglanti.Close();

            //ortalama maas
            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("select avg(permaas) from tbl_personel", baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblortmaas.Text = dr6[0].ToString();
            }
            baglanti.Close();
        }
    }
}
