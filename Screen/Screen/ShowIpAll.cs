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
    public partial class ShowIpAll : Form
    {
        public ShowIpAll()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        void griddoldur()
        {
            con = new SqlConnection("server=DESKTOP-AT2VN8M; Initial Catalog=Database;Integrated Security=SSPI");
            da = new SqlDataAdapter("Select *From tblAdress", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "tblAdress");
            dataGridView1.DataSource = ds.Tables["tblAdress"];
            dataGridView1.Columns[0].Width = 182;
            dataGridView1.Columns[1].Width = 182;
            dataGridView1.Columns[2].Width = 185;
            con.Close();
        }
        private void ShowIpAll_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
