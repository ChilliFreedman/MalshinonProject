using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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
            SetAlert();
        }
        public static void FuncLogin()
        {
            try
            {
                
                Console.WriteLine("\nEnter your full name or code.");
                string reporterName = Console.ReadLine();
                //קורא לפונקציה שבודקת האם קיים הקוד או השם המלא בעמודה של האנשים
                if (Validation.chekIfNameInDB(reporterName))
                {
                    //נותן ל Reporter.ReporterId את ה Person_Id לפי הקוד או השם המלא (בשביל שנוכל אחר כך להכניס לדיווח)
                    string connectionString = ConnectToSql.connectionString;
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    conn.Open();
                    string query = @"SELECT Person_Id FROM Person WHERE CONCAT(First_Name,' ', Last_Name) = @name OR Code_Person = @name";
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
                Console.WriteLine("\nEnter the full name or code of the target.");
                string targetNameOrCode = Console.ReadLine();
                //קורא לפונקציה שבודקת האם קיים הקוד או השם המלא בעמודה של האנשים
                if (Validation.chekIfNameInDB(targetNameOrCode))
                {
                    //נותן ל Target.TargetId את ה Person_Id לפי הקוד או השם המלא (בשביל שנוכל אחר כך להכניס לדיווח)
                    string connectionString = ConnectToSql.connectionString;
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    conn.Open();
                    string query = @"SELECT Person_Id FROM Person WHERE CONCAT(First_Name,' ', Last_Name) = @name OR Code_Person = @name";
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
                Console.WriteLine("\nEnter the report.");
                string reportText = Console.ReadLine();
                Report.ReporterId = Reporter.ReporterId;
                Report.TargetId = Target.TargetId;
                Report.ReportText = reportText;
                Report.TimeOfReport = DateTime.Now;
                //מכניס ערכים לטבלה של הדיווחים
                ConnectToSql.InsertToReportDB();
                Console.WriteLine("\nThe report was successfully received, thank you very much.");
                //מעדכן את הטבלה של המדווח בעמודה של כמות הדיווחים
                ConnectToSql.UpdateAmountsReporter();
                //מעדכן את הטבלה של המטרה בעמודה של כמות הדיווחים
                ConnectToSql.UpdateAmountsTarget(); 
                //מקבל את כמות המילים של הדיווח ומעדכן את זה בטבלת המדווח בעמןדה של כמות המילים
                string[] aryWReport = reportText.Split(' ');
                int lengthReport = aryWReport.Length;
                ConnectToSql.UpdateAmountWords(lengthReport);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            


        }
        //פונקציה שמכניסה ערכים לטבלת Alerts במידת הצורך
        public static void SetAlert()
        {
            int amount15minut = ConnectToSql.get15minutreports();
            ConnectToSql.Update15MinutReportsToTarget(amount15minut);
            //קבלת מספר הדיווחים על המטרה עד כה
            int amountreports = ConnectToSql.GetAmuntOfReports(Report.TargetId);
            //בדיקה האם יש מעל 20 דיווחים על המטרה או מתפרצת של 3 ומעלה דיווחים ב15 דקות האחרונות ולפי זה מחליט אם ליצר התרעות ולהכניס את הערכים
            if (amount15minut >= 3 && amountreports >= 20)
            {
                ConnectToSql.InsertToAlert($"The alert was created after receiving {amount15minut} reports Between {Report.TimeOfReport - TimeSpan.FromMinutes(15)} and {Report.TimeOfReport} and because there are {amountreports} reports on the target as of {Report.TimeOfReport} o'clock.");
                Console.WriteLine("On alert was set!!!");
            }
            else if (amount15minut >= 3)
            {
                ConnectToSql.InsertToAlert($"The alert was created after receiving {amount15minut} reports Between {Report.TimeOfReport - TimeSpan.FromMinutes(15)} and {Report.TimeOfReport}.");
                Console.WriteLine("On alert was set!!!");
            }
            else if (amountreports >= 20)
            {
                ConnectToSql.InsertToAlert($"The alert was created because there are {amountreports} reports on the target as of {Report.TimeOfReport} o'clock.");
                Console.WriteLine("On alert was set!!!");
            }
        }

        
    }
}
