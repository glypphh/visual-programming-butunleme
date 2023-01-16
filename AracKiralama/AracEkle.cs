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
    public partial class AracEkle : Form
    {
        public AracEkle()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Server=localhost;Port=6605;Database=otokiralama;uid=root;pwd=yusuf;";
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            if (comboBox1.SelectedIndex == 0)
            {
                comboBox2.Items.Add("Corsa");
                comboBox2.Items.Add("Astra");
                comboBox2.Items.Add("İnsignia");

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                comboBox2.Items.Add("A-200");
                comboBox2.Items.Add("C63");
                comboBox2.Items.Add("S-400");
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                comboBox2.Items.Add("M 520");
                comboBox2.Items.Add("M4 competition");
                comboBox2.Items.Add("M 63");
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                comboBox2.Items.Add("Megane 6");
                comboBox2.Items.Add("Clıo");
                comboBox2.Items.Add("Fluence");
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                comboBox2.Items.Add("courier");
                comboBox2.Items.Add("GT-500");
                comboBox2.Items.Add("Mustang");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Insert Into Araclar Values (@plaka,@Marka,@Seri,@Model,@Renk,@Km,@Yakit,@Ücret,@Durumu)";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@plaka", textBox1.Text);
            komut.Parameters.AddWithValue("@Marka", comboBox1.SelectedItem);
            komut.Parameters.AddWithValue("@Seri", comboBox2.SelectedItem);
            komut.Parameters.AddWithValue("@Model", textBox2.Text);
            komut.Parameters.AddWithValue("@Renk", textBox3.Text);
            komut.Parameters.AddWithValue("@Km", textBox4.Text);
            komut.Parameters.AddWithValue("@Yakit", comboBox3.SelectedItem);
            komut.Parameters.AddWithValue("@Ücret", textBox5.Text);
            komut.Parameters.AddWithValue("@Durumu", cbxDurum.SelectedItem);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Başarılı");
        }
    }
}
