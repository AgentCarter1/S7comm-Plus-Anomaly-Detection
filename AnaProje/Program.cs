using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using Grpc.Core;
using PacketDotNet;
using SharpPcap;
using SuperSimpleTcp;

namespace AnaProje
{
    class Program
    {
        
        private static string currentvolume, curentlevel = "";
        private static string sakla = " ";
        private static int beklet = 0;
        private static string sakla1 = " ";
        private static string sakla2 = " ";
        private static string sakla3 = " ";
        private static decimal reeltime = 0;
        private static decimal respfark = 0;
        private static List<string> sequ1 = new List<string>();
        private static List<double> time1 = new List<double>();
        private static decimal[] tuttu = new decimal[1000000];
        private static decimal[] resp = new decimal[1000000];
        private static int s7comm_sayac = 0;
        private static int s7commplus_sayac = 0;
        private static int a = 1;
        private static int b = 1;
        private static string cong = "";
        private static string ecn = "";
        private static string urg = "";
        private static string ack = "";
        private static string push = "";
        private static string reset = "";
        private static string syn = "";
        private static string fin = "";
        private static string  versiyon= "";
        private static string data_length = "";
        private static string opcode = "";
        private static string function = "";
        private static string seq = "";
        private static int ttl =0 ;
        private static string session_id = "";
        private static string s7_flag = "";
        private static string cpu = " ";
        private static string seq_sakla = "";
        private static string cpu_control="";
        private static char virgül = ',';
        private static string ana_kisim, ilk_kisim, ikinci_kisim ,ucuncu_kisim= "";
        private static float sayacreq = 0;
        private static float sayacpaket = 0;
        private static float akanbyte = 0;
        private static bool birinci, ikinci = false;
        static void dectobinary(string deger)
        {
            string bütünbit = "";
            int number = int.Parse(deger);
            int i;
            int[] numberArray = new int[10];
            for (i = 0; number > 0; i++)
            {
                numberArray[i] = number % 2;
                number = number / 2;
            }
            int a = i;
            if (a != 8)
            {
                for (int j = 8; j > i; j--)
                {
                    //Console.Write("0");
                    bütünbit += "" + "0";
                }
            }

            for (i = i - 1; i >= 0; i--)
            {
                //Console.Write(numberArray[i]);
                bütünbit += "" + numberArray[i];
            }
            Console.WriteLine(bütünbit);
            Console.Write(".... ..." + bütünbit.Substring(7, 1)); if (bütünbit.Substring(7, 1) == "0") { Console.Write(": False"); } else Console.Write("=Bit0: True"); Console.WriteLine();
            Console.Write(".... .." + bütünbit.Substring(6, 1) + "."); if (bütünbit.Substring(6, 1) == "0") { Console.Write(": False"); } else Console.Write("=Bit1-SometimesSet?: True"); Console.WriteLine();
            Console.Write(".... ." + bütünbit.Substring(5, 1) + ".."); if (bütünbit.Substring(5, 1) == "0") { Console.Write(": False"); } else Console.Write("=Bit2-AlwaysSet: True"); Console.WriteLine();
            Console.Write(".... " + bütünbit.Substring(4, 1) + "..."); if (bütünbit.Substring(4, 1) == "0") { Console.Write(": False"); } else Console.Write("=Bit3: True"); Console.WriteLine();
            Console.Write("..." + bütünbit.Substring(3, 1) + " ...."); if (bütünbit.Substring(3, 1) == "0") { Console.Write(": False"); } else Console.Write("=Bit4-AlwaysSet: True"); Console.WriteLine();
            Console.Write(".." + bütünbit.Substring(2, 1) + ". ...."); if (bütünbit.Substring(2, 1) == "0") { Console.Write(": False"); } else Console.Write("=Bit5-AlwaysSet: True"); Console.WriteLine();
            Console.Write("." + bütünbit.Substring(1, 1) + ".. ...."); if (bütünbit.Substring(1, 1) == "0") { Console.Write(": False"); } else Console.Write("=Bit6-NoResponseExpected: True"); Console.WriteLine();
            Console.Write("" + bütünbit.Substring(0, 1) + "... ...."); if (bütünbit.Substring(0, 1) == "0") { Console.Write(": False"); } else Console.Write("=Bit7: True"); Console.WriteLine();
        }
        public static string cevir(byte deger)
        {
            
            int decn, q, dn = 0, m, l;
            int tmp;
            int s;

            decn = Convert.ToInt32(deger);
            q = decn;
            for (l = q; l > 0; l = l / 16)
            {
                tmp = l % 16;
                if (tmp < 10)
                    tmp = tmp + 48;
                else
                    tmp = tmp + 55;
                dn = dn * 100 + tmp;
            }
            //Console.Write(" ");
            //sakla1 += " ";
            for (m = dn; m > 0; m = m / 100)
            {
                s = m % 100;
                // Console.Write("{0}", (char)s);
                sakla1 = sakla1 + ((char)s).ToString();
            }

            return sakla1;
        }
        public static string bytetut(byte deger)
        {
            int decn, q, dn = 0, m, l;
            int tmp;
            int s;

            decn = Convert.ToInt32(deger);
            q = decn;
            for (l = q; l > 0; l = l / 16)
            {
                tmp = l % 16;
                if (tmp < 10)
                    tmp = tmp + 48;
                else
                    tmp = tmp + 55;
                dn = dn * 100 + tmp;
            }
            Console.Write(" ");
            for (m = dn; m > 0; m = m / 100)
            {
                s = m % 100;



                //Console.Write("{0}", (char)s);
                //string sakla = " ";
                sakla = "0x" + sakla + ((char)s).ToString() + "000";

            }
            return sakla;
        }
        public static void flag(string deger)
        {
            string bütünbit = "";

            int number = int.Parse(deger);
            int i;
            int[] numberArray = new int[10];
            for (i = 0; number > 0; i++)
            {
                numberArray[i] = number % 2;
                number = number / 2;
            }
            int a = i;
            if (a != 8)
            {
                for (int j = 8; j > i; j--)
                {
                    //Console.Write("0");
                    bütünbit += "" + "0";
                }
            }

            for (i = i - 1; i >= 0; i--)
            {
                //Console.Write(numberArray[i]);
                bütünbit += "" + numberArray[i];
            }
            Console.WriteLine(bütünbit);
            Console.WriteLine("Reserved:Not Set");
            Console.WriteLine("Nonce:Not Set");
            Console.Write(bütünbit.Substring(0, 1) + "Congestion Window Reduced (CWR):"); if (bütünbit.Substring(0, 1) == "0") { Console.Write(": Not set"); cong = "0"; } else { Console.Write("Set"); cong = "1"; } Console.WriteLine();
            Console.Write(bütünbit.Substring(1, 1) + "ECN-Echo:"); if (bütünbit.Substring(1, 1) == "0") { Console.Write("Not set"); ecn = "0"; } else {Console.Write("Set"); ecn = "1"; } Console.WriteLine();
            Console.Write(bütünbit.Substring(2, 1) + "Urgent:"); if (bütünbit.Substring(2, 1) == "0") { Console.Write("Not set"); urg = "0"; } else { Console.Write("Set"); urg = "1"; } Console.WriteLine();
            Console.Write(bütünbit.Substring(3, 1) + "Acknowledgment:"); if (bütünbit.Substring(3, 1) == "0") { Console.Write("Not set"); ack = "0"; } else { Console.Write("Set"); ack = "1"; } Console.WriteLine();
            Console.Write(bütünbit.Substring(4, 1) + "Push:"); if (bütünbit.Substring(4, 1) == "0") { Console.Write("Not set"); push = "0"; } else {Console.Write("Set"); push = "1"; }Console.WriteLine();
            Console.Write(bütünbit.Substring(5, 1) + "Reset:"); if (bütünbit.Substring(5, 1) == "0") { Console.Write("Not set"); reset = "0"; } else {Console.Write("Set"); reset = "1"; }Console.WriteLine();
            Console.Write(bütünbit.Substring(6, 1) + "Syn:"); if (bütünbit.Substring(6, 1) == "0") { Console.Write("Not set"); syn = "0"; } else { Console.Write("Set"); syn = "1"; } Console.WriteLine();
            Console.Write(bütünbit.Substring(7, 1) + "Fin:"); if (bütünbit.Substring(7, 1) == "0") { Console.Write("Not set"); fin = "0"; } else { Console.Write("Set"); fin = "1"; } Console.WriteLine();
        }
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            int flag = 0;
            string kayit4 = sayacreq + ";" + sayacpaket + ";" + akanbyte;
            ucuncu_kisim = kayit4;
            
            if (device != 0)
            {
                if (flag == 0)
                {
                    server.Send(deviceIpAddr1, kayit4);
                    sayacreq = sayacpaket = akanbyte =   0;
                    flag = 1;
                }
                sayacreq = sayacpaket = akanbyte = 0;

            }

            Console.ReadKey();

        }
        private static void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
          
            respfark = 0;
            reeltime = 0;
            var time = e.Packet.Timeval.Date;
            decimal second = (decimal)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6));
            //tuttu[a] = e.Packet.Timeval.MicroSeconds;

            double timefark = 0;
            double timetoplam = 0;
            string protocol_name = "";
            string type = "";
            var len = e.Packet.Data.Length;
            string dosya10 = @"C:\Users\karis\Desktop\Normal.csv";
            string dosya11 = @"C:\Users\karis\Desktop\dataset3.csv";
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

            var tcpPacket = (PacketDotNet.TcpPacket)packet.Extract(typeof(PacketDotNet.TcpPacket));
            if (tcpPacket != null)
            {
                var ipPacket = (PacketDotNet.IpPacket)tcpPacket.ParentPacket;

                System.Net.IPAddress srcIp = ipPacket.SourceAddress;
                System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
                int srcPort = tcpPacket.SourcePort;
                int dstPort = tcpPacket.DestinationPort;
                int tcplen = tcpPacket.Bytes.Length;
                uint tcpseq = tcpPacket.SequenceNumber;
                
                byte[] elimde = packet.Bytes;
                byte[] tcpby = tcpPacket.Bytes;
                byte[] tcpheader = tcpPacket.Header;
                // byte[] comm = tcpPacket.PayloadData;
                
                Console.WriteLine("{0}:{1}:{2}:{3} {4} -> {5} , Len={6} ",
                   time.Hour, time.Minute, time.Second, time.Millisecond,
                   srcIp, dstIp, len);
                Console.WriteLine("IP Source Adres.............:" + srcIp);
                Console.WriteLine("IP Destination Adres........:" + dstIp);
                Console.WriteLine("TCP Source Port.............:" + srcPort);
                Console.WriteLine("TCP Destination Port........:" + dstPort);
                Console.WriteLine("TCP Seq(raw)................:" + tcpseq);
                cong = ecn = urg = ack = push = reset = syn = fin = null;
                flag((tcpheader[13]).ToString());
                //flag("18");
                string dosya = @"C:\Users\karis\Desktop\malbaris.csv";
                string bolen = ",";
               
                    Console.WriteLine(); sakla2 = null; sakla = null;

                byte[] comm = tcpPacket.PayloadData;

                if (tcpPacket.PayloadData.Length > 0)
                {
                    if (comm[0] != 0)
                    {
                        int beklet = Convert.ToInt32(comm[0]);
                        if (beklet == 3&&comm.Length>3)
                        {
                            
                            //cotp
                            
                            string iso1len = cevir(comm[4]);
                            sakla1 = "";
                            string isopdutype = cevir(comm[5]);
                            sakla1 = "";

                            

                            if (tcpPacket.PayloadData.Length > 7)
                            {
                                string beklet5 = cevir(comm[7]);
                                if(comm[7]==50||comm[7]==114)
                                {
                                    sayacpaket++;
                                    akanbyte = akanbyte + len;
                                    tuttu[a] = second;
                                    a++;
                                   
                                    ttl = elimde[22];

                                    sakla1 = "";
                                //int beklet = null;
                                beklet = Convert.ToInt32(beklet5);
                                    if (beklet == 32)
                                    {
                                        reeltime = (decimal)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) - tuttu[a - 2];
                                        if (reeltime == 0)
                                        {
                                            reeltime = (decimal)Math.Pow(10, -6);
                                        }
                                        Console.WriteLine("protokol s7comm--------------------------------------------------------------");
                                        s7comm_sayac++;
                                        protocol_name = "s7comm";
                         
                                        type = comm[8].ToString();


                                        string dosya2 = @"C:\Users\karis\Desktop\dataset6.csv";

                                        string kayit = srcIp + ";" + dstIp + ";" + protocol_name + ";" + type + ";" + len + ";" + reeltime + ";" + ttl + ";" + respfark + ";" + "0" + ";" + "0" + '\n';
                                       
                                        File.AppendAllText(dosya2, kayit);
                                        ikinci_kisim = protocol_name + ";" + type + ";" + len + ";" + reeltime + ";" + ttl + ";" + respfark + ";" + "0" + ";";
                                    }
                                    else if (beklet == 72)
                                    {
                                        protocol_name = "s7comm-plus";
                                        Console.WriteLine("protocol s7comm-plus***********************************************************");
                                        Console.WriteLine("HEADER");
                                        Console.WriteLine("Protocol ID.................:0x" + beklet);
                                        string beklet1 = cevir(comm[8]);
                                        sakla1 = "";
                                        //int saklatut = Convert.ToInt32(beklet1);

                                        if (beklet1 == "FE")
                                        {
                                            Console.WriteLine("Protocol Versiyon...........:System Event");
                                            versiyon = beklet1;
                                            Console.WriteLine("Protocol Data Length........:" + comm[10]);
                                            data_length = comm[10].ToString();
                                            //var csv2 = new StringBuilder();
                                            //var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}", srcIp, dstIp,
                                               // srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length);
                                            ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" + versiyon + ";" + data_length + ";" + "0" + ";" + "0" + ";"
                                                + "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";";
                                            //csv2.AppendLine(newLine2);
                                            //File.AppendAllText(dosya, csv2.ToString());
                                            string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + "" + ";" + "" + ";" + "" + ";" + "" + ";" + ""+";"+"" + ";" + " " +";"+"0"+ '\n';
                                            File.AppendAllText(dosya10, veri);

                                        }
                                        else if (Convert.ToInt32(beklet1) == 1)
                                        {
                                            Console.WriteLine("Protocol Versiyon...........:V" + beklet1);
                                            versiyon = beklet1;
                                            Console.WriteLine("Protocol Data Length........:" + comm[10]);
                                            data_length = comm[10].ToString();
                                            Console.WriteLine("DATA");
                                            if (comm[11] == 49)//31
                                            {
                                                sayacreq++;
                                                type = cevir(comm[11]);
                                                Console.WriteLine("Opcode:Request (0x" + cevir(comm[11]) + ")");
                                                sakla1 = null;
                                                opcode = cevir(comm[11]);
                                                sakla1 = null;
                                                string tut = "";
                                                for (int i = 14; i < 16; i++)
                                                {
                                                    tut = "" + cevir(comm[i]);
                                                }
                                                sakla1 = null;
                                                Console.WriteLine("Function: CreateObject (0x" + tut + ")"); function = tut;
                                                string seq_1 = "";
                                                for (int j = 18; j < 20; j++)
                                                {
                                                    seq_1 = "" + cevir(comm[j]);
                                                }
                                                sakla1 = null;
                                                double timeistek = (double)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6));

                                                time1.Add(timeistek);


                                                sequ1.Add(seq_1);
                                                tut = null;
                                               
                                                for (int i = 20; i < 24; i++)
                                                {
                                                    tut = "" + cevir(comm[i]);
                                                }
                                                sakla1 = null;
                                                Console.WriteLine("Session ID: 0X" + tut);
                                                session_id = tut;
                                                tut = null;
                                                Console.WriteLine("----Transport flags:0x" + cevir(comm[24]));
                                                sakla1 = null;
                                                //dectobinary(cevir(comm[24])); 
                                                s7_flag = cevir(comm[24]);
                                                sakla1 = null;
                                                //v1 request set
                                               // var csv2 = new StringBuilder();
                                                //var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", srcIp, dstIp,
                                                   // srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length, opcode, function, seq_1, session_id, s7_flag, " ", " ,");
                                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                                    versiyon + ";" + data_length + ";" + function + ";" + seq_1 + ";" + session_id + ";" + s7_flag + ";" + "0 " + ";" + "0" + ";";


                                               // csv2.AppendLine(newLine2);
                                               // File.AppendAllText(dosya, csv2.ToString());
                                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + opcode + ";" + function + ";" + seq_1 + ";" + session_id + ";" + s7_flag + ";" + "" + ";" + " "+ ";" + "0" + '\n';
                                                File.AppendAllText(dosya10, veri);


                                            }
                                            else if (comm[11] == 50)//32
                                            {
                                                type = cevir(comm[11]);
                                                resp[b] = (decimal)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6));

                                                b++;
                                                Console.WriteLine("Opcode:Response (0x" + cevir(comm[11]) + ")");
                                                sakla1 = null;
                                                opcode = cevir(comm[11]);
                                                sakla1 = null;
                                                string tut4 = "";
                                                for (int i = 14; i < 16; i++)
                                                {
                                                    tut4 = "" + cevir(comm[i]);
                                                }
                                                sakla1 = null;
                                                Console.WriteLine("Function: CreateObject (0x" + tut4 + ")"); function = tut4;
                                              
                                                tut4 = null;
                                                string seq_2 = "";
                                                for (int j = 18; j < 20; j++)
                                                {
                                                    seq_2 = "" + cevir(comm[j]);
                                                }
                                                sakla1 = null;


                                                for (int i = 0; i < sequ1.Count; i++)
                                                {
                                                    if (sequ1[i] == seq_2)
                                                    {
                                                        timefark = (e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) - time1[i];
                                                        timetoplam = (double)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) + time1[i];
                                                        Console.WriteLine(timefark);
                                                    }
                                                }
                                                respfark = (decimal)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) - resp[b - 2];
                                                if (respfark == 0)
                                                {
                                                    respfark = (decimal)Math.Pow(10, -7);
                                                }
                                                Console.WriteLine("----Transport flags:0x" + cevir(comm[24]));
                                                sakla1 = null;
                                                //dectobinary(cevir(comm[24]));
                                                s7_flag = cevir(comm[24]);
                                                sakla1 = null;
                                                //v1 respons set
                   

                                              //  var csv2 = new StringBuilder();
                                              //  var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", srcIp, dstIp,
                                                   // srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length, opcode, function, seq, " ", s7_flag, " ", " ");
                                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + function + ";" + seq_2 + ";" + "0" + ";" + s7_flag + ";" + "0 " + ";" + "0";

                                              //  csv2.AppendLine(newLine2);
                                             //   File.AppendAllText(dosya, csv2.ToString());
                                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + opcode + ";" + function + ";" + seq_2 + ";" + "" + ";" + s7_flag + ";" + "" + ";" + " " + ";" + "0" + '\n';
                                                File.AppendAllText(dosya10, veri);

                                            }
                                            else if (comm[11] == 51)//33
                                            {
                                                type = cevir(comm[11]);
                                                Console.WriteLine("Opcode:Natification (0x" + cevir(comm[11]) + ")");
                                                sakla1 = null;
                                                //v1 notification
                                                opcode = cevir(comm[11]);
                                                sakla1 = null;
                                              //  var csv2 = new StringBuilder();
                                               // var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", srcIp, dstIp,
                                                  //  srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length, opcode, " ", " ", " ", " ", " ", " ,");
                                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0 " + ";" + "0" + ";";

                                               // csv2.AppendLine(newLine2);
                                               // File.AppendAllText(dosya, csv2.ToString());
                                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + opcode + ";" + function + ";" + "" + ";" + "" + ";" + s7_flag + ";" + "" + ";" + " " + ";" + "0" + '\n';
                                                File.AppendAllText(dosya10, veri);
                                                for(int x=12;x<comm.Length-5;x++)
                                                {
                                                    for(int y=x;y<x+2;y++)
                                                    {
                                                        sakla2 = sakla2 + cevir(comm[y]);
                                                    }
                                                    sakla1 = null;
                                                    if(sakla2=="E46")
                                                    {
                                                        birinci = true; 
                                                        for(int u=x+1;u<x+5;u++)
                                                        {
                                                            sakla2 = "" + cevir(comm[u]);
                                                        }
                                                        currentvolume = kayan(sakla2);
                                                        sakla1 = null;
                                                    }
                                                    else if(sakla2=="E40")
                                                    {
                                                        ikinci = true;
                                                        for (int u = x + 1; u < x + 5; u++)
                                                        {
                                                            sakla2 = "" + cevir(comm[u]);
                                                        }
                                                        curentlevel = kayan(sakla2);
                                                        sakla1 =null;
                                                    }
                                                }
                                                if(srcIp.ToString()=="192.168.41.10"&& dstIp.ToString()=="192.168.41.30")
                                                {
                                                    string veri20 = "";
                                                    if (birinci == true && ikinci == true)
                                                    {
                                                        veri20 = currentvolume + ";" + curentlevel + ";" + "1" + '\n';
                                                        File.AppendAllText(dosya11, veri20);
                                                    }
                                                    birinci = ikinci = false;
                                                }
                                                
                                            }


                                            string dosya1 = @"C:\Users\karis\Desktop\dataset6.csv";

                                            string kayit1 =srcIp + ";" + dstIp + ";" + protocol_name + ";" + type + ";" + len + ";" + reeltime + ";" + ttl + ";" + respfark + ";" + timefark + ";" + "0" + '\n';
                                            ikinci_kisim = protocol_name + ";" + type + ";" + len + ";" + reeltime + ";" + ttl + ";" + respfark + ";" + timefark + ";";
                                            File.AppendAllText(dosya1, kayit1);

                                        }
                                        else if (Convert.ToInt32(beklet1) == 2)
                                        {
                                            Console.WriteLine("Protocol Versiyon...........:V" + beklet1); versiyon = beklet1;
                                            Console.WriteLine("Protocol Data Length........:" + comm[10]); data_length = comm[10].ToString();
                                            Console.WriteLine("DATA");
                                            if (comm[11] == 49)//31
                                            {
                                                sayacreq++;
                                                type = cevir(comm[11]);
                                                Console.WriteLine("Opcode:Request (0x" + cevir(comm[11]) + ")");
                                                sakla1 = null;
                                                opcode = cevir(comm[11]);
                                                sakla1 = null;
                                                string tut6 = "";
                                                for (int i = 14; i < 16; i++)
                                                {
                                                    tut6 = "" + cevir(comm[i]);
                                                }
                                                sakla1 = null;
                                                Console.WriteLine("Function: CreateObject (0x" + tut6 + ")"); function = tut6;
                                               
                                                tut6 = null;
                                                double timeistek = (double)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6));
                                           
                                                time1.Add(timeistek);
                                                string seq_3 = "";
                                                for (int j = 18; j < 20; j++)
                                                {
                                                    seq_3 = "" + cevir(comm[j]);
                                                }
                                                sakla1 = null;



                                                sequ1.Add(seq_3);
                                                for (int i = 20; i < 24; i++)
                                                {
                                                    tut6 = "" + cevir(comm[i]);
                                                }
                                                sakla1 = null;
                                                session_id = tut6;
                                                Console.WriteLine("Session ID: 0X" + tut6);
                                                tut6 = null;
                                                Console.WriteLine("----Transport flags:0x" + cevir(comm[24]));
                                                sakla1 = null;
                                                //dectobinary(cevir(comm[24]));
                                                s7_flag = cevir(comm[24]);
                                                sakla1 = null;
                                                //v2 request set
                                              //  var csv2 = new StringBuilder();
                                              //  var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", srcIp, dstIp,
                                                //    srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length, opcode, function, seq_3, session_id, s7_flag, " ", " ,");
                                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + function + ";" + seq_3 + ";" + session_id + ";" + s7_flag + ";" + "0 " + ";" + "0" + ";";

                                                //csv2.AppendLine(newLine2);
                                               // File.AppendAllText(dosya, csv2.ToString());
                                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + opcode + ";" + function + ";" + seq_3 + ";" + session_id+";"+s7_flag + ";" + "" + ";" + " " + ";" + "0" + '\n';
                                                File.AppendAllText(dosya10, veri);

                                            }
                                            else if (comm[11] == 50)//32
                                            {
                                                type = cevir(comm[11]);
                                                resp[b] = (decimal)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6));

                                                b++;
                                                Console.WriteLine("Opcode:Response (0x" + cevir(comm[11]) + ")");
                                                sakla1 = null;
                                                opcode = cevir(comm[11]);
                                                sakla1 = null;
                                                string tut7 = "";
                                                for (int i = 14; i < 16; i++)
                                                {
                                                    tut7 = "" + cevir(comm[i]);
                                                }
                                                sakla1 = null;
                                                Console.WriteLine("Function: CreateObject (0x" + tut7 + ")"); function = tut7;
                            
                                                tut7 = null;
                                                string seq_4 = "";
                                                for (int j = 18; j < 20; j++)
                                                {
                                                    seq_4 = "" + cevir(comm[j]);
                                                }
                                                sakla1 = null;


                                                for (int i = 0; i < sequ1.Count; i++)
                                                {
                                                    if (sequ1[i] == seq_4)
                                                    {
                                                        timefark = (e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) - time1[i];
                                                        timetoplam = (double)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) + time1[i];
                                                        Console.WriteLine(timefark);
                                                    }
                                                }
                                                respfark = (decimal)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) - resp[b - 2];
                                                if (respfark == 0)
                                                {
                                                    respfark = (decimal)Math.Pow(10, -7);
                                                }
                                                Console.WriteLine("----Transport flags:0x" + cevir(comm[24]));
                                                sakla1 = null;
                                                // dectobinary(cevir(comm[24])); 
                                                s7_flag = cevir(comm[24]);
                                                sakla1 = null;
                                                //v2 respons set
                                               // var csv2 = new StringBuilder();
                                               // var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", srcIp, dstIp,
                                                 //   srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length, opcode, function, seq_4, " ", s7_flag, " ", " ,");

                                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + function + ";" + seq_4 + ";" + "0" + ";" + s7_flag + ";" + "0 " + ";" + "0" + ";";
                                                //csv2.AppendLine(newLine2);
                                                //File.AppendAllText(dosya, csv2.ToString());
                                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + opcode + ";" + function + ";" + seq_4 + ";" + "" + ";" + s7_flag+ ";"+" "+";" + "" + ";" + "0" + '\n';
                                                File.AppendAllText(dosya10, veri);

                                            }
                                            else if (comm[11] == 51)//33
                                            {
                                                type = cevir(comm[11]);
                                                Console.WriteLine("Opcode:Natification (0x" + cevir(comm[11]) + ")");
                                                sakla1 = null;
                                                opcode = cevir(comm[11]);
                                                sakla1 = null;
                                                //v2 notification
                                                for (int x = 12; x < comm.Length -5; x++)
                                                {
                                                    sakla2 = null;
                                                    for (int y = x; y < x + 2; y++)
                                                    {
                                                        sakla2 = "" + cevir(comm[y]);
                                                    }
                                                    sakla1 = null;
                                                    if (sakla2 == "E46")
                                                    {
                                                        birinci = true; 
                                                        for (int u = x + 1; u < x + 5; u++)
                                                        {
                                                            sakla2 = "" + cevir(comm[u]);
                                                        }
                                                        currentvolume = kayan(sakla2);
                                                        sakla1 = null;
                                                    }
                                                    else if (sakla2 == "E40")
                                                    {
                                                        ikinci = true; 
                                                        for (int u = x + 1; u < x + 5; u++)
                                                        {
                                                            sakla2 = "" + cevir(comm[u]);
                                                        }
                                                        curentlevel = kayan(sakla2);
                                                        sakla1 = null;
                                                      
                                                    }
                                                }
                                                if (srcIp.ToString() == "192.168.41.10" && dstIp.ToString() == "192.168.41.30")
                                                {
                                                    //!!!!!!BU DEĞİŞKENE DEĞER ATAMADAN DOSYAYA YAZDIRMAYA ÇALIŞYORSUN
                                                    if (birinci == true && ikinci == true)
                                                    {
                                                        string veri20 = "";
                                                        veri20 = currentvolume + ";" + curentlevel + ";" + "1" + '\n';
                                                        File.AppendAllText(dosya11, veri20);
                                                    }
                                                    birinci = ikinci = false;
                                                }
                                                // var csv2 = new StringBuilder();
                                                // var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", srcIp, dstIp,
                                                //   srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length, opcode, " ", " ", " ", " ", " ", " ,");
                                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                           versiyon + ";" + data_length + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0 " + ";" + "0" + ";";

                                             //   csv2.AppendLine(newLine2);
                                              //  File.AppendAllText(dosya, csv2.ToString());
                                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" +opcode+ ";" + "" + ";" + "" + ";" + "" + ";" + "" + ";" +" "+";"+" " + ";" + "0" + '\n';
                                                File.AppendAllText(dosya10, veri);

                                            }
                                            string dosya3 = @"C:\Users\karis\Desktop\dataset6.csv";

                                            string kayit = srcIp + ";" + dstIp + ";" + protocol_name + ";" + type + ";" + len + ";" + reeltime + ";" + ttl + ";" + respfark + ";" + timefark + ";" + "0" + '\n';
                                            ikinci_kisim = protocol_name + ";" + type + ";" + len + ";" + reeltime + ";" + ttl + ";" + respfark + ";" + timefark + ";";
                                            File.AppendAllText(dosya3, kayit);

                                        }
                                        else if (Convert.ToInt32(beklet1) == 3)
                                        {
                                            Console.WriteLine("Protocol Versiyon...........:" + beklet1); versiyon = beklet1;
                                            Console.WriteLine("Protocol Data Length........:" + comm[10]); data_length = comm[10].ToString();
                                            /* Console.WriteLine("Integrty Part");
                                             Console.WriteLine("Digest Part.................:" + comm[11]);
                                             string tut = "";
                                             for(int l=12;l<44;l++)
                                             {
                                                 tut =""+ cevir(comm[l]);
                                             }
                                             Console.WriteLine("Packet Digest:" + tut);*/
                                            Console.WriteLine("DATA");
                                            if (comm[44] == 49)//31
                                            {
                                                sayacreq++;
                                                type = cevir(comm[44]);
                                                Console.WriteLine("Opcode:Request (0x" + cevir(comm[44]) + ")");
                                                sakla1 = null;
                                                opcode = cevir(comm[44]);
                                                sakla1 = null;
                                                string tut2 = "";
                                                for (int i = 47; i < 49; i++)
                                                {
                                                    tut2 = "" + cevir(comm[i]);
                                                }
                                                sakla1 = null;
                                                string seq_5 = "";
                                                for (int j = 51; j < 53; j++)
                                                {
                                                    seq_5 = "" + cevir(comm[j]);
                                                }
                                                sakla1 = null;


                                                sequ1.Add(seq_5);
                                                double timeistek = (double)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6));

                                                time1.Add(timeistek);
                                                Console.WriteLine("Function: CreateObject (0x" + tut2 + ")"); function = tut2;
                                                /*string seqtut = "";
                                                for (int i = 51; i < 53; i++)
                                                {
                                                    seqtut = "" + comm[i].ToString();
                                                }
                                                sakla1 = null;*/
                                               

                                                if (tut2 == "4F2")
                                                {
                                                    if (comm[67] == 3)
                                                    {
                                                        cpu = "1";//acik
                                                    }
                                                    else if (comm[67] == 1)
                                                    {
                                                        cpu = "0";//kapali
                                                    }
                                                }
                                                
                                                else if (tut2 == "54C")
                                                {
                                                    for (int a = 58; a < 100; a++)
                                                    {
                                                        if (comm.Length > a)
                                                        {
                                                            if (comm[a] == 52)
                                                            {
                                                                if (comm[a + 1] == 2)
                                                                {
                                                                    seq_sakla = seq_5;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                                

                                                tut2 = null;

                                                for (int i = 53; i < 57; i++)
                                                {
                                                    tut2 = "" + cevir(comm[i]);
                                                }
                                                sakla1 = null;
                                                session_id = tut2;
                                                tut2 = null;
                                                Console.WriteLine("Session ID: 0X" + tut2);
                                                Console.WriteLine("----Transport flags:0x" + cevir(comm[57]));
                                                sakla1 = null;
                                                //dectobinary(cevir(comm[57]));
                                                s7_flag = cevir(comm[57]);
                                                sakla1 = null;
                                                //v1 request set
                                              //  var csv2 = new StringBuilder();
                                            //    var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", srcIp, dstIp,
                                              //      srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length, opcode, function, seq_5, session_id, s7_flag, cpu, virgül);

                                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" +  function + ";" + seq_5 + ";" + session_id + ";" + s7_flag + ";" + cpu + ";" + "0" + ";";
                                              //  csv2.AppendLine(newLine2);
                                                //File.AppendAllText(dosya, csv2.ToString());
                                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + opcode + ";" + function + ";" + seq_5 + ";" + session_id+";"+s7_flag + ";" + cpu+ ";" + "" + ";" + "0" + '\n';
                                                File.AppendAllText(dosya10, veri);

                                                cpu = "";

                                            }
                                            else if (comm[44] == 50)//32
                                            {
                                                type = cevir(comm[44]);
                                                resp[b] = (decimal)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6));

                                                b++;
                                                Console.WriteLine("Opcode:Response (0x" + cevir(comm[44]) + ")");
                                                sakla1 = null;
                                                opcode = cevir(comm[44]);
                                                sakla1 = null;
                                                string tut3 = "";
                                                for (int i = 47; i < 49; i++)
                                                {
                                                    tut3 = "" + cevir(comm[i]);
                                                }
                                                sakla1 = null;
                                                string seq_6 = "";
                                                for (int j = 51; j < 53; j++)
                                                {
                                                    seq_6 = "" + cevir(comm[j]);
                                                }
                                                sakla1 = null;




                                                for (int i = 0; i < sequ1.Count; i++)
                                                {
                                                    if (sequ1[i] == seq_6)
                                                    {
                                                        timefark = (e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) - time1[i];
                                                        timetoplam = (double)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) + time1[i];
                                                        Console.WriteLine(timefark);
                                                    }
                                                }
                                                respfark = (decimal)(e.Packet.Timeval.MicroSeconds * Math.Pow(10, -6)) - resp[b - 2];
                                                if (respfark == 0)
                                                {
                                                    respfark = (decimal)Math.Pow(10, -7);
                                                }
                                                Console.WriteLine("Function: CreateObject (0x" + tut3 + ")"); function = tut3;
                                                /* string seqtut1 = "";
                                                 for (int i = 51; i < 53; i++)
                                                 {
                                                     seqtut1 = "" + comm[i].ToString();
                                                 }
                                                 sakla1 = null;*/
                                              
                                                if (tut3 == "54C")
                                                {
                                                    for (int a = 58; a < 100; a++)
                                                    {
                                                        if (comm.Length > a)
                                                        {

                                                            if (seq_sakla == seq_6)
                                                            {
                                                                for (int f = 54; f < 88; f++)
                                                                {
                                                                    if (comm[f] == 0 && comm[f + 1] == 8 && comm[f + 2] == 8)
                                                                    {
                                                                        cpu_control = "1";//acik
                                                                    }
                                                                    else if (comm[f] == 0 && comm[f + 1] == 8 && comm[f + 2] == 0)
                                                                    {
                                                                        cpu_control = "0";
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                tut3 = null;
                                                


                                                Console.WriteLine("----Transport flags:0x" + cevir(comm[54]));
                                                sakla1 = null;
                                                //dectobinary(cevir(comm[57])); 
                                                s7_flag = cevir(comm[53]);
                                                sakla1 = null;
                                                //v1 respons set
                                               // var csv2 = new StringBuilder();
                                               // var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", srcIp, dstIp,
                                                   // srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length, opcode, function, seq_6, " ", s7_flag, " ", cpu_control);
                                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";" + function + ";" + seq_6 + ";" + "0" + ";" + s7_flag + ";" + " " + ";" + cpu_control + ";";

                                                //  csv2.AppendLine(newLine2);
                                                //  File.AppendAllText(dosya, csv2.ToString());
                                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                                versiyon + ";" + data_length + ";" + opcode + ";" + function + ";" + seq_6 + ";" + "" +";"+s7_flag + ";"+"" + ";" + cpu_control + ";" + "0" + '\n';
                                                File.AppendAllText(dosya10, veri);

                                                cpu_control = "";
                                               
                                            }
                                            else if (comm[44] == 51)//33
                                            {
                                                for (int x = 45; x < comm.Length-5; x++)
                                                {
                                                    sakla2 = null;
                                                    for (int y = x; y < x + 2; y++)
                                                    {
                                                        sakla2 = "" + cevir(comm[y]);
                                                    }
                                                    sakla1 = null;
                                                    if (sakla2 == "E46")
                                                    {
                                                        birinci = true; 
                                                        for (int u = x + 1; u < x + 5; u++)
                                                        {
                                                            sakla2 = "" + cevir(comm[u]);
                                                        }
                                                        currentvolume = kayan(sakla2);
                                                        Console.WriteLine("111111111111111111111111111111111111111111111111111111"+currentvolume);
                                                        sakla1 =  null;
                                                    }
                                                    else if (sakla2 == "E40")
                                                    {
                                                        ikinci = true; 
                                                        for (int u = x + 1; u < x + 5; u++)
                                                        {
                                                            sakla2 ="" + cevir(comm[u]);
                                                        }
                                                        curentlevel = kayan(sakla2);
                                                        sakla1 = null;

                                                    }
                                                }
                                                if (srcIp.ToString() == "192.168.41.10" && dstIp.ToString() == "192.168.41.30")
                                                {
                                                    string veri20 = "";
                                                    if (birinci == true && ikinci == true)
                                                    {
                                                        veri20 = currentvolume + ";" + curentlevel + ";" + "1" + '\n';
                                                        File.AppendAllText(dosya11, veri20);
                                                    }
                                                    birinci = ikinci = false;
                                                }
                                                type = cevir(comm[44]);
                                                Console.WriteLine("Opcode:Natification (0x" + cevir(comm[44]) + ")");
                                                sakla1 = null;
                                                opcode = cevir(comm[44]);
                                                sakla1 = null;
                                                //v1 notification
                                               // var csv2 = new StringBuilder();
                                              //  var newLine2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", srcIp, dstIp,
                                                 //   srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin, iso1len, isopdutype, versiyon, data_length, opcode, " ", " ", " ", " ", " ", " ,");
                                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                              versiyon + ";" + data_length + ";"  +  "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0"+";";

                                               // csv2.AppendLine(newLine2);
                                              //  File.AppendAllText(dosya, csv2.ToString());
                                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                               versiyon + ";" + data_length + ";" + opcode+ ";" + "" + ";" + "" + ";" + "" + ";" +""+";"+""+ ";" + " " + ";" + "0" + '\n';
                                                File.AppendAllText(dosya10, veri);
                                            }
                                            string dosya3 = @"C:\Users\karis\Desktop\dataset6.csv";

                                            string kayit =srcIp + ";" + dstIp + ";" + protocol_name + ";" + type + ";" + len + ";" + reeltime + ";" + ttl + ";" + respfark + ";" + timefark + ";" + "0" + '\n';
                                            ikinci_kisim = protocol_name + ";" + type + ";" + len + ";" + reeltime + ";" + ttl + ";" + respfark + ";" + timefark + ";";
                                            File.AppendAllText(dosya3, kayit);

                                        }
                                        
                                    }

                                    ana_kisim = ilk_kisim + ikinci_kisim + ucuncu_kisim;
                                    Console.WriteLine(ana_kisim);
                                    if(device==2)
                                        server.Send(deviceIpAddr2, ana_kisim);
                                    if(device == 3)
                                    {
                                        server.Send(deviceIpAddr2, ana_kisim);
                                        server.Send(deviceIpAddr3, ana_kisim);
                                    }if(device == 4)
                                    {
                                        server.Send(deviceIpAddr2, ana_kisim);
                                        server.Send(deviceIpAddr3, ana_kisim);
                                    }
                                }
                                

                            }
                            else
                            {
                                string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                               "" + ";" + "" + ";" + "" + ";" + "" + ";" + "" + ";" + "" + ";" +""+";"+ "" + ";" + " " + ";" + "0" + '\n';
                                File.AppendAllText(dosya10, veri);
                              /*  var csv1 = new StringBuilder();
                            var newLine1 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", srcIp, dstIp,
                                srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin,iso1len,isopdutype);*/
                                ilk_kisim = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + iso1len + ";" + isopdutype + ";" +
                                             "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0 " + ";";

                                /*csv1.AppendLine(newLine1);
                            File.AppendAllText(dosya, csv1.ToString());*/
                            }
                        }
                       
                    }
                   

                }
                else
                {
                    string veri = srcIp + ";" + dstIp + ";" + srcPort + ";" + dstPort + ";" + "0" + ";" + "0" + ";" + cong + ";" + ecn + ";" + urg + ";" + ack + ";" + push + ";" + reset + ";" + syn + ";" + fin + ";" + "" + ";" + "" + ";" +
                                             "" + ";" + "" + ";" + "" + ";" + "" + ";" + "" + ";" + "" +";"+""+ ";" + "" + ";" + " " + ";" + "0" + '\n';
                    File.AppendAllText(dosya10, veri);
                    var csv = new StringBuilder();
                    var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", srcIp, dstIp,
                      srcPort, dstPort, "0", "0", cong, ecn, urg, ack, push, reset, syn, fin);


                    csv.AppendLine(newLine);
                    File.AppendAllText(dosya, csv.ToString());
                }
            }
            Console.WriteLine("Ana Kisim: "+ana_kisim);
           // ana_kisim = ilk_kisim = ikinci_kisim = ucuncu_kisim = "";
        }
        private static System.Timers.Timer aTimer;
        public static string deviceIpAddr1 = "";
        public static string deviceIpAddr2 = "";
        public static string deviceIpAddr3 = "";
        public static string deviceIpAddr4 = "";
        public static SimpleTcpServer server = new SimpleTcpServer("127.0.0.1:9000");
        public static int device = 0;
        static void Main(string[] args)
        {

            server.Events.ClientConnected += ClientConnected;
            server.Events.ClientDisconnected += ClientDisconnected;
            server.Events.DataReceived += DataReceived;


            server.Start();

            string ver = SharpPcap.Version.VersionString;
            Console.WriteLine("SharpPcap {0}, Example1.IfList.cs", ver);

            // Retrieve the device list
            CaptureDeviceList devices = CaptureDeviceList.Instance;

            // If no devices were found print an error
            if (devices.Count < 1)
            {
                Console.WriteLine("No devices were found on this machine");
                return;
            }

            Console.WriteLine("\nThe following devices are available on this machine:");
            Console.WriteLine("----------------------------------------------------\n");
            //Network adapter 'Intel(R) Wi-Fi 6 AX201 160MHz' on local host
            // Print out the available network devices
            //"Network adapter 'Realtek PCIe GbE Family Controller' on local host"

            foreach (ICaptureDevice dev in devices)
            {

                if (dev.Description == "Network adapter 'Realtek PCIe GbE Family Controller' on local host")
                {
                    Console.WriteLine("{0}\n", dev.ToString());
                    ICaptureDevice device = dev;

                    device.OnPacketArrival +=
                        new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);

                    // Open the device for capturing
                    int readTimeoutMilliseconds = 1000;

                    device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                    //device.Filter

                    string filter1 = "tcp port 102";
                    device.Filter = filter1;

                    Console.WriteLine("-- Listening on {0}, hit 'Enter' to stop...",
                        device.Description);

                    // Start the capturing process
                    device.StartCapture();
                    aTimer = new System.Timers.Timer();
                    aTimer.Interval = 1000;

                    // Hook up the Elapsed event for the timer. 
                    aTimer.Elapsed += OnTimedEvent;

                    // Have the timer fire repeated events (true is the default)
                    aTimer.AutoReset = true;

                    // Start the timer
                    aTimer.Enabled = true;

                    Console.WriteLine("Press the Enter key to exit the program at any time... ");
                    // Wait for 'Enter' from the user.
                    Console.ReadLine();

                    // Stop the capturing process
                    device.StopCapture();

                    // Close the pcap device
                    device.Close();
                    
                }
            }


        }
        static void ClientConnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"{e.IpPort} client connected");
            if (device == 0)
            {
                deviceIpAddr1 = (e.IpPort).ToString();
            }
            if (device == 1)
            {
                deviceIpAddr2 = (e.IpPort).ToString();
            }
            if (device == 2)
            {
                deviceIpAddr3 = (e.IpPort).ToString();
            }
            if (device == 3)
            {
                deviceIpAddr4 = (e.IpPort).ToString();
            }
            device++;
        }

        static void ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}] client disconnected: {e.Reason}");
            device--;
        }

        static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data)}");
        }
        static string kayan(string veri_1)
        {
            Int32 IntRep = Int32.Parse(veri_1, NumberStyles.AllowHexSpecifier);
            // Integer to Byte[] and presenting it for float conversion
            float myFloat = BitConverter.ToSingle(BitConverter.GetBytes(IntRep), 0);
            // There you go
            string rsult = myFloat.ToString();
            return rsult;
        }
    }
    
}

