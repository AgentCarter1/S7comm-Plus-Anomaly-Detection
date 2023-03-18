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
    public partial class AddUser : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public AddUser()
        {
            InitializeComponent();
            con = new SqlConnection("server=DESKTOP-AT2VN8M; Initial Catalog=Database;Integrated Security=SSPI");
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "insert into tblUser(name,lastname,mail,pwd,lastSeen) values (@name,@lastname,@mail,@pwd,@lastSeen)";
                SqlCommand komut = new SqlCommand(kayit, con);
                komut.Parameters.AddWithValue("@name", textBox1.Text);
                komut.Parameters.AddWithValue("@lastname", textBox2.Text);
                komut.Parameters.AddWithValue("@mail", textBox3.Text);
                komut.Parameters.AddWithValue("@pwd", textBox5.Text);
                komut.Parameters.AddWithValue("@lastSeen", "0.0.0 00:00:00");
                komut.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Congratulations! User Saved.");
                this.Hide();
            }
            catch (Exception err)
            {
                MessageBox.Show("An Error Occurred During Operation." + err.Message);
            }
        }
    }
}
