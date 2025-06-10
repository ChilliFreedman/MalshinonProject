using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace MalshinonProject
{
    internal static class Functions
    {
       public static void FuncAll()
       {
            FuncLogin();
            FuncEnterTarget();
            FuncEnterReport();
            FuncSetReport();
            
        }
        public static void FuncLogin()
        {

            Console.WriteLine("enter your full name or code.");
            string reporterName = Console.ReadLine();
            if (Validation.chekIfNameInDB(reporterName))
            {
                string connectionString = ConnectToSql.connectionString;
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string query = @"SELECT Person_Id FROM Person WHERE (First_Name + Last_Name) = @name OR Code_Person = @name";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", reporterName);
                MySqlDataReader reader = cmd.ExecuteReader();
                Reporter.ReporterId = reader.GetInt32("Person_Id");
                reader.Close();
                conn.Close();
                



            }
            else
            {
                FunctionsIfNotIn.FunSetPersonReporter();
            }
            



        }
        


        
        
        
        public static void FuncEnterTarget()
        {
            Console.WriteLine("enter the full name or code of the target.");
            string targetNameOrCode = Console.ReadLine();
            if (Validation.chekIfNameInDB(targetNameOrCode))
            {
                string connectionString = ConnectToSql.connectionString;
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string query = @"SELECT Person_Id FROM Person WHERE (First_Name + Last_Name) = @name OR Code_Person = @name";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", targetNameOrCode);
                MySqlDataReader reader = cmd.ExecuteReader();
                Target.TargetId = reader.GetInt32("Person_Id");
                reader.Close();
                conn.Close();
                
            }
            else
            {
                FunctionsIfNotIn.FunSetPersonTarget();

            }

        }
        public static string FuncEnterReport()
        {
            Console.WriteLine("enter the report.");
            string reportText = Console.ReadLine();
            return reportText;
        }
        public static void FuncSetReport()
        {
            Report.ReporterId = Reporter.ReporterId;
            Report.TargetId = Target.TargetId;
            Report.ReportText = FuncEnterReport();
            Report.TimeOfReport = DateTime.Now;
            ConnectToSql.InsertToReportDB();
            ConnectToSql.UpdateAmountsReporter(1, Reporter.AmountOfReports);
            
            ConnectToSql.UpdateAmountsTarget();
            string[] aryWReport = FuncEnterReport().Split(' ');
            int lengthReport = aryWReport.Length;
            ConnectToSql.UpdateAmountsReporter(lengthReport, Reporter.AmountOfWords);


        }


    }
}
