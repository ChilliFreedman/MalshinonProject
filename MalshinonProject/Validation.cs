using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MalshinonProject
{
    internal static class Validation
    {
        public static bool chekIfNameInDB(string name)
        {
            string connectionString = ConnectToSql.connectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query = @"SELECT EXISTS (SELECT 1 FROM Person WHERE (First_Name + Last_Name) = @name OR Code_Person = @name)AS EXISTS";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            object result = cmd.ExecuteScalar();
            bool exists = Convert.ToBoolean(result);
            conn.Close();
            return exists;

        }
    } 
}
