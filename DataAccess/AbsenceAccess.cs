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
    public class AbsenceAccess
    {
        public static List<AbsenceModel> LoadPeople(string text = "", string village = "")
        {
            text = text.ToLower();
            string name = text.ToUpper();
            string query = @"SELECT Absence.Name, Absence.IdentityCode, Absence.BirthDay, Absence.Gender, Absence.PermanentAddress, ShelterAddress, Absence.CurrentAddress, ReasonAbsence, FromDay, ToDay, Destination, Absence.Note
                            FROM Demographic, Absence, Household
                            WHERE (Absence.IdentityCode like '%" + text + "%' or Absence.Name like '%" + name + "%' or Absence.Gender like '%" + text + "%' or Absence.BirthDay like '%" + text + "%' or Absence.PermanentAddress like '%" + text + "%' or FromDay like '%" + text + "%' or ToDay like '%" + text + "%' or ReasonAbsence like '%" + text + "%') " +
                            "and Demographic.IdentityCode = Absence.IdentityCode and Demographic.HouseholdCode = Household.HouseholdCode and Household.Village like '%" + village + "%'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<AbsenceModel>(query, new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SavePerson(AbsenceModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Absence values (@Name, @IdentityCode, @BirthDay, @Gender, @PermanentAddress, @ShelterAddress, @CurrentAddress, @ReasonAbsence, @FromDay, @ToDay, @Destination, @Note)", person);
                cnn.Execute("update Demographic set LivingStatus='Tạm vắng' where IdentityCode='" + person.IdentityCode + "'");
            }
        }
        public static void UpdateDate(string identityCode, string day)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Absence set ToDay='" + day + "' where IdentityCode='" + identityCode + "'");
            }
        }
        public static void UpdatePerson(AbsenceModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Absence set Name=@Name, BirthDay=@BirthDay, Gender=@Gender, PermanentAddress=@PermanentAddress, ShelterAddress=@ShelterAddress, CurrentAddress=@CurrentAddress, FromDay=@FromDay, ToDay=@ToDay, Destination=@Destination, Note=@Note where IdentityCode=@IdentityCode", person);
            }
        }
        public static void DeletePerson(string identityCode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Absence where IdentityCode='"+identityCode+"'");
                cnn.Execute("update Demographic set LivingStatus='Thường trú' where IdentityCode='" + identityCode + "'");
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
