using Dapper;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Household_Management_System.DataAccess
{
    public class MilitaryAccess
    {
        public static List<MilitaryModel> LoadPeople(string village = "", string status = "")
        {
            string query = @"SELECT Military.IdentityCode, Military.Name, Military.Gender, Military.BirthDay, Military.Ethnic, Military.CurrentAddress, Military.Status, Military.Note
                            FROM Military, Household, Demographic 
                            WHERE Military.Status like '%"+status+"%' and Military.IdentityCode = Demographic.IdentityCode and Household.HouseholdCode = Demographic.HouseholdCode and Household.Village like '%" + village + "%'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MilitaryModel>(query, new DynamicParameters());
                return output.ToList();
            }
        }
        public static void UpdateStatus(string identityCode, string status)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Military set Status='" + status + "' where IdentityCode='" + identityCode + "'");
            }
        }
        public static void UpdateMilitaryList()
        {
            List<PersonEnough> list, list2;
            string currentDay = DateTime.Now.ToString("dd/MM/yyyy");
            int pastYear = Int16.Parse(currentDay.Substring(6, 4)) - 27;
            int futureYear = Int16.Parse(currentDay.Substring(6, 4)) - 18;
            string futureDay = currentDay.Substring(0, 6) + futureYear;
            string pastDay = currentDay.Substring(0, 6) + pastYear;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonEnough>("select Name, Gender, IdentityCode, BirthDay, Ethnic, CurrentAddress from Demographic", new DynamicParameters());
                list = output.ToList();
                list2 = output.ToList();
            }
            
            for (int i = list.Count - 1;i >=0; i--)
            {
                DateTime dtBirthDay = DateTime.ParseExact(list[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                list[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
                if (!(DateTime.Parse(pastDay) <= DateTime.Parse(list[i].BirthDay)
                    && DateTime.Parse(list[i].BirthDay) <= DateTime.Parse(futureDay) && list[i].Gender == "Nam"))
                {
                    list.RemoveAt(i);
                }
            }
            
            for (int i = list2.Count - 1; i >= 0; i--)
            {
                if (DateTime.Parse(pastDay) <= DateTime.Parse(list2[i].BirthDay)
                    && DateTime.Parse(list2[i].BirthDay) <= DateTime.Parse(futureDay) && list2[i].Gender == "Nam")
                {
                    list2.RemoveAt(i);
                }          
            }
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Military");
                for (int i = 0; i < list.Count; i++)
                {
                    var output = cnn.Query<string>("select IdentityCode from Military where IdentityCode='" + list[i].IdentityCode + "'");
                    if (output.FirstOrDefault() == null)
                    {
                        cnn.Execute("insert into Military values (@IdentityCode, @Name, @Gender, @BirthDay, @Ethnic, @CurrentAddress, 'Đủ tuổi gia nhập', '')", list[i]);
                    }
                }
                for (int i = 0; i < list2.Count; i++)
                {
                    var output = cnn.Query<string>("select IdentityCode from Military where IdentityCode='" + list2[i].IdentityCode + "'");
                    if (output.FirstOrDefault() != null)
                    {
                        
                        cnn.Execute("delete from Military where IdentityCode = '"+list2[i].IdentityCode+"'");
                    }
                }
                
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
        private class PersonEnough
        {
            private string name, gender, identityCode, birthDay, ethnic, currentAddress;

            public PersonEnough(string name, string gender, string identityCode, string birthDay, string ethnic, string currentAddress)
            {
                this.Name = name;
                this.Gender = gender;
                this.IdentityCode = identityCode;
                this.BirthDay = birthDay;
                this.Ethnic = ethnic;
                this.CurrentAddress = currentAddress;
            }

            public string Name { get => name; set => name = value; }
            public string Gender { get => gender; set => gender = value; }
            public string IdentityCode { get => identityCode; set => identityCode = value; }
            public string BirthDay { get => birthDay; set => birthDay = value; }
            public string Ethnic { get => ethnic; set => ethnic = value; }
            public string CurrentAddress { get => currentAddress; set => currentAddress = value; }
        }
    }
}
