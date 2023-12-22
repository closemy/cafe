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

namespace cafe
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        static string constring = ("Data Source=DESKTOP-KT0V982;Initial Catalog=cafe;Integrated Security=True");
        SqlConnection connect = new SqlConnection(constring);

        private void button1_Click(object sender, EventArgs e)
        {
                 try
            {
                if (textBox1.Text=="")
                {
                    MessageBox.Show("ÜRÜN ADI BOŞ BIRAKILAMAZ");
                    textBox1.Focus();
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("FİYAT ALANI BOŞ BIRAKILAMAZ");
                    textBox2.Focus();
                }
                else if (connect.State==ConnectionState.Closed)
                {
                    connect.Open();
                    string kayit = "insert into urunler (urunadi,fiyat) values(@urunadi,@fiyat)";
                    SqlCommand komut = new SqlCommand(kayit,connect);
                    komut.Parameters.AddWithValue("@urunadi", textBox1.Text);
                    komut.Parameters.AddWithValue("@fiyat", textBox2.Text);
                    komut.ExecuteNonQuery();
                    connect.Close();
                }

            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata Meydana Geldi" + hata.Message);
            }
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(constring))
            {
                string query = "SELECT * FROM urunler";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == "")
                {
                    MessageBox.Show("ID BOŞ BIRAKILAMAZ");
                    textBox4.Focus();
                }
                else if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                    string kayit = "delete from urunler where id='" + textBox4.Text + "'";
                    SqlCommand komut = new SqlCommand(kayit, connect);
                    komut.ExecuteNonQuery();
                    connect.Close();
                }

            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata Meydana Geldi" + hata.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == "")
                {
                    MessageBox.Show("ID BOŞ BIRAKILAMAZ");
                    textBox5.Focus();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("FİYAT ALANI BOŞ BIRAKILAMAZ");
                    textBox3.Focus();
                }
                else if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                    string kayit = "update urunler set fiyat='"+textBox3.Text+"' where id='"+textBox5.Text+"'";
                    SqlCommand komut = new SqlCommand(kayit, connect);
                    komut.Parameters.AddWithValue("@id", textBox5.Text);
                    komut.Parameters.AddWithValue("@fiyat", textBox3.Text);
                    komut.ExecuteNonQuery();
                    connect.Close();
                }

            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata Meydana Geldi" + hata.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == "")
                {
                    MessageBox.Show("ID BOŞ BIRAKILAMAZ");
                    textBox6.Focus();
                }
                else if (textBox7.Text == "")
                {
                    MessageBox.Show("ÜRÜN ADI BOŞ BIRAKILAMAZ");
                    textBox7.Focus();
                }
                else if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                    string kayit = "update urunler set urunadi='" + textBox7.Text + "' where id='" + textBox6.Text + "'";
                    SqlCommand komut = new SqlCommand(kayit, connect);
                    komut.Parameters.AddWithValue("@id", textBox6.Text);
                    komut.Parameters.AddWithValue("@urunadi", textBox7.Text);
                    komut.ExecuteNonQuery();
                    connect.Close();
                }

            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata Meydana Geldi" + hata.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }
    }
}
