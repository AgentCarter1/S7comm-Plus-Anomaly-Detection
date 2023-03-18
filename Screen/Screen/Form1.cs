using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
namespace Screen
{
    public partial class Form1 : Form
    {
        public string userMail = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader dr;
        public static string takingData = "";
        public static int flag = 0;
        SqlConnection con;
        void setListView(ListView listView1)
        {
            listView1.Clear();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Time", 60);
            listView1.Columns.Add("Source", 130);
            listView1.Columns.Add("Destination", 130);
            listView1.Columns.Add("Protocol", 120);
            listView1.Columns.Add("Lenght", 110);
            listView1.Columns.Add("Function", 120);
        }
        public static void ClearDataGridViewRows(DataGridView dataGridView)
         {
           foreach (DataGridViewRow row in dataGridView.Rows)
            {
            if (dataGridView.Rows.Count == 1) continue;
              dataGridView.Rows.RemoveAt(dataGridView.Rows.Count -1);
            }
        }
        void griddoldur()
        {
            con = new SqlConnection("server=DESKTOP-AT2VN8M; Initial Catalog=Database;Integrated Security=SSPI");
            da = new SqlDataAdapter("Select *From tblAnomaly order by date desc", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "tblAnomaly");
            dataGridView1.DataSource = ds.Tables["tblAnomaly"];
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 132;
            dataGridView1.Columns[2].Width = 223;
            dataGridView1.Columns[3].Width = 215;
            con.Close();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            setListView(listView1);
            griddoldur();
            timer_Anomaly.Start();
            SimpleTcpClient client = new SimpleTcpClient("127.0.0.1:9000");

            // set events
            client.Events.Connected += Connected;
            client.Events.Disconnected += Disconnected;
            client.Events.DataReceived += DataReceived;

            // let's go!           
            client.Connect();
            timer_packets.Start();
            con = new SqlConnection("server=DESKTOP-AT2VN8M; Initial Catalog=Database;Integrated Security=SSPI");
            string sqlQuery = "select name, lastname, lastseen, mail FROM tblUser";
            SqlCommand command = new SqlCommand(sqlQuery, con);
            try
            {
                con.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {   
                    if (reader["mail"].ToString() == userMail)
                    {
                        label2.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["name"].ToString());
                        label4.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["lastname"].ToString());
                        label6.Text = reader["lastseen"].ToString();
                    }
                   
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        static void Connected(object sender, ConnectionEventArgs e)
        {
            //Console.WriteLine($"*** Server {e.IpPort} connected");
        }

        static void Disconnected(object sender, ConnectionEventArgs e)
        {
            //Console.WriteLine($"*** Server {e.IpPort} disconnected");
        }

        static void DataReceived(object sender, DataReceivedEventArgs e)
        {

            //MessageBox.Show((Encoding.UTF8.GetString(e.Data)).ToString());
            takingData = (Encoding.UTF8.GetString(e.Data)).ToString();

            flag = 1;
            // Console.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data)}");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ShowIpAll frm = new ShowIpAll();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VerificationPassword pswd = new VerificationPassword();
            pswd.mail = userMail;
            pswd.choice = "addIp";
            pswd.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VerificationPassword pswd = new VerificationPassword();
            pswd.mail = userMail;
            pswd.choice = "addUser";
            pswd.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void timer_Anomaly_Tick(object sender, EventArgs e)
        {
            griddoldur();
        }
        void writeDatabase()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "insert into tblAnomaly(name,description,date) values (@name,@description,@date)";
                SqlCommand komut = new SqlCommand(kayit, con);
                komut.Parameters.AddWithValue("@name", "Incorrect Ip");
                komut.Parameters.AddWithValue("@description", "From The Ip Database.");
                komut.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine("An Error Occurred During Operation." + err.Message);
            }
        }
        private void IpControl(string ip)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM tblAdress where addr='" + ip + "'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                
                
            }
            else
            {
                writeDatabase();
            }
            con.Close();
        }
        private void timer_packets_Tick(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                //listBox2.Items.Add(takingData);
                string[] splitData = takingData.Split(';');
                string sourceIp = splitData[0];
                string destionationIp = splitData[1];
                string sourceport = splitData[2];
                string dstport = splitData[3];
                string reserved = splitData[4];
                string nonce = splitData[5];
                string cong = splitData[6];
                string ecn = splitData[7];
                string urg = splitData[8];
                string ack = splitData[9];
                string push = splitData[10];
                string reset = splitData[11];
                string syn = splitData[12];
                string fin = splitData[13];
                string isolenght = splitData[14];
                string isopdutype = splitData[15];
                string versiyon = splitData[16];
                string data_lenght = splitData[17];
                string function = splitData[18];
                string sequence = splitData[19];
                string sessionid = splitData[20];
                string s7_flag = splitData[21];
                string cpu = splitData[22];
                string cpu_control = splitData[23];
                string protocol_name = splitData[24];
                string type = splitData[25];
                string len = splitData[26];
                string reeltime = splitData[27];
                string ttl = splitData[28];
                string respfark = splitData[29];
                string timefark = splitData[30];
                string sayacreq = splitData[31];
                string sayacpacket = splitData[32];
                string akanbyte = splitData[33];
                /*listView1.Columns.Add("Time", 120);
                listView1.Columns.Add("Source", 120);
                listView1.Columns.Add("Destination", 120);
                listView1.Columns.Add("Protocol", 120);
                listView1.Columns.Add("Lenght", 120);
                listView1.Columns.Add("Function", 120);*/
                string[] bilgiler = { (DateTime.Now.Second).ToString(), sourceIp,destionationIp, protocol_name, data_lenght, function };
                
                
                
               /*     listReelTime.Add(reeltime);
                    listSourceIp.Add(sourceIp) ;
                    listDestinationIp.Add(destionationIp) ;
                    listProtocolName.Add(protocol_name) ;
                    listDataLenght.Add(data_lenght) ;
                    listFunction.Add(function) ;
                    allPacketsX++;
                for(int i = listFunction.Count-1; i > 0; i--)
                {
                    string[] bilgiler = { listReelTime[i], listSourceIp[i], listDestinationIp[i], listProtocolName[i], listDataLenght[i], listFunction[i] };
                    var satir = new ListViewItem(bilgiler);
                    listView1.Items.Add(satir);
                }*/
               // string[] bilgiler = { listReelTime[i], listSourceIp[i], listDestinationIp[i], listProtocolName[i], listDataLenght[i], listFunction[i] };
                var satir = new ListViewItem(bilgiler);
                listView1.Items.Add(satir);
                /*var satir = new ListViewItem(bilgiler);
                listView1.Items.Add(satir);*/
                flag = 0;
                takingData = "";
            }
        }
    }
}
