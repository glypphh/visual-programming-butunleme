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
    public partial class Satis : Form
    {
        public Satis()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Server=localhost;Port=6605;Database=otokiralama;uid=root;pwd=yusuf;";
        private void Satis_Load(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Select * From Satis";
            MySqlCommand komut = new MySqlCommand(komutCumlesi, baglanti);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
    }
}
