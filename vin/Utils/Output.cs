using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vin.Utils
{
    internal class VinOutput
    {
        public static void err(string err, string at) 
        { 
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"    @ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"T: {at}: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(err+"\n");
        }
        public static void errinfo(string err)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("@info ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(err + "\n");
        }
        public static void warn(string warning, string at)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"    @ ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"T: {at}: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(warning + "\n");
        }
        public static void warninfo(string warning)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("@info ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(warning + "\n");
        }
        public static void stdout(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("@info ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(warning + "\n");
        }
        public static void result(string warning)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("@resu ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(warning + "\n");
        }
    }
}
