// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML.Data;

namespace BirinciML.Model
{
    public class ModelInput
    {
        [ColumnName("Source_Ip"), LoadColumn(0)]
        public string Source_Ip { get; set; }


        [ColumnName("Destination_Ip"), LoadColumn(1)]
        public string Destination_Ip { get; set; }


        [ColumnName("Flags_Reversed"), LoadColumn(2)]
        public float Flags_Reversed { get; set; }


        [ColumnName("Flags_Nonce"), LoadColumn(3)]
        public float Flags_Nonce { get; set; }


        [ColumnName("Flags_Congestion"), LoadColumn(4)]
        public float Flags_Congestion { get; set; }


        [ColumnName("Flags_Ecn"), LoadColumn(5)]
        public float Flags_Ecn { get; set; }


        [ColumnName("Flags_Urg"), LoadColumn(6)]
        public float Flags_Urg { get; set; }


        [ColumnName("Flags_Ack"), LoadColumn(7)]
        public float Flags_Ack { get; set; }


        [ColumnName("Flags_Push"), LoadColumn(8)]
        public float Flags_Push { get; set; }


        [ColumnName("Flags_Reset"), LoadColumn(9)]
        public float Flags_Reset { get; set; }


        [ColumnName("Flags_Syn"), LoadColumn(10)]
        public float Flags_Syn { get; set; }


        [ColumnName("Flags_Fin"), LoadColumn(11)]
        public float Flags_Fin { get; set; }


        [ColumnName("Iso_Length"), LoadColumn(12)]
        public float Iso_Length { get; set; }


        [ColumnName("Iso_Datatype"), LoadColumn(13)]
        public string Iso_Datatype { get; set; }


        [ColumnName("S7_Version"), LoadColumn(14)]
        public string S7_Version { get; set; }


        [ColumnName("S7_Data_Length"), LoadColumn(15)]
        public float S7_Data_Length { get; set; }


        [ColumnName("S7_Function"), LoadColumn(16)]
        public string S7_Function { get; set; }


        [ColumnName("S7_Sequence"), LoadColumn(17)]
        public string S7_Sequence { get; set; }


        [ColumnName("S7_Session"), LoadColumn(18)]
        public string S7_Session { get; set; }


        [ColumnName("S7_Flag"), LoadColumn(19)]
        public float S7_Flag { get; set; }


        [ColumnName("Label"), LoadColumn(20)]
        public string Label { get; set; }


    }
}