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
using System.IO;

namespace Household_Management_System.DataAccess
{
    public class ReportAccess
    {  
        private static List<DemographicModel> ListDemographic()
        {
            string query = @"SELECT * FROM Demographic";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DemographicModel>(query, new DynamicParameters());
                return output.ToList();
            }
        }
        private static List<HouseholdModel> ListHousehold()
        {
            string query = @"SELECT * FROM Household";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<HouseholdModel>(query, new DynamicParameters());
                return output.ToList();
            }
        }
        private static List<AbsenceModel> ListAbsence()
        {
            string query = @"SELECT * FROM Absence";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<AbsenceModel>(query, new DynamicParameters());
                return output.ToList();
            }
        }
        public static int TotalDemographic(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated)
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].LivingStatus != "Tạm vắng")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus != "Tạm vắng")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int TotalHousehold(string from = "", string to = "")
        {
            List<HouseholdModel> list = ListHousehold();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated)
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to))
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    count++;
                }
            }
            return count;
        }
        public static int TotalMale(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated)
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].Gender == "Nam" && list[i].LivingStatus != "Tạm vắng")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Gender == "Nam" && list[i].LivingStatus != "Tạm vắng")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int TotalFemale(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated)
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].Gender == "Nữ" && list[i].LivingStatus != "Tạm vắng")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Gender == "Nữ" && list[i].LivingStatus != "Tạm vắng")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int PermanentHousehold(string from = "", string to = "")
        {
            string query = @"SELECT COUNT(DISTINCT HouseholdCode) FROM Demographic WHERE LivingStatus='Thường trú'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<int>(query, new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static int PermanentDemographic(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated)
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].LivingStatus == "Thường trú")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus == "Thường trú")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int PermanentMale(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && list[i].LivingStatus == "Thường trú"
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].Gender == "Nam")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus == "Thường trú" && list[i].Gender == "Nam")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int PermanentFemale(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && list[i].LivingStatus == "Thường trú"
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].Gender == "Nữ")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus == "Thường trú" && list[i].Gender == "Nữ")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int Permanent14(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            string currentDay = DateTime.Now.ToString("dd/MM/yyyy");
            int enough14Year = Int16.Parse(currentDay.Substring(6, 4)) - 14;
            string enough14Day = currentDay.Substring(0, 6) + enough14Year;
          
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    DateTime dtBirthDay = DateTime.ParseExact(list[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    list[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && DateTime.Parse(list[i].BirthDay) <= DateTime.Parse(enough14Day)
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].LivingStatus == "Thường trú")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    DateTime dtBirthDay = DateTime.ParseExact(list[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    list[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
                    if (DateTime.Parse(list[i].BirthDay) <= DateTime.Parse(enough14Day) && list[i].LivingStatus == "Thường trú")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int TemporaryHousehold(string from = "", string to = "")
        {
            string query = @"SELECT COUNT(DISTINCT HouseholdCode) FROM Demographic WHERE LivingStatus='Tạm trú'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<int>(query, new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static int TemporaryDemographic(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && list[i].LivingStatus == "Tạm trú"
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to))
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus == "Tạm trú")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int TemporaryMale(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && list[i].LivingStatus == "Tạm trú"
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].Gender == "Nam")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus == "Tạm trú" && list[i].Gender == "Nam")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int TemporaryFemale(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && list[i].LivingStatus == "Tạm trú"
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].Gender == "Nữ")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus == "Tạm trú" && list[i].Gender == "Nữ")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int Temporary14(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            string currentDay = DateTime.Now.ToString("dd/MM/yyyy");
            int enough14Year = Int16.Parse(currentDay.Substring(6, 4)) - 14;
            string enough14Day = currentDay.Substring(0, 6) + enough14Year;

            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    DateTime dtBirthDay = DateTime.ParseExact(list[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    list[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && DateTime.Parse(list[i].BirthDay) <= DateTime.Parse(enough14Day)
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].LivingStatus == "Tạm trú")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    DateTime dtBirthDay = DateTime.ParseExact(list[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    list[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
                    if (DateTime.Parse(list[i].BirthDay) <= DateTime.Parse(enough14Day) && list[i].LivingStatus == "Tạm trú")
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public static int AbsenceHousehold(string from = "", string to = "")
        {
            string query = @"SELECT COUNT(DISTINCT HouseholdCode) FROM Demographic WHERE LivingStatus='Tạm vắng'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<int>(query, new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
        public static int AbsenceDemographic(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && list[i].LivingStatus == "Tạm vắng"
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to))
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus == "Tạm vắng")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int AbsenceMale(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && list[i].LivingStatus == "Tạm vắng"
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].Gender == "Nam")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus == "Tạm vắng" && list[i].Gender == "Nam")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int AbsenceFemale(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && list[i].LivingStatus == "Tạm vắng"
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].Gender == "Nữ")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].LivingStatus == "Tạm vắng" && list[i].Gender == "Nữ")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int Absence14(string from = "", string to = "")
        {
            List<DemographicModel> list = ListDemographic();
            string currentDay = DateTime.Now.ToString("dd/MM/yyyy");
            int enough14Year = Int16.Parse(currentDay.Substring(6, 4)) - 14;
            string enough14Day = currentDay.Substring(0, 6) + enough14Year;

            int count = 0;
            if (from != "" && to != "")
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    DateTime dtBirthDay = DateTime.ParseExact(list[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    list[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
                    if (DateTime.Parse(from) <= DateTime.Parse(list[i].DateCreated) && DateTime.Parse(list[i].BirthDay) <= DateTime.Parse(enough14Day)
                    && DateTime.Parse(list[i].DateCreated) <= DateTime.Parse(to) && list[i].LivingStatus == "Tạm vắng")
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    DateTime dtBirthDay = DateTime.ParseExact(list[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    list[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
                    if (DateTime.Parse(list[i].BirthDay) <= DateTime.Parse(enough14Day) && list[i].LivingStatus == "Tạm vắng")
                    {
                        count++;
                    }
                }
            }
            return count;
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
