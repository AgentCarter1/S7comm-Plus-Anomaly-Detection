// This file was auto-generated by ML.NET Model Builder. 

using System;
using BirinciML.Model;

namespace BirinciML.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = new ModelInput()
            {
                Source_Ip = @"192.168.41.10",
                Destination_Ip = @"192.168.41.30",
                Flags_Reversed = 0F,
                Flags_Nonce = 0F,
                Flags_Congestion = 0F,
                Flags_Ecn = 0F,
                Flags_Urg = 0F,
                Flags_Ack = 1F,
                Flags_Push = 1F,
                Flags_Reset = 0F,
                Flags_Syn = 0F,
                Flags_Fin = 0F,
                Iso_Length = 2F,
                Iso_Datatype = @"F0",
                S7_Version = @"3",
                S7_Data_Length = 65F,
                S7_Function = @"0",
                S7_Sequence = @"0",
                S7_Session = @"0",
                S7_Flag = 0F,
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Label with predicted Label from sample data...\n\n");
            Console.WriteLine($"Source_Ip: {sampleData.Source_Ip}");
            Console.WriteLine($"Destination_Ip: {sampleData.Destination_Ip}");
            Console.WriteLine($"Flags_Reversed: {sampleData.Flags_Reversed}");
            Console.WriteLine($"Flags_Nonce: {sampleData.Flags_Nonce}");
            Console.WriteLine($"Flags_Congestion: {sampleData.Flags_Congestion}");
            Console.WriteLine($"Flags_Ecn: {sampleData.Flags_Ecn}");
            Console.WriteLine($"Flags_Urg: {sampleData.Flags_Urg}");
            Console.WriteLine($"Flags_Ack: {sampleData.Flags_Ack}");
            Console.WriteLine($"Flags_Push: {sampleData.Flags_Push}");
            Console.WriteLine($"Flags_Reset: {sampleData.Flags_Reset}");
            Console.WriteLine($"Flags_Syn: {sampleData.Flags_Syn}");
            Console.WriteLine($"Flags_Fin: {sampleData.Flags_Fin}");
            Console.WriteLine($"Iso_Length: {sampleData.Iso_Length}");
            Console.WriteLine($"Iso_Datatype: {sampleData.Iso_Datatype}");
            Console.WriteLine($"S7_Version: {sampleData.S7_Version}");
            Console.WriteLine($"S7_Data_Length: {sampleData.S7_Data_Length}");
            Console.WriteLine($"S7_Function: {sampleData.S7_Function}");
            Console.WriteLine($"S7_Sequence: {sampleData.S7_Sequence}");
            Console.WriteLine($"S7_Session: {sampleData.S7_Session}");
            Console.WriteLine($"S7_Flag: {sampleData.S7_Flag}");
            Console.WriteLine($"\n\nPredicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }
    }
}
