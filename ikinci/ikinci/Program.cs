using İkinciML.Model;
using SuperSimpleTcp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ikinci
{
    class Program
    {
        static SqlConnection con;

        static void Main(string[] args)
        {
            
            SimpleTcpClient client = new SimpleTcpClient("127.0.0.1:9000");
            
            // set events
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
                komut.Parameters.AddWithValue("@name", "Dos");
                komut.Parameters.AddWithValue("@description", "From The Second Dataset.");
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
            
            string data = Encoding.UTF8.GetString(e.Data);
            string[] words = data.Split(';');
            ModelInput sampleData = new ModelInput()
            {
                Req =Convert.ToInt32( words[0]),
                Paket = Convert.ToInt32(words[1]),
                Akan_byte = Convert.ToInt32(words[2]),
            };
            var predictionResult = ConsumeModel.Predict(sampleData);
            Console.WriteLine("Words 0"+words[0]);
            Console.WriteLine("Words 1"+words[1]);
            Console.WriteLine("Words 2"+words[2]);
            /*Console.WriteLine("Using model to make single prediction -- Comparing actual Bool with predicted Bool from sample data...\n\n");
            Console.WriteLine($"Req: {sampleData.Req}");
            Console.WriteLine($"Paket: {sampleData.Paket}");
            Console.WriteLine($"Akan_byte: {sampleData.Akan_byte}");
            Console.WriteLine($"\n\nPredicted Bool value {predictionResult.Prediction} \nPredicted Bool scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();*/
            Console.WriteLine($"\n\nPredicted Bool value {predictionResult.Prediction} \n\n");
            if (Convert.ToInt32(predictionResult.Prediction)==1)
            {
                writeDatabase();
            }
            // Console.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data)}");
        }
    }
}
