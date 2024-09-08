using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using vin.Utils;

namespace vin.Utils
{
    internal class Errors
    {
        
    }

    public class VinErrorList
    {
        public static List<string> ErrorsAre   = [];
        public static void New(string err_msg)
        {
            ErrorsAre.Add(err_msg);

            //Console.WriteLine(ErrorsAre.ToArray());
        }

        public static void CacheClean()
        {

            ErrorsAre.Clear();

        }

        public static void ListThem()
        {
            //BreakPoint.hit("listthem");
            if (ErrorsAre.Count > 0)
            {
                if (ErrorsAre.Count > 0 && ErrorsAre.Count < 20)
                {
                    VinOutput.errinfo($"Found {ErrorsAre.Count} Errors: Showing {ErrorsAre.Count} out of {ErrorsAre.Count} Errors!");
                    for (int i = 0; i <= (ErrorsAre.Count() - 1); i++)
                    {
                        VinOutput.err(ErrorsAre[i],"StdError");
                    }
                }
                else if (ErrorsAre.Count > 20)
                {
                    VinOutput.errinfo($"Found {ErrorsAre.Count} Errors: Showing 20 out of {ErrorsAre.Count} Errors!");
                    for (int i = 0; i <= (21); i++)
                    {
                        VinOutput.err(ErrorsAre[i],"StdError");
                    }
                }
                else 
                {
                    VinOutput.errinfo($"Found {ErrorsAre.Count} Errors: Showing {ErrorsAre.Count} out of {ErrorsAre.Count} Errors!");
                    for (int i = 0; i <= (ErrorsAre.Count() - 1); i++)
                    {
                        VinOutput.err(ErrorsAre[i],"StdError");
                    }
                }
            }
            else 
            {
                VinOutput.warninfo("no Errors found!");
            }       
        }

        public static void ListThemAll()
        {
            //BreakPoint.hit("listthem");
            if (ErrorsAre.Count > 0)
            {
                VinOutput.errinfo($"Found {ErrorsAre.Count} Errors: Showing {ErrorsAre.Count} out of {ErrorsAre.Count} Errors!");
                for (int i = 0; i <= (ErrorsAre.Count() - 1); i++)
                {
                    VinOutput.err(ErrorsAre[i],"StdError");
                }
            }
            else
            {
                VinOutput.warninfo("no Errors found!");
            }
        }
    }
}
