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
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(""); //Veritabanı adresinin buraya girilmesi gerekiyor. EN The database address must be entered here.

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            // grafik 1
            baglanti.Open();
            SqlCommand komutg1 = new SqlCommand("select persehir, count(*) From Tbl_Personel Group by PerSehir", baglanti);
            SqlDataReader dr1 = komutg1.ExecuteReader();
            while (dr1.Read())
            {
                chart1.Series["Sehirler"].Points.AddXY(dr1[0], dr1[1]);
            }
            baglanti.Close();

            //grafik 2
            baglanti.Open();
            SqlCommand komutg2 = new SqlCommand("Select permeslek, avg(permaas) from tbl_personel group by permeslek", baglanti);
            SqlDataReader dr2 = komutg2.ExecuteReader();
            while (dr2.Read())
            {
                chart2.Series["Meslek - Maas"].Points.AddXY(dr2[0], dr2[1]);
            }
            baglanti.Close();

        }

    }
    
}
