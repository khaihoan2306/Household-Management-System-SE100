using Dapper;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.DataAccess
{
    public class HouseholdAccess
    {
        public static List<HouseholdModel> LoadHousehold()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<HouseholdModel>("select * from Household", new DynamicParameters());
                return output.ToList();
            }
        }
        private static string LoadConnectionString(string id = "Default")
        {
            string connectionString = "Data Source=";
            string dir = Environment.CurrentDirectory;
            dir = dir.Remove(dir.Length - 9, 9);
            dir += "HouseholdDB.db";
            connectionString += dir + ";Version=3;";
            return connectionString;
        }
    }
}
