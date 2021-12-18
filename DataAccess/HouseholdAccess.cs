﻿using Dapper;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Household_Management_System.DataAccess
{
    public class HouseholdAccess
    {
        public static List<HouseholdModel> LoadHousehold(string village = "", string text = "")
        {
            string hostName = text.ToUpper();
            text = text.ToLower();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<HouseholdModel>("select * from Household where Village like '%" + village + "%' and (HouseholdCode like '%"+text+"%' or HostName like '%"+hostName+"%' or Address like '%"+text+"%')", new DynamicParameters());
                return output.ToList();
            }
        }
        public static bool CheckHouseholdCode(string householdCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("select HouseholdCode from Household where HouseholdCode='" + householdCode + "'", new DynamicParameters());
                
                if (output.FirstOrDefault() != null) return true;
                return false;
            }
        }
        public static string LoadAddress(string householdCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("select Address from Household where HouseholdCode='" + householdCode + "'", new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static void SaveHousehold(HouseholdModel household)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Household values (@HouseholdCode, @HostName, @Address, @Village, @Ward, @District, @Province, @Note)", household);
            }
        }
        public static void DeleteHousehold(string householdCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Household where HouseholdCode='" + householdCode + "'");
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
