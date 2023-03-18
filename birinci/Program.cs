using BirinciML.Model;
using SuperSimpleTcp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace birinci
{
    class Program
    {
        static SqlConnection con;
        static void Main(string[] args)
        {
            SimpleTcpClient client = new SimpleTcpClient("127.0.0.1:9000");
            client.Events.Connected += Connected;
            client.Events.Disconnected += Disconnected;
            client.Events.DataReceived += DataReceived;

            // let's go!           
            client.Connect();
            Console.ReadKey();
        }
        static void Connected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"*** Server {e.IpPort} connected");
        }

        static void Disconnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"*** Server {e.IpPort} disconnected");
        }
        static void writeDatabase()
        {
            con = new SqlConnection("server=DESKTOP-AT2VN8M; Initial Catalog=Database;Integrated Security=SSPI");
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "insert into tblAnomaly(name,description,date) values (@name,@description,@date)";
                SqlCommand komut = new SqlCommand(kayit, con);
                komut.Parameters.AddWithValue("@name", "Fake Packet");
                komut.Parameters.AddWithValue("@description", "From The First Dataset.");
                komut.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine("An Error Occurred During Operation." + err.Message);
            }
        }
        static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            

            // Make a single prediction on the sample data and print results

            string takingData = Encoding.UTF8.GetString(e.Data);
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
            ModelInput sampleData = new ModelInput()
            {
                Destination_Ip = destionationIp,
                Flags_Ack = Convert.ToInt32(ack),
                Flags_Congestion = Convert.ToInt32(cong),
                Flags_Ecn = Convert.ToInt32(ecn),
                Flags_Fin = Convert.ToInt32(fin),
                Flags_Nonce = Convert.ToInt32(nonce),
                Flags_Push = Convert.ToInt32(push),
                Flags_Reset = Convert.ToInt32(reset),
                Flags_Reversed = Convert.ToInt32(reserved),
                Flags_Syn = Convert.ToInt32(syn),
                Flags_Urg = Convert.ToInt32(urg),
                Iso_Datatype = isopdutype,
                Iso_Length = Convert.ToInt32(isolenght),
                S7_Data_Length = Convert.ToInt32(data_lenght),
                S7_Flag = Convert.ToInt32(s7_flag),
                
                S7_Function = function,
                S7_Sequence = sequence,
                S7_Session = sessionid,
                S7_Version = versiyon,
                Source_Ip = sourceIp,
            };
            var predictionResult = ConsumeModel.Predict(sampleData);
            Console.WriteLine($"Predicted Label value {predictionResult.Prediction}\n\n");
            if (Convert.ToInt32(predictionResult.Prediction) == 1)
            {
                writeDatabase();
            }
        }
    }
}
