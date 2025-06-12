using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalshinonProject
{
    internal class Menue
    {
        public static void AdminMenu()
        {
            string adminmenue = "Welcome to the system:\r\n" +
                                "1. To submit a report, press 1\r\n" +
                                "2. To receive the list of candidates for recruitment, press 2\r\n" +
                                "3. To receive all alerts, press 3";
            Console.WriteLine(adminmenue);
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Functions.FuncAll();
                    break;
                case "2":
                    PrintsForDisplay.PrintStrongReporters();
                    break;
                case "3":
                    PrintsForDisplay.PrintSAlerts();
                    break;




            }
        }
    }
}
