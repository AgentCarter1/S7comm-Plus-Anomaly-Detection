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
namespace Screen
{
    public partial class HomePage : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public HomePage()
        {
            InitializeComponent();

            con = new SqlConnection("server=DESKTOP-AT2VN8M; Initial Catalog=Database;Integrated Security=SSPI");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM tblUser where mail='" + textBox1.Text + "' AND pwd='" + textBox2.Text + "'";
            dr = cmd.ExecuteReader();
            
            if (dr.Read())
            {
                MessageBox.Show("Congratulations! You have successfully logged in.");
                Form1 frm = new Form1();
                frm.userMail = textBox1.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Check your username and password.");
            }
            con.Close();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
    }
}
