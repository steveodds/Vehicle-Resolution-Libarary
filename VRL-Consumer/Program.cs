using System;
using Vehicle_Resolution_Libarary;

namespace VRL_Consumer
{
    class Program
    {
        static void Main()
        {
            var lib = new Start();
            var result = lib.ResolveSingleWord("SUNARU", @"C:\Users\user\Documents\sample\Agilis LIVE Make & Models.csv");
            Console.WriteLine(result);
        }
    }
}
