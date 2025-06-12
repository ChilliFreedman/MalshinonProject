using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;

namespace MalshinonProject
{
    internal static class ConnectToSql
    {
        public static string connectionString = "server=localhost;" + "user=root;" + "database=malshinon_project;" + "port=3306;";

        public static void InsertToReportDB()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query1 = @"INSERT INTO Report (Reporter_Id, Target_Id, Report_Text,Time_Report )
                  VALUES (@Reporter_Id, @Target_Id, @Report_Text, @Time_Report)";
            MySqlCommand cmd = new MySqlCommand(query1, conn);
            cmd.Parameters.AddWithValue("@Reporter_Id", Report.ReporterId);
            cmd.Parameters.AddWithValue("@Target_Id", Report.TargetId);
            cmd.Parameters.AddWithValue("@Report_Text", Report.ReportText);
            cmd.Parameters.AddWithValue("@Time_Report", Report.TimeOfReport);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateAmountsReporter()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query2 = @"UPDATE Reporters SET Amount_Reports = Amount_Reports + 1 WHERE Reporter_Id = @Id";
            MySqlCommand cmd = new MySqlCommand(query2, conn);
            cmd.Parameters.AddWithValue("@Id", Reporter.ReporterId);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateAmountWords(int adder)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query8 = @"UPDATE Reporters SET Amount_Words = Amount_Words + @adder WHERE Reporter_Id = @Id";
            MySqlCommand cmd = new MySqlCommand(query8, conn);
            cmd.Parameters.AddWithValue("@Id", Reporter.ReporterId);
            cmd.Parameters.AddWithValue("@adder", adder);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateAmountsTarget()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query3 = @"UPDATE Targets SET Amount_Reports = Amount_Reports + 1 WHERE Target_Id = @Id";
            MySqlCommand cmd = new MySqlCommand(query3, conn);
            cmd.Parameters.AddWithValue("@Id", Target.TargetId);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static int getpersonid()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query4 = @"SELECT Person_Id FROM Person WHERE Code_Person = @code_Person";
            MySqlCommand cmd = new MySqlCommand(query4, conn);
            cmd.Parameters.AddWithValue("@Code_Person", Person.CodePerson);
            int idperson = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return idperson;




        }

        public static void InsertToPerson()
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query5 = @"INSERT INTO Person (First_Name, Last_Name, Code_Person)
                  VALUES (@First_Name, @Last_Name, @Code_Person)";
            MySqlCommand cmd = new MySqlCommand(query5, conn);
            cmd.Parameters.AddWithValue("@First_Name", Person.FirstName);
            cmd.Parameters.AddWithValue("@Last_Name", Person.LastName);
            cmd.Parameters.AddWithValue("@Code_Person", Person.CodePerson);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public static void InsertToReporter(int personid)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query6 = @"
            INSERT INTO Reporters (Reporter_Id, Amount_Reports, Amount_Words)
            VALUES (@Reporter_Id, @Amount_Reports, @Amount_Words)
            ON DUPLICATE KEY UPDATE Reporter_Id = Reporter_Id;
            ";
            MySqlCommand cmd = new MySqlCommand(query6, conn);
            cmd.Parameters.AddWithValue("@Reporter_Id", personid);
            cmd.Parameters.AddWithValue("@Amount_Reports", 0);
            cmd.Parameters.AddWithValue("@Amount_Words", 0);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public static void InsertToTarget(int personid)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query7 = @"
            INSERT INTO Targets (Target_Id, Amount_Reports, Amount_In_15_Minuts)
            VALUES (@Target_Id, @Amount_Reports, @Amount_In_15_Minuts)
            ON DUPLICATE KEY UPDATE Target_Id = Target_Id;
            ";

            MySqlCommand cmd = new MySqlCommand(query7, conn);
            cmd.Parameters.AddWithValue("@Target_Id", personid);
            cmd.Parameters.AddWithValue("@Amount_Reports", 0);
            cmd.Parameters.AddWithValue("@Amount_In_15_Minuts", 0);
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        public static int get15minutreports()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query9 = @"SELECT COUNT(*) FROM Report WHERE Target_Id = @corentid AND  Time_Report BETWEEN DATE_SUB(@datetime, INTERVAL 15 MINUTE) AND @datetime";
            MySqlCommand cmd = new MySqlCommand(query9, conn);
            cmd.Parameters.AddWithValue("@corentid", Report.TargetId);
            cmd.Parameters.AddWithValue("@datetime", Report.TimeOfReport);
            int amount15minut = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return amount15minut;
        }
        public static void Update15MinutReportsToTarget(int get15minutreports)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query10 = @"UPDATE Targets SET Amount_In_15_Minuts  =  + @get15minutreports WHERE Target_Id = @Id";
            MySqlCommand cmd = new MySqlCommand(query10, conn);
            cmd.Parameters.AddWithValue("@Id", Target.TargetId);
            cmd.Parameters.AddWithValue("@get15minutreports", get15minutreports);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void InsertToAlert(string text)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query11 = @"INSERT INTO Alerts (Target_Id, Brief_Explanation)
                  VALUES (@Target_Id, @Brief_Explanation)";
            MySqlCommand cmd = new MySqlCommand(query11, conn);
            cmd.Parameters.AddWithValue(@"Target_Id", Report.TargetId);
            cmd.Parameters.AddWithValue("@Brief_Explanation", text);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static int GetAmuntOfReports(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query12 = @"SELECT Amount_Reports FROM Targets WHERE Target_Id = @id ";
            MySqlCommand cmd = new MySqlCommand(query12, conn);
            cmd.Parameters.AddWithValue("@id", id);

            int amounreports = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return amounreports;
        }
        //פונקצית חיבור לDB והדפסת כל ההתרעות
        public static List<KeyValuePair<string, string>> GetaAlerts()
        {
            try
            {
                List<KeyValuePair<string, string>> FullNameBrief = new List<KeyValuePair<string, string>>();
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string query12 = "SELECT CONCAT(person.First_Name,' ', person.Last_Name)  AS Fullname ,Alerts.Brief_Explanation AS Brief " +
                                   "FROM Person "+
                                   "JOIN Alerts "+
                                   "ON Alerts.Target_Id = Person.Person_Id";
 
                                   
                MySqlCommand cmd = new MySqlCommand(query12, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string fullName = reader["Fullname"].ToString();
                    string brief = reader["Brief"].ToString();

                    FullNameBrief.Add(new KeyValuePair<string, string>(fullName, brief));
                }
                reader.Close();
                conn.Close();
                return FullNameBrief;

        }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        // פונקציית חיבור לDB וקבלת כל הדווחים המועמדים לגיוס
        public static Dictionary<string,string> GetAllStrongReporters()
        {
            Dictionary<string,string> FullNameAndCode = new Dictionary<string,string>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query13 = @"SELECT  CONCAT(person.First_Name,' ', person.Last_Name)  AS Fuulname ,person.Code_Person AS Code
                               FROM person
                               JOIN reporters
                               on reporters.Reporter_Id = person.Person_Id
                               WHERE reporters.Amount_Reports >= 10 AND reporters.Amount_Words / reporters.Reporter_Id >= 100 ";
            MySqlCommand cmd = new MySqlCommand(query13, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FullNameAndCode.Add(reader.GetString("Fuulname"), reader.GetString("Code"));
            }
            reader.Close();
            conn.Close();
            return FullNameAndCode;
        }
    }  
}

