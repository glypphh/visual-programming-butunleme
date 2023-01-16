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
    public partial class Sozlesme : Form
    {
        public Sozlesme()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Server=localhost;Port=6605;Database=otokiralama;uid=root;pwd=yusuf;";

        public void Arac_Listele()
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Select * From Araclar where Durumu = 'Bos'";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cbxAraclar.Items.Add(read["Plaka"]);
            }
           
        }
        public void Sozlesme_Listele()
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Select * From Sözlesme";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void cbxKiraSekli_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Select Kira_Ücreti From Araclar";
            MySqlCommand komut = new MySqlCommand(komutCumlesi,baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (cbxKiraSekli.SelectedIndex == 0)
                {
                    txtKiraÜcreti.Text =(int.Parse(read["Kira_Ücreti"].ToString()) * 1 ).ToString();
                }
                else if(cbxKiraSekli.SelectedIndex == 1)
                {
                    txtKiraÜcreti.Text = (int.Parse(read["Kira_Ücreti"].ToString()) * 0.80).ToString();
                }
                else if(cbxKiraSekli.SelectedIndex == 2)
                {
                    txtKiraÜcreti.Text = (int.Parse(read["Kira_Ücreti"].ToString()) * 0.70).ToString();
                }

            }

        }

        private void Sozlesme_Load(object sender, EventArgs e)
        {
            Arac_Listele();
            Sozlesme_Listele();
        }

        private void cbxAraclar_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Select * From Araclar where Plaka like '" + cbxAraclar.SelectedItem +"'";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while(read.Read())
            {
                txtMarka.Text = read["Marka"].ToString();
                txtSeri.Text = read["Seri"].ToString();
                txtModel.Text = read["Model"].ToString();
                txtRenk.Text = read["Renk"].ToString();
            }
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            TimeSpan gunfarki = DateTime.Parse(datetimeDönüs.Text) - DateTime.Parse(datetimeCikis.Text);
            int gunhesap = gunfarki.Days;
            txtGün.Text = gunhesap.ToString();

            txtTutar.Text = (gunhesap * int.Parse(txtKiraÜcreti.Text)).ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Insert Into Sözlesme Values (@tcno,@AdSoyad,@Telefon,@ehliyetno,@ehliyettarih,@plaka,@Marka,@Seri,@Model,@Renk,@kirasekli,@kiraücreti,@kiralanangünsayisi,@tutar,@cikistarih,@dönüstarih)";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@tcno", txtTc.Text);
            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTel.Text);
            komut.Parameters.AddWithValue("@ehliyetno", txtEhliyetNo.Text);
            komut.Parameters.AddWithValue("@ehliyettarih", txtEhliyetTarih.Text);
            komut.Parameters.AddWithValue("@plaka", cbxAraclar.Text);
            komut.Parameters.AddWithValue("@Marka", txtMarka.Text);
            komut.Parameters.AddWithValue("@Seri", txtSeri.Text);
            komut.Parameters.AddWithValue("@Model", txtModel.Text);
            komut.Parameters.AddWithValue("@Renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@kirasekli",cbxKiraSekli.SelectedItem);
            komut.Parameters.AddWithValue("@kiraücreti",txtKiraÜcreti.Text);
            komut.Parameters.AddWithValue("@kiralanangünsayisi",txtGün.Text);
            komut.Parameters.AddWithValue("@tutar",txtTutar.Text);
            komut.Parameters.AddWithValue("@cikistarih",datetimeCikis.Value);
            komut.Parameters.AddWithValue("@dönüstarih",datetimeDönüs.Value);

            string komutCumlesiUp = "update Araclar set Durumu = 'Dolu' where Plaka ='" + cbxAraclar.SelectedItem + "'";
            MySqlCommand komutUp = new MySqlCommand(komutCumlesiUp, baglanti);

            komutUp.ExecuteNonQuery();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Sozlesme_Listele();
            Arac_Listele();
            MessageBox.Show("Kayıt Başarılı");
        }

        private void btnAracTeslim_Click(object sender, EventArgs e)
        {
            DataGridViewRow satir = dataGridView1.CurrentRow;
            DateTime bugün = DateTime.Parse(DateTime.Now.ToShortDateString());
            int ucret = int.Parse(satir.Cells["Kira_Ücreti"].Value.ToString());
            int tutar = int.Parse(satir.Cells["Tutar"].Value.ToString());
            DateTime cikis = DateTime.Parse(satir.Cells["Cikis_Tarihi"].Value.ToString());
            TimeSpan gun = bugün - cikis;
            int gunu = gun.Days;
            int toplamtutar = gunu - ucret;

            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Delete from Sözlesme where Plaka = '" + satir.Cells["Plaka"].Value.ToString() + "'";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            komut.ExecuteNonQuery();


            string komutCumlesiUp = "update Araclar set Durumu = 'Bos' where Plaka = '" + satir.Cells["Plaka"].Value.ToString() + "'";
            MySqlCommand komutUp = new MySqlCommand(komutCumlesi, baglanti);
            komutUp.ExecuteNonQuery();

            string komutCumlesiSatis = "Insert Into Satis Values (@tc_no,@AdSoyad,@plaka,@gun,@kirasekli,@kiraücreti,@tutar,@cikistarih,@dönüstarih)";
            MySqlCommand komutSatis = new MySqlCommand(komutCumlesiSatis,baglanti);
            komutSatis.Parameters.AddWithValue("@tc_no", satir.Cells["Tc_No"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@AdSoyad", satir.Cells["Ad_Soyad"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@plaka", satir.Cells["Plaka"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@gun", gunu);
            komutSatis.Parameters.AddWithValue("@kirasekli", satir.Cells["Kira_Sekli"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@kiraücreti", ucret);
            komutSatis.Parameters.AddWithValue("@tutar", toplamtutar);
            komutSatis.Parameters.AddWithValue("@cikistarih", satir.Cells["Cikis_Tarihi"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@dönüstarih", satir.Cells["Dönüs_Tarihi"].Value.ToString());
            komutSatis.ExecuteNonQuery();

            MessageBox.Show("Araç Teslim Edildi");
        }
    }
}
