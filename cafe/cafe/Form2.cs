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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-KT0V982;Initial Catalog=cafe;Integrated Security=True");
        SqlConnection connect = new SqlConnection(constring);
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text=="")
                {
                    MessageBox.Show("KULLANICI ADI BOŞ BIRAKILAMAZ");
                    textBox1.Focus();
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("MAİL ADRESİ BOŞ BIRAKILAMAZ");
                    textBox2.Focus();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("ŞİFRE BOŞ BIRAKILAMAZ");
                    textBox3.Focus();
                }
                else if (connect.State==ConnectionState.Closed)
                {
                    connect.Open();
                    string kayit = "insert into signup (kullaniciadi,mail,sifre) values(@kullaniciadi,@mail,@sifre)";
                    SqlCommand komut = new SqlCommand(kayit,connect);
                    komut.Parameters.AddWithValue("@kullaniciadi", textBox1.Text);
                    komut.Parameters.AddWithValue("@mail", textBox2.Text);
                    komut.Parameters.AddWithValue("@sifre", textBox3.Text);
                    komut.ExecuteNonQuery();
                    connect.Close();
                    MessageBox.Show("KAYIT YAPILDI");

                }

            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata Meydana Geldi" + hata.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            form1.Show();

            this.Hide();
        }
    }
}
