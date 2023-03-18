using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screen
{
    public partial class VerificationPassword : Form
    {
        public string mail = string.Empty;
        public string choice = string.Empty;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public VerificationPassword()
        {
            InitializeComponent();
        }

        private void VerificationPassword_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("server=DESKTOP-AT2VN8M; Initial Catalog=Database;Integrated Security=SSPI");
            textBox1.PasswordChar = '*';

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM tblUser where mail='" + mail + "' AND pwd='" + textBox1.Text + "'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Congratulations!");
                if(choice == "addIp")
                {
                    AddIp frm = new AddIp();
                    this.Hide();
                    frm.ShowDialog();
                }
                if(choice == "addUser")
                {
                    AddUser frm = new AddUser();
                    this.Hide();
                    frm.ShowDialog();
                }
                
            }
            else
            {
                MessageBox.Show("Check your password.");
            }
            con.Close();
        }
    }
}
