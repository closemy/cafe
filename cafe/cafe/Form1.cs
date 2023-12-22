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
    public partial class Form1 : Form
    {
        

        SqlConnection con;
        SqlCommand komut;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pw = textBox2.Text;
            con = new SqlConnection("Data Source=DESKTOP-KT0V982;Initial Catalog=cafe;Integrated Security=True");
            komut = new SqlCommand();
            con.Open();
            komut.Connection = con;
            komut.CommandText = "SELECT*FROM signup where kullaniciadi='" + textBox1.Text + "'And sifre='" + textBox2.Text + "'";
            dr = komut.ExecuteReader();

            if (textBox1.Text=="")
            {
                MessageBox.Show("KULLANICI ADINI BOŞ BIRAKAMAZSINIZ");
                textBox1.Focus();
            }
            else if (textBox2.Text=="")
            {
                MessageBox.Show("ŞİFRENİZİ BOŞ BIRAKAMAZSINIZ");
                textBox2.Focus();
                
            }

            else if (dr.Read())
            {
                Form3 hesaba_ekle = new Form3();
                hesaba_ekle.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("KULLANICI ADINIZ VEYA ŞİFRENİZ YANLIŞ!");
            }
            con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form2 sign = new Form2();
            this.Hide();
            sign.Show();
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
