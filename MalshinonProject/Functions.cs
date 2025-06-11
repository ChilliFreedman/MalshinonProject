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
        //פונקציה שקוראת לפונקציות של בדיקה והכנסת ערכים במידת הצורך לטבלאות אנשים, מדווח, מטרה, ודיווחים
       public static void FuncAll()
       {
            FuncLogin();
            FuncEnterTarget();
            FuncSetReport();
            
        }
        public static void FuncLogin()
        {
            try
            {
                
                Console.WriteLine("enter your full name or code.");
                string reporterName = Console.ReadLine();
                //קורא לפונקציה שבודקת האם קיים הקוד או השם המלא בעמודה של האנשים
                if (Validation.chekIfNameInDB(reporterName))
                {
                    //נותן ל Reporter.ReporterId את ה Person_Id לפי הקוד או השם המלא (בשביל שנוכל אחר כך להכניס לדיווח)
                    string connectionString = ConnectToSql.connectionString;
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    conn.Open();
                    string query = @"SELECT Person_Id FROM Person WHERE CONCAT(First_Name, Last_Name) = @name OR Code_Person = @name";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", reporterName);
                    Reporter.ReporterId = Convert.ToInt32(cmd.ExecuteScalar());
                    //אם הוא מוכר רק במטרה אז מכניס  ערכים גם לעמודה של המדווח
                    ConnectToSql.InsertToReporter(Reporter.ReporterId);
                    conn.Close();
                }
                else
                {
                    //קורא לפונקציה שמבקשת למשתמש להכניס ערכים של המדווח ומכניסה את הערכים לעמודה של האנשים ושל המדווח
                    FunctionsIfNotIn.FunSetPersonReporter();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
        public static void FuncEnterTarget()
        {
            try
            {
                Console.WriteLine("enter the full name or code of the target.");
                string targetNameOrCode = Console.ReadLine();
                //קורא לפונקציה שבודקת האם קיים הקוד או השם המלא בעמודה של האנשים
                if (Validation.chekIfNameInDB(targetNameOrCode))
                {
                    //נותן ל Target.TargetId את ה Person_Id לפי הקוד או השם המלא (בשביל שנוכל אחר כך להכניס לדיווח)
                    string connectionString = ConnectToSql.connectionString;
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    conn.Open();
                    string query = @"SELECT Person_Id FROM Person WHERE CONCAT(First_Name, Last_Name) = @name OR Code_Person = @name";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", targetNameOrCode);
                    Target.TargetId = Convert.ToInt32(cmd.ExecuteScalar());
                    //אם הוא מוכר רק במדווח ערכים לעמודה של המטרה
                    ConnectToSql.InsertToTarget(Target.TargetId);
                    conn.Close();

                }
                else
                {
                    //קורא לפונקציה שמבקשת למשתמש להכניס ערכים של המטרה ומכניסה את הערכים לעמודה של האנשים ושל המטרה
                    FunctionsIfNotIn.FunSetPersonTarget();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        
        public static void FuncSetReport()
        {
            try
            {
                Console.WriteLine("enter the report.");
                string reportText = Console.ReadLine();
                Report.ReporterId = Reporter.ReporterId;
                Report.TargetId = Target.TargetId;
                Report.ReportText = reportText;
                Report.TimeOfReport = DateTime.Now;
                //מכניס ערכים לטבלה של הדיווחים
                ConnectToSql.InsertToReportDB();
                Console.WriteLine("The report was successfully received, thank you very much.");
                //מעדכן את הטבלה של המדווח בעמודה של כמות הדיווחים
                ConnectToSql.UpdateAmountsReporter();
                //מעדכן את הטבלה של המטרה בעמודה של כמות הדיווחים
                ConnectToSql.UpdateAmountsTarget(); 
                //מקבל את כמות המילים של הדיווח ומעדכן את זה בטבלת המדווח בעמןדה של כמות המילים
                string[] aryWReport = reportText.Split(' ');
                int lengthReport = aryWReport.Length;
                ConnectToSql.UpdateAmountWords(lengthReport);
                int amount15minut = ConnectToSql.get15minutreports();
                ConnectToSql.Update15MinutReportsToTarget(amount15minut);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }


    }
}
