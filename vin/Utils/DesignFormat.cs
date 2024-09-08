using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace vin.Utils
{
    internal class DesignFormat
    {
        public static void TakeInput(List<string> things)
        {

            for (int i = 0; i < things.Count; i++)
            {
                if (things[i].Contains("@"))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(things[i]);
                } 
                else if (things[i].Contains("!"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(things[i]);
                }
                else if (things[i].Contains(":"))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(things[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(things[i]);
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        
        }

        public static void Banner()
        {
            Console.WriteLine("(#) Vin Virtual Os / Develpement Env\n" +
                              "(c) Fri3nds Enterprise (2008-2088)\n");

        }
    }
}
