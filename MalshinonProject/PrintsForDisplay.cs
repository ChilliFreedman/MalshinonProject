﻿using System;
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
            Dictionary<string,string>  DictStrongReporters = new Dictionary<string,string>();
            DictStrongReporters = ConnectToSql.GetAllStrongReporters();
            
            foreach (var keyvalue in DictStrongReporters)
            {
                Console.WriteLine($"\nFuulName :{keyvalue.Key} \ncode: {keyvalue.Value}");

            }
        }

        public static void PrintSAlerts()
        {
            List<KeyValuePair<string, string>> FullNameBrief = new List<KeyValuePair<string, string>>();
            int a = 1;
            FullNameBrief = ConnectToSql.GetaAlerts();
            foreach (var keyvalue in FullNameBrief)

            {
                Console.WriteLine($"\nAlert {a}: \nFuulName: {keyvalue.Key}. \nBrief: {keyvalue.Value}.");
                a++;
            }
        }
    }   
}
