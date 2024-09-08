using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System.Collections;

using vin.Utils;
using vin.Command;
using vin.Tests;

namespace VinVirtualOS
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("(c) Vin Initial Developers | Fri3nds .G");
            DesignFormat.Banner();

            InitVin();
        }

        public static void InitVin() 
        {
            #region UnitTests
            //UnitTests.Test1();
            //UnitTests.Test2();
            //UnitTests.Test3();
            //UnitTests.Test4();
            //UnitTests.Test_RunCmd();

            //vin.Command.env.Vars.main_test();
            //UnitTests.Test_Exit();
            #endregion



            #region Actual Init

        a:

            DesignFormat.TakeInput(["\n!Guest"," in ","HostPc"," !at ",$"{vin.Command.Prooocessesss.CurrentDirDest}"," $ "]);

            List<string> commands = UserInput.Prepare(UserInput.Input());

            IdentifyCommand.Identify(commands);
            List<string> parsed_commands = IdentifyCommand.ReturnThemPlease();

            PleaseProoocessesss.TheseCommands(parsed_commands);

            IdentifyCommand.CacheClean();

            goto a;

            #endregion
        }

    }
}