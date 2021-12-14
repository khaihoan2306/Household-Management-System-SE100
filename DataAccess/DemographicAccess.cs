﻿using Dapper;
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
            string query = @"SELECT IdentityCode, Name, SecondName, Household.HouseholdCode, Gender, BirthDay, Relative, BirthPlace, NativeVillage, Ethnic, Religion, Nationality, CurrentAddress, EducationLevel, TechnicalLevel, Job, WorkPlace, MaritalStatus, LivingStatus, Demographic.Note
                            FROM Household, Demographic 
                            WHERE (IdentityCode like '%" + text + "%' or Name like '%" + name + "%' or Gender like '%" + text + "%' or BirthDay like '%" + text + "%' or Ethnic like '%" + text + "%' or LivingStatus like '%" + text + "%' or NativeVillage like '%" + text + "%' or Demographic.HouseholdCode like '%" + text + "%' or Relative like '%" + text + "%') " +
                            "and Household.HouseholdCode = Demographic.HouseholdCode and Household.Village like '%"+village+"%'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DemographicModel>(query, new DynamicParameters());
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