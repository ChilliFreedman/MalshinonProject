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
        public static string connectionString = "server=localhost;" + "user=root;" + "database=malshinon_project;" + "port=3306;";

        public static void InsertToReportDB()
        {
            //string connectionString = ConnectToSql.connectionString;
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
        public static void UpdateAmountsReporter(int adder)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query2 = @"UPDATE Repoters SET Amount_Reports = @Amount_Reports + @adder WHERE Reporter_Id = @Id";
            MySqlCommand cmd = new MySqlCommand(query2, conn);
            cmd.Parameters.AddWithValue("@Id", Reporter.ReporterId);
            
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateAmountWords(int adder)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query8 = @"UPDATE Repoters SET Amount_Words = @Amount_Words + @adder WHERE Reporter_Id = @Id";
            MySqlCommand cmd = new MySqlCommand(query8, conn);
            cmd.Parameters.AddWithValue("@Id", Reporter.ReporterId);

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

        public static int getpersonid(string codename)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query4 = @"SELECT Person_Id FROM Person WHERE Code_Person = @codePerson";
            MySqlCommand cmd = new MySqlCommand(query4, conn);
            cmd.Parameters.AddWithValue("@Code_Person", codename);
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
            string query6 = @"INSERT INTO Reporter (Reporter_Id, Amount_Reports, Amount_Words)
                  VALUES (@Reporter_Id, @Amount_Reports, @Amount_Words)";
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
            string query7 = @"INSERT INTO  Target (Target_Id, Amount_Reports, Amount_In_15_Minuts)
                  VALUES (@Target_Id, @Amount_Reports, @Amount_In_15_Minuts)";
            MySqlCommand cmd = new MySqlCommand(query7, conn);
            cmd.Parameters.AddWithValue("@Target_Id", personid);
            cmd.Parameters.AddWithValue("@Amount_Reports", 0);
            cmd.Parameters.AddWithValue("@Amount_In_15_Minuts", 0);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

    }
}
