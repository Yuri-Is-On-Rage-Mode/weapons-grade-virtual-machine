using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vin.Utils
{
    internal class Warnings
    {
    }

    public class VinWarningList
    {
        public static List<string> WarningsAre = [];
        public static void New(string err_msg)
        {
            WarningsAre.Add(err_msg);

            //Console.WriteLine(WarningsAre.ToArray());
        }

        public static void CacheClean()
        {

            WarningsAre.Clear();

        }

        public static void ListThem()
        {
            //BreakPoint.hit("listthem");
            if (WarningsAre.Count > 0)
            {
                if (WarningsAre.Count > 0 && WarningsAre.Count < 20)
                {
                    VinOutput.warninfo($"Found {WarningsAre.Count} Warnings: Showing {WarningsAre.Count} out of {WarningsAre.Count} Warnings!");
                    for (int i = 0; i <= (WarningsAre.Count() - 1); i++)
                    {
                        VinOutput.warn(WarningsAre[i],"StdWarning");
                    }
                }
                else if (WarningsAre.Count > 20)
                {
                    VinOutput.warninfo($"Found {WarningsAre.Count} Warnings: Showing 20 out of {WarningsAre.Count} Warnings!");
                    for (int i = 0; i <= (21); i++)
                    {
                        VinOutput.warn(WarningsAre[i],"StdWarning");
                    }
                }
                else
                {
                    VinOutput.warninfo($"Found {WarningsAre.Count} Warnings: Showing {WarningsAre.Count} out of {WarningsAre.Count} Warnings!");
                    for (int i = 0; i <= (WarningsAre.Count() - 1); i++)
                    {
                        VinOutput.warn(WarningsAre[i],"StdWarning");
                    }
                }
            }
            else
            {
                VinOutput.warninfo("no Warnings found!");
            }
        }

        public static void ListThemAll()
        {
            //BreakPoint.hit("listthem");
            if (WarningsAre.Count > 0)
            {
                VinOutput.warninfo($"Found {WarningsAre.Count} Warnings: Showing {WarningsAre.Count} out of {WarningsAre.Count} Warnings!");
                for (int i = 0; i <= (WarningsAre.Count() - 1); i++)
                {
                    VinOutput.warn(WarningsAre[i],"StdWarning");
                }
            }
            else
            {
                VinOutput.warninfo("no Warnings found!");
            }
        }
    }
}
