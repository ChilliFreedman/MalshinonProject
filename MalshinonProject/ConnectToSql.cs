using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MalshinonProject
{
    internal static  class ConnectToSql
    {
        public static string connectionString = "server=localhost;" + "user=root;" + "database=malshinon;" + "port=3306;";

        public static void InsertToReportDB()
        {
            //string connectionString = ConnectToSql.connectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query1 = @"INSERT INTO Reports (ReporterId, TargetId, ReportText,TimeOfReport )
                  VALUES (@ReporterId, @TargetId, @ReportText, @TimeOfReport)";
            MySqlCommand cmd = new MySqlCommand(query1, conn);
            cmd.Parameters.AddWithValue("@ReporterId", Report.ReporterId);
            cmd.Parameters.AddWithValue("@TargetId", Report.TargetId);
            cmd.Parameters.AddWithValue("@ReportText", Report.ReportText);
            cmd.Parameters.AddWithValue("@TimeOfReport", Report.TimeOfReport);
            
        }
        public static void UpdateAmountsReporter(int adder,int colom)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query2 = @"UPDATE Repoter SET @colom = @colom + @adder WHERE ReporterId = @Id";
            MySqlCommand cmd = new MySqlCommand(query2, conn);
            cmd.Parameters.AddWithValue("@Id", Reporter.ReporterId);
            cmd.Parameters.AddWithValue("@colom", colom);
            conn.Close();
        }
        public static void UpdateAmountsTarget()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query3 = @"UPDATE Target SET AmountReports = AmountReports + 1 WHERE TargetId = @Id";
            MySqlCommand cmd = new MySqlCommand(query3, conn);
            cmd.Parameters.AddWithValue("@Id", Target.TargetId);
            conn.Close();
        }

        public static int getpersonid(string codename)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query4 = @"SELECT Person.Id FROM Person WHERE CodePerson = @codePerson";
            MySqlCommand cmd = new MySqlCommand(query4, conn);
            cmd.Parameters.AddWithValue("@codePerson", codename);
            MySqlDataReader reader = cmd.ExecuteReader();
            int idperson = reader.GetInt32("Person_Id");
            reader.Close();
            conn.Close();
            return idperson;
        }

        public static void InsertToPerson()
        {
         
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query5 = @"INSERT INTO Person (FirstName, LastName, CodePerson)
                  VALUES (@FirstName, @LastName, @CodePerson)";
            MySqlCommand cmd = new MySqlCommand(query5, conn);
            cmd.Parameters.AddWithValue("@FirstName", Person.FirstName);
            cmd.Parameters.AddWithValue("@LastName", Person.LastName);
            cmd.Parameters.AddWithValue("@CodePerson", Person.CodePerson);
            conn.Close();

        }

        public static void InsertToReporter(int personid)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query6 = @"INSERT INTO Reporter (ReporterId, AmountOfReports, AmountOfWords)
                  VALUES (@ReporterId, @AmountOfReports, @AmountOfWords)";
            MySqlCommand cmd = new MySqlCommand(query6, conn);
            cmd.Parameters.AddWithValue("@ReporterId", personid);
            cmd.Parameters.AddWithValue("@AmountOfReports", 0);
            cmd.Parameters.AddWithValue("@AmountOfWords", 0);
            conn.Close();

        }

        public static void InsertToTarget(int personid)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query7 = @"INSERT INTO  Target (TargetId, AmountOfReports, AmountOfReportsIn15Minuts)
                  VALUES (@TargetId, @AmountOfReports, @AmountOfReportsIn15Minuts)";
            MySqlCommand cmd = new MySqlCommand(query7, conn);
            cmd.Parameters.AddWithValue("@TargetId", personid);
            cmd.Parameters.AddWithValue("@AmountOfReports", 0);
            cmd.Parameters.AddWithValue("@AmountOfWords", 0);
            conn.Close();

        }

    }
}
