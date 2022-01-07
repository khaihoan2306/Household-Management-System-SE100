using Dapper;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Household_Management_System.DataAccess
{
    public class LocalPoliceAccess
    {
        public static LocalPoliceModel LoadPolice(string username)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<LocalPoliceModel>("select * from LocalPolice where Username='" + username + "'", new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static List<LocalPoliceModel> LoadListPolice(string province, string district, string ward)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<LocalPoliceModel>("select * from LocalPolice where ProvinceManage='"+province+"' and DistrictManage='"+district+"' and WardManage='"+ward+"'", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void ChangePassword(string newPass, string username)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update LocalPolice set Password='" + newPass + "' where Username='" + username + "'");
            }
        }
        public static void UpdatePolice(LocalPoliceModel police)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update LocalPolice set Name='" + police.Name + "', IdentityCode='"+police.IdentityCode+"', BirthDay='"+police.BirthDay+"', Phone='"+police.Phone+"', Gender='"+police.Gender+"', Position='"+police.Position+"' where Username='" + police.Username + "'");
            }
        }
        public static void SavePolice(LocalPoliceModel police)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into LocalPolice values (@IdentityCode, @Name, @BirthDay, @Position, @Phone, @Address, @ProvinceManage, @DistrictManage, @WardManage, @Gender, @Role, @Username, @Password)", police);
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
