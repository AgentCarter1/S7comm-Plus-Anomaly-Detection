using System;
using System.Globalization;

namespace ConsoleApp1
{
    class Program
    {
        static string kayan(string veri_1)
        {
         


            Int32 IntRep = Int32.Parse(veri_1, NumberStyles.AllowHexSpecifier);
            // Integer to Byte[] and presenting it for float conversion
            float myFloat = BitConverter.ToSingle(BitConverter.GetBytes(IntRep), 0);
            // There you go
            string rsult = myFloat.ToString();
            return rsult;
        }
        static void Main(string[] args)
        {
            string veri = "bc14";
           Console.WriteLine( kayan(veri));
        }
    }
}
