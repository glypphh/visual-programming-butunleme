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
using MySql.Data.MySqlClient;

namespace AracKiralama
{
    public partial class MusteriListele : Form
    {
        public MusteriListele()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Server=localhost;Port=6605;Database=otokiralama;uid=root;pwd=yusuf;";
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Musteri_Listele()
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Select Tc_No,Ad_Soyad,Telefon,Mail,Adres from Musteriler";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        public void Musteri_Guncelle()
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Update Musteriler set Ad_Soyad=@adsoyad,Telefon=@telefon,Mail=@mail,Adres=@adres where Tc_No=@tc";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTel.Text);
            komut.Parameters.AddWithValue("@mail", txtMail.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Musteri_Listele();
            
        }
        public void Musteri_Sil()
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Delete from Musteriler where Tc_No='" + dataGridView1.CurrentRow.Cells["Tc_No"].Value.ToString() + "'";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            
            komut.ExecuteNonQuery();
            baglanti.Close();
            Musteri_Listele();
        }
        private void MusteriListele_Load(object sender, EventArgs e)
        {
            Musteri_Listele();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            
        }
        
        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            Musteri_Guncelle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Musteri_Sil();
        }
    }
}
