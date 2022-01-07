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
    public class ResidenceAccess
    {
        public static List<ResidenceModel> LoadPeople(string text = "", string village = "")
        {
            text = text.ToLower();
            string name = text.ToUpper();
            string query = @"SELECT Residence.IdentityCode, Residence.Name, Residence.BirthDay, Residence.Gender, Residence.PermanentAddress, ShelterAddress, ArrivalDay, Residence.Note
                            FROM Demographic, Residence, Household
                            WHERE (Residence.IdentityCode like '%" + text + "%' or Residence.Name like '%" + name + "%' or Residence.Gender like '%" + text + "%' or Residence.BirthDay like '%" + text + "%' or Residence.PermanentAddress like '%" + text + "%' or ArrivalDay like '%" + text + "%') " +
                            "and Demographic.IdentityCode = Residence.IdentityCode and Demographic.HouseholdCode = Household.HouseholdCode and Household.Village like '%" + village + "%'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ResidenceModel>(query, new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SavePerson(ResidenceModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Residence values (@IdentityCode, @Name, @BirthDay, @Gender, @PermanentAddress, @ShelterAddress, @ArrivalDay, @Note)", person);
                cnn.Execute("update Demographic set LivingStatus='Tạm trú' where IdentityCode='" + person.IdentityCode + "'");
            }
        }
        public static void DeletePerson(string identityCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Absence where IdentityCode='" + identityCode + "'");
                cnn.Execute("delete from Demographic where IdentityCode='" + identityCode + "'");
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
