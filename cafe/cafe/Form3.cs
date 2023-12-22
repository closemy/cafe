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
using System.IO;

namespace cafe
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        static string connect = ("Data Source=DESKTOP-KT0V982;Initial Catalog=cafe;Integrated Security=True");
         SqlConnection veriekleconnect = new SqlConnection(connect);
        private void button1_Click(object sender, EventArgs e)
        {
            int fiyat, adet;
    fiyat = 0;

    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-KT0V982;Initial Catalog=cafe;Integrated Security=True"))
    {
        using (SqlCommand komut = new SqlCommand())
        {
            if (comboBox1.SelectedIndex==-1)
            {
                MessageBox.Show("MASAYI SEÇİNİZ");
            }
            else if (comboBox2.SelectedIndex==-1)
            {
                MessageBox.Show("ÜRÜNÜ SEÇİNİZ");
            }
            else if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("ADEDİ SEÇİNİZ");
            }
            else
            {
                con.Open();
                komut.Connection = con;
                komut.CommandText = "SELECT fiyat FROM urunler WHERE urunadi=@urunadi";
                komut.Parameters.AddWithValue("@urunadi", comboBox2.SelectedItem.ToString());
                object result = komut.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    if (int.TryParse(result.ToString(), out fiyat))
                    {
                        adet = Convert.ToInt32(comboBox3.SelectedItem);
                        listBox1.Items.Add(comboBox1.SelectedItem + "     " + comboBox2.SelectedItem + "     " + comboBox3.SelectedItem + "     FİYAT: " + (fiyat * adet).ToString());

                        if (veriekleconnect.State == ConnectionState.Closed)
                {
                    veriekleconnect.Open();
                    string kayit = "insert into odenecekler (masaadi,urunadi,adet,topfiyat) values(@masaadi,@urunadi,@adet,@topfiyat)";
                    SqlCommand verieklekomut = new SqlCommand(kayit, veriekleconnect);
                    verieklekomut.Parameters.AddWithValue("@masaadi", comboBox1.SelectedItem);
                    verieklekomut.Parameters.AddWithValue("@urunadi", comboBox2.SelectedItem);
                    verieklekomut.Parameters.AddWithValue("@adet", comboBox3.SelectedItem);
                    verieklekomut.Parameters.AddWithValue("@topfiyat", adet * fiyat);
                    verieklekomut.ExecuteNonQuery();
                    veriekleconnect.Close();
                }
                    }
                    else
                    {
                        MessageBox.Show("Fiyat değeri integer'a çevrilemiyor.");
                    }
                }
                else
                {
                    MessageBox.Show("Veri bulunamadı.");
                }
            }
        }
        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 urun_islemleri = new Form4();
            urun_islemleri.Show();
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(File.ReadAllLines("masalar.txt"));
            comboBox4.Items.AddRange(File.ReadAllLines("masalar.txt"));
            using (SqlConnection connection = new SqlConnection(connect))
            {
                string query = "SELECT urunadi FROM urunler";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader["urunadi"].ToString());
                        }
                    }
                    connection.Close();
                }
                for (int i = 1; i <=10; i++)
                {
                    comboBox3.Items.Add(i);
                }
        }
    }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
              listBox2.Items.Clear(); 

    if (comboBox4.SelectedIndex != -1)
    {
        string seciliMasaAdi = comboBox4.SelectedItem.ToString();
        using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-KT0V982;Initial Catalog=cafe;Integrated Security=True"))
        {
            con.Open();
            using (SqlCommand komut = new SqlCommand("SELECT * FROM odenecekler WHERE masaadi = @masaadi", con))
            {
                komut.Parameters.AddWithValue("@masaadi", seciliMasaAdi);

                using (SqlDataReader reader = komut.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader["masaadi"] + " - " + reader["urunadi"] + " - " + reader["adet"] + " - " + reader["topfiyat"]);
                    }
                }
            }
        }
    }
        }

       private void button3_Click(object sender, EventArgs e)
{
    if (comboBox4.SelectedIndex != -1)
    {
        string seciliMasaAdi = comboBox4.SelectedItem.ToString();

        listBox2.Items.Clear();

        if (veriekleconnect.State == ConnectionState.Closed)
        {
            veriekleconnect.Open();
        }

        using (SqlCommand komut = new SqlCommand("DELETE FROM odenecekler WHERE masaadi = @masaadi", veriekleconnect))
        {
            komut.Parameters.AddWithValue("@masaadi", seciliMasaAdi);
            komut.ExecuteNonQuery();
        }

        if (veriekleconnect.State == ConnectionState.Open)
        {
            veriekleconnect.Close();
        }

        MessageBox.Show("Masaya ait bütün veriler silindi: " + seciliMasaAdi);
        label1.Text = "";
    }
    else
    {
        MessageBox.Show("Lütfen bir masa seçiniz.");
    }
}

private void button4_Click(object sender, EventArgs e)
{
    int toplamFiyat = 0;

    if (comboBox4.SelectedIndex ==-1)
    {
        MessageBox.Show("Lütfen bir masa seçiniz.","HATA");
        return;
    }
    
    string constring = "Data Source=DESKTOP-KT0V982;Initial Catalog=cafe;Integrated Security=True";
   
    using (SqlConnection connect = new SqlConnection(constring))
    {
        try
        {
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();

                string kayit = "SELECT ISNULL(SUM(topfiyat), 0) FROM odenecekler WHERE masaadi = @masaadi";
                SqlCommand komut = new SqlCommand(kayit, connect);
                komut.Parameters.AddWithValue("@masaadi", comboBox4.SelectedItem.ToString());

                object result = komut.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    toplamFiyat = Convert.ToInt32(result);
                    label1.Text = "Toplam Fiyat (" + comboBox4.SelectedItem.ToString() + "): " + toplamFiyat + " TL";
                }
                
            }
        }
        catch (Exception hata)
        {
            MessageBox.Show("Hata Meydana Geldi: " + hata.Message);
        }
    }
}

}





}


 
