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
    public class DemographicAccess
    {
        public static List<DemographicModel> LoadPeople(string text="", string village="")
        {
            text = text.ToLower();
            string name = text.ToUpper();
            string query = @"SELECT IdentityCode, ICDate, ICPlace, Name, SecondName, Household.HouseholdCode, Gender, BirthDay, Relative, BirthPlace, NativeVillage, Ethnic, Religion, Nationality, CurrentAddress, PermanentAddress, EducationLevel, TechnicalLevel, Job, WorkPlace, MaritalStatus, LivingStatus, Demographic.Note
                            FROM Household, Demographic 
                            WHERE (IdentityCode like '%" + text + "%' or Name like '%" + name + "%' or Gender like '%" + text + "%' or BirthDay like '%" + text + "%' or Ethnic like '%" + text + "%' or LivingStatus like '%" + text + "%' or NativeVillage like '%" + text + "%' or Demographic.HouseholdCode like '%" + text + "%' or Relative like '%" + text + "%') " +
                            "and Household.HouseholdCode = Demographic.HouseholdCode and Household.Village like '%"+village+"%'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DemographicModel>(query, new DynamicParameters());
                return output.ToList();
            }
        }
        public static DemographicModel LoadPerson(string identityCode)
        {
            string query = @"SELECT * FROM Demographic WHERE IdentityCode='"+identityCode+"'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DemographicModel>(query, new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static void SavePerson(DemographicModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Demographic values (@IdentityCode, @ICDate, @ICPlace, @Name, @SecondName, @HouseholdCode, @Gender, @BirthDay, @Relative, @BirthPlace, @NativeVillage, @Ethnic, @Religion, @Nationality, @CurrentAddress, @PermanentAddress, @EducationLevel, @TechnicalLevel, @Job, @WorkPlace, @MaritalStatus, @LivingStatus, @Note)", person);
            }
        }
        public static bool CheckPerson(string identityCode, string name, string birthDay, string gender)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("select Name from Demographic where IdentityCode='"+identityCode+"' and Name='"+name+"' and BirthDay='"+birthDay+"' and Gender='"+gender+"'", new DynamicParameters());
                if (output.FirstOrDefault() == null) return false;
                return true;
            }
        }
        public static void UpdatePerson(string identityCode, DemographicModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Demographic set ICDate = @ICDate, ICPlace = @ICPlace, Name = @Name, SecondName = @SecondName, Gender = @Gender, BirthDay = @BirthDay, Relative = @Relative, BirthPlace = @BirthPlace, NativeVillage = @NativeVillage, Ethnic = @Ethnic, Religion = @Religion, Nationality = @Nationality, CurrentAddress = @CurrentAddress,PermanentAddress = @PermanentAddress, EducationLevel = @EducationLevel, TechnicalLevel = @TechnicalLevel, Job = @Job, WorkPlace = @WorkPlace, MaritalStatus = @MaritalStatus, LivingStatus = @LivingStatus, Note = @Note where IdentityCode = '"+identityCode+"'", person);
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
