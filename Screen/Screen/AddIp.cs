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
    public partial class AddIp : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public AddIp()
        {
            InitializeComponent();
            con = new SqlConnection("server=DESKTOP-AT2VN8M; Initial Catalog=Database;Integrated Security=SSPI");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "insert into tblAdress(addr,savetime) values (@addr,@savetime)";
                SqlCommand komut = new SqlCommand(kayit, con);
                komut.Parameters.AddWithValue("@addr", textBox1.Text);
                komut.Parameters.AddWithValue("@savetime",DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Congratulations! IP Address Saved.");
                this.Hide();
            }
            catch (Exception err)
            {
                MessageBox.Show("An Error Occurred During Operation." + err.Message);
            }

        }


        

        private void AddIp_Load(object sender, EventArgs e)
        {

        }
    }
}
