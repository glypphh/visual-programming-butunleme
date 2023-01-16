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
    public partial class AracListele : Form
    {
        public AracListele()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Server=localhost;Port=6605;Database=otokiralama;uid=root;pwd=yusuf;";
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void Arac_Listele()
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            String komutCumlesi = "Select * From Araclar";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        public void Arac_Guncelle()
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Update Araclar set Marka=@marka,Seri=@seri,Model=@model,Renk=@renk,Kilometre=@km,Yakıt=@yakit,Kira_Ücreti=@ücret where Plaka=@plaka";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut.Parameters.AddWithValue("@marka", cbxMarka.Text);
            komut.Parameters.AddWithValue("@seri", cbxSeri.Text);
            komut.Parameters.AddWithValue("@model", txtModel.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@km", txtKm.Text);
            komut.Parameters.AddWithValue("@yakit", cbxYakit.Text);
            komut.Parameters.AddWithValue("@ücret", txtÜcret.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Arac_Listele();

        }
        private void AracListele_Load(object sender, EventArgs e)
        {
            Arac_Listele();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            Arac_Guncelle();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPlaka.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cbxMarka.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cbxSeri.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtModel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtRenk.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtKm.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            cbxYakit.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtÜcret.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Delete from Araclar where Plaka='" + dataGridView1.CurrentRow.Cells["Plaka"].Value.ToString() + "'";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close();
            Arac_Listele();
        }
    }
}
