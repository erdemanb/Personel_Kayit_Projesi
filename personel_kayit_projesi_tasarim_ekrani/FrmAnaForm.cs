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
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(""); //buraya veri tabanı adresi girilmesi gerekiyor. EN The database address must be entered here.
        void temizle()
        {
            textBoxID.Text = "";
            textBoxAd.Text = "";
            textBoxSoyad.Text = "";
            textBoxMeslek.Text = "";
            textBoxMaas.Text = "";
            radioButtonBekar.Checked = false;
            radioButtonEvli.Checked = false;
            labelkopru.Text = "False";
            comboBoxSehir.Text = "";
            textBoxAd.Focus();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel); // Oluşturduğunuz veri tabanına göre burası değişiklik gerektirebilir. EN Depending on the database you are creating, this line may need to be modifie
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personel(PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,Perdurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", textBoxAd.Text);
            komut.Parameters.AddWithValue("@p2", textBoxSoyad.Text);
            komut.Parameters.AddWithValue("@p3", comboBoxSehir.Text);
            komut.Parameters.AddWithValue("@p4", textBoxMaas.Text);
            komut.Parameters.AddWithValue("@p5", textBoxMeslek.Text);
            komut.Parameters.AddWithValue("@p6", labelkopru.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Eklendi!");
        }

        private void radioButtonEvli_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEvli.Checked == true)
            {
                labelkopru.Text = "True";
            }
        }

        private void radioButtonBekar_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBekar.Checked == true)
            {
                labelkopru.Text = "False";
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            textBoxID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBoxAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBoxSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            comboBoxSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBoxMaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            labelkopru.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            textBoxMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void labelkopru_TextChanged(object sender, EventArgs e)
        {
            if (labelkopru.Text == "True")
            {
                radioButtonEvli.Checked = true;
                radioButtonBekar.Checked = false;

            }
            if (labelkopru.Text == "False")
            {
                radioButtonBekar.Checked = true;
                radioButtonEvli.Checked = false;

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand();
            using (komutsil = baglanti.CreateCommand())
            {
                komutsil.Connection = baglanti;
                komutsil.CommandType = CommandType.Text;
                komutsil.Parameters.AddWithValue("@k1", textBoxID.Text);
                komutsil.CommandText = "Delete From Tbl_Personel Where PerID=@k1";
            }
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi!");

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komutguncelle = new SqlCommand("Update Tbl_Personel Set PerAd=@a1, PerSoyad = @a2, PerSehir = @a3, PerMaas = @a4, PerDurum = @a5, PerMeslek = @a6 where PerID = @a7", baglanti);
                komutguncelle.Parameters.AddWithValue("@a1", textBoxAd.Text);
                komutguncelle.Parameters.AddWithValue("@a2", textBoxSoyad.Text);
                komutguncelle.Parameters.AddWithValue("@a3", comboBoxSehir.Text);
                komutguncelle.Parameters.AddWithValue("@a4", textBoxMaas.Text);
                komutguncelle.Parameters.AddWithValue("@a5", labelkopru.Text);
                komutguncelle.Parameters.AddWithValue("@a6", textBoxMeslek.Text);
                komutguncelle.Parameters.AddWithValue("@a7", textBoxID.Text);
                komutguncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Personel Bilgisi Güncellendi!");

            }
            catch (Exception)
            {
                MessageBox.Show("Problem var!");

            }

        }

        private void btnIstatistik_Click(object sender, EventArgs e)
        {
            FrmIstatistik fr = new FrmIstatistik();
            fr.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Ad";
            dataGridView1.Columns[2].Name = "Soyad";
            dataGridView1.Columns[3].Name = "Sehir";
            dataGridView1.Columns[4].Name = "Maas";
            dataGridView1.Columns[5].Name = "Durum";
            dataGridView1.Columns[6].Name = "Meslek";
            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["Ad"].HeaderText = "Ad";
            dataGridView1.Columns["Soyad"].HeaderText = "Soyad";
            dataGridView1.Columns["Sehir"].HeaderText = "Şehir";
            dataGridView1.Columns["Maas"].HeaderText = "Maaş";
            dataGridView1.Columns["Durum"].HeaderText = "Medeni Durum";
            dataGridView1.Columns["Meslek"].HeaderText = "Meslek";
            this.dataGridView1.Sort(this.dataGridView1.Columns["ID"], ListSortDirection.Ascending);


        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frg = new FrmGrafikler();
            frg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Raporlar frr = new Raporlar();
            frr.Show();
        }
    }
}
