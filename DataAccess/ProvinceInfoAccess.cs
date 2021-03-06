using Dapper;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Household_Management_System.DataAccess
{
    public class ProvinceInfoAccess
    {
        public static string LoadWardName(string provinceCode, string districtCode, string wardCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("select WardName from LocalAddress where ProvinceCode='" + provinceCode + "'and DistrictCode='" + districtCode + "'and WardCode='" + wardCode + "'", new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static string LoadDistrictName(string provinceCode, string districtCode, string wardCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("select DistrictName from LocalAddress where ProvinceCode='" + provinceCode + "'and DistrictCode='" + districtCode + "'and WardCode='" + wardCode + "'", new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static string LoadProvinceName(string provinceCode, string districtCode, string wardCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("select ProvinceName from LocalAddress where ProvinceCode='" + provinceCode + "'and DistrictCode='" + districtCode + "'and WardCode='" + wardCode + "'", new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static LocalAddressModel LoadFullInfo(string provinceCode, string districtCode, string wardCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<LocalAddressModel>("select * from LocalAddress where ProvinceCode='" + provinceCode + "'and DistrictCode='" + districtCode + "'and WardCode='" + wardCode + "'", new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static List<string> LoadVillageList(string provinceCode, string districtCode, string wardCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("select VillageName from LocalAddress where ProvinceCode='" + provinceCode + "'and DistrictCode='" + districtCode + "'and WardCode='" + wardCode + "'", new DynamicParameters());
                return output.ToList();
            }
        }
        private static string LoadConnectionString(string id = "Default")
        {

            string CurrentDirectory = System.Environment.CurrentDirectory;
            while (CurrentDirectory.Contains("bin"))
            {
                CurrentDirectory = Directory.GetParent(CurrentDirectory).FullName;
            }
            string connectionString = @"Data Source=" + CurrentDirectory + @"\HouseholdDB.db;Version=3;";
            return connectionString;
        }
    }
}
