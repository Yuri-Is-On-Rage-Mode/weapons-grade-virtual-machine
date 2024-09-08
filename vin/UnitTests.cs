using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vin.Command;
using vin.Utils;

namespace vin.Tests
{
    internal class UnitTests
    {
        public static void Test1()
        {
            VinErrorList.New("first error");

            for (int i = 0; i < 20; i++)
            {
                VinErrorList.New("message number: " + i);
            }

            VinErrorList.New("last message");

            VinErrorList.ListThemAll();

            Console.ReadLine();
        }
        public static void Test2()
        {
            VinOutput.err("f safdsf dfadf sdf asdf d", "UnitTest");
            VinOutput.warn("8gfads fdf af d h hk hgfkh ", "UnitTest");

            VinOutput.stdout("f safdsf dfadf sdf asdf d");
            VinOutput.err("f safdafdfsdf d fds fasd d", "UnitTest");

            VinOutput.warn("8g yufiu f f g h hk hgfkh ", "UnitTest");
        }
        public static void Test3()
        {
            VinWarningList.New("first error");

            for (int i = 0; i < 20; i++)
            {
                VinWarningList.New("message number: " + i);
            }

            VinWarningList.New("last message");

            VinWarningList.ListThemAll();

            Console.ReadLine();
        }
        public static void Test4()
        {
            // command parsing tests 
            IdentifyCommand.Identify(["sudo", "make", "me", "a", "coffee"]);
            List<string> parsed_commands = IdentifyCommand.ReturnThemPlease();

            for (int i = 0; i < parsed_commands.Count; i++)
            {
                Console.WriteLine(parsed_commands[i]);
            }
        }

        public static void Test_RunCmd()
        {
            RunOnWindows.RunCmd(["sudo","make","me","a","coffee"]);

            Test_Exit();
        }

        public static void Test_Exit()
        {
           Environment.Exit(0);
        }
    }
}
