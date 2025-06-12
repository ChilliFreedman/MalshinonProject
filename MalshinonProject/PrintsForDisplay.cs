using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalshinonProject
{
    internal static class PrintsForDisplay
    {
        public static void PrintStrongReporters()
        {
            Dictionary<string,string>  dict = new Dictionary<string,string>();
            dict = ConnectToSql.GetAllStrongReporters();
            foreach (var keyvalue in dict)


                {
                    Console.WriteLine($"FuulName :{keyvalue.Key} code: {keyvalue.Value}");

                }
        }
    }   
}
