using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Helpers;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Household_Management_System.ViewModels
{
    public class ReportViewModel : Screen
    {
        private string totalMale, totalHousehold, totalDemographic, totalFemale, permanentHousehold, permanentDemographic, permanentMale, permanentFemale, permanent14, temporaryHousehold, temporaryDemographic, temporaryMale, temporaryFemale, temporary14, absenceHousehold, absenceDemographic, absenceMale, absenceFemale, absence14;
        private string toDay, fromDay, timeRange, provinceName, districtName;
        private LocalPoliceModel currentUser;
        public string ProvinceName
        {
            get
            {
                return provinceName;
            }
            set
            {
                provinceName = value;
                NotifyOfPropertyChange(() => ProvinceName);
            }
        }
        public string DistrictName  
        {
            get
            {
                return districtName;
            }
            set
            {
                districtName = value;
                NotifyOfPropertyChange(() => DistrictName);
            }
        }
        public string TimeRange
        {
            get
            {
                return timeRange;
            }
            set
            {
                timeRange = value;
                NotifyOfPropertyChange(() => TimeRange);
            }
        }
        public string ToDay
        {
            get
            {
                return toDay;
            }
            set
            {
                toDay = value;
                NotifyOfPropertyChange(() => ToDay);
            }
        }
        public string FromDay   
        {
            get
            {
                return fromDay;
            }
            set
            {
                fromDay = value;
                NotifyOfPropertyChange(() => FromDay);
            }
        }
        public string TotalMale
        {
            get
            {
                return totalMale;
            }
            set
            {
                totalMale = value;
                NotifyOfPropertyChange(() => TotalMale);
            }
        }
        public string TotalFemale
        {
            get
            {
                return totalFemale;
            }
            set
            {
                totalFemale = value;
                NotifyOfPropertyChange(() => TotalFemale);
            }
        }
        public string TotalHousehold
        {
            get
            {
                return totalHousehold;
            }
            set
            {
                totalHousehold = value;
                NotifyOfPropertyChange(() => TotalHousehold);
            }
        }
        public string TotalDemographic
        {
            get
            {
                return totalDemographic;
            }
            set
            {
                totalDemographic = value;
                NotifyOfPropertyChange(() => TotalDemographic);
            }
        }
        public string PermanentHousehold
        {
            get
            {
                return permanentHousehold;
            }
            set
            {
                permanentHousehold = value;
                NotifyOfPropertyChange(() => PermanentHousehold);
            }
        }
        public string PermanentDemographic
        {
            get
            {
                return permanentDemographic;
            }
            set
            {
                permanentDemographic = value;
                NotifyOfPropertyChange(() => PermanentDemographic);
            }
        }
        public string PermanentMale
        {
            get
            {
                return permanentMale;
            }
            set
            {
                permanentMale = value;
                NotifyOfPropertyChange(() => PermanentMale);
            }
        }
        public string PermanentFemale
        {
            get
            {
                return permanentFemale;
            }
            set
            {
                permanentFemale = value;
                NotifyOfPropertyChange(() => PermanentFemale);
            }
        }
        public string Permanent14
        {
            get
            {
                return permanent14;
            }
            set
            {
                permanent14 = value;
                NotifyOfPropertyChange(() => Permanent14);
            }
        }
        public string TemporaryHousehold
        {
            get
            {
                return temporaryHousehold;
            }
            set
            {
                temporaryHousehold = value;
                NotifyOfPropertyChange(() => TemporaryHousehold);
            }
        }
        public string TemporaryDemographic
        {
            get
            {
                return temporaryDemographic;
            }
            set
            {
                temporaryDemographic = value;
                NotifyOfPropertyChange(() => TemporaryDemographic);
            }
        }
        public string TemporaryMale
        {
            get
            {
                return temporaryMale;
            }
            set
            {
                temporaryMale = value;
                NotifyOfPropertyChange(() => TemporaryMale);
            }
        }
        public string TemporaryFemale
        {
            get
            {
                return temporaryFemale;
            }
            set
            {
                temporaryFemale = value;
                NotifyOfPropertyChange(() => temporaryFemale);
            }
        }
        public string Temporary14
        {
            get
            {
                return temporary14;
            }
            set
            {
                temporary14 = value;
                NotifyOfPropertyChange(() => Temporary14);
            }
        }
        public string AbsenceHousehold
        {
            get
            {
                return absenceHousehold;
            }
            set
            {
                absenceHousehold = value;
                NotifyOfPropertyChange(() => AbsenceHousehold);
            }
        }
        public string AbsenceDemographic
        {
            get
            {
                return absenceDemographic;
            }
            set
            {
                absenceDemographic = value;
                NotifyOfPropertyChange(() => AbsenceDemographic);
            }
        }
        public string AbsenceMale
        {
            get
            {
                return absenceMale;
            }
            set
            {
                absenceMale = value;
                NotifyOfPropertyChange(() => AbsenceMale);
            }
        }
        public string AbsenceFemale
        {
            get
            {
                return absenceFemale;
            }
            set
            {
                absenceFemale = value;
                NotifyOfPropertyChange(() => AbsenceFemale);
            }
        }
        public string Absence14
        {
            get
            {
                return absence14;
            }
            set
            {
                absence14 = value;
                NotifyOfPropertyChange(() => Absence14);
            }
        }
        public ReportViewModel(string username)
        {
            currentUser = LocalPoliceAccess.LoadPolice(username);
            provinceName = "CÔNG AN " + ProvinceInfoAccess.LoadProvinceName(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage).ToUpper();
            districtName = "CÔNG AN " + ProvinceInfoAccess.LoadDistrictName(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage).ToUpper();
            NotifyOfPropertyChange(() => ProvinceName);
            NotifyOfPropertyChange(() => DistrictName);
            timeRange = "(Từ trước đến nay)";
            totalDemographic = ReportAccess.TotalDemographic().ToString();
            totalHousehold = ReportAccess.TotalHousehold().ToString();
            totalMale = ReportAccess.TotalMale().ToString();
            totalFemale = ReportAccess.TotalFemale().ToString();

            permanentDemographic = ReportAccess.PermanentDemographic().ToString();
            permanentHousehold = ReportAccess.PermanentHousehold().ToString();
            permanentMale = ReportAccess.PermanentMale().ToString();
            permanentFemale = ReportAccess.PermanentFemale().ToString();
            permanent14 = ReportAccess.Permanent14().ToString();

            temporaryDemographic = ReportAccess.TemporaryDemographic().ToString();
            temporaryHousehold = ReportAccess.TemporaryHousehold().ToString();
            temporaryMale = ReportAccess.TemporaryMale().ToString();
            temporaryFemale = ReportAccess.TemporaryFemale().ToString();
            temporary14 = ReportAccess.Temporary14().ToString();

            absenceDemographic = ReportAccess.AbsenceDemographic().ToString();
            absenceHousehold = ReportAccess.AbsenceHousehold().ToString();
            absenceMale = ReportAccess.AbsenceMale().ToString();
            absenceFemale = ReportAccess.AbsenceFemale().ToString();
            absence14 = ReportAccess.Absence14().ToString();
        }
        public void Report()
        {
            if (toDay != null && fromDay != null)
            {
                DateTime dtFromDay = DateTime.ParseExact(fromDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime dtToDay = DateTime.ParseExact(toDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                timeRange = "(Từ ngày " + dtFromDay.ToString("dd/MM/yyyy") + " đến ngày " + dtToDay.ToString("dd/MM/yyyy") + ")";
                NotifyOfPropertyChange(() => TimeRange);
                Search(dtFromDay.ToString("dd/MM/yyyy"), dtToDay.ToString("dd/MM/yyyy"));
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ngày hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Search(string from, string to)
        {
            if (DateTime.Parse(from) <= DateTime.Parse(to) && from != "" && to != null)
            {
                totalDemographic = ReportAccess.TotalDemographic(from, to).ToString();
                totalHousehold = ReportAccess.TotalHousehold(from, to).ToString();
                totalMale = ReportAccess.TotalMale(from, to).ToString();
                totalFemale = ReportAccess.TotalFemale(from, to).ToString();

                permanentDemographic = ReportAccess.PermanentDemographic(from, to).ToString();
                permanentHousehold = ReportAccess.PermanentHousehold(from, to).ToString();
                permanentMale = ReportAccess.PermanentMale(from, to).ToString();
                permanentFemale = ReportAccess.PermanentFemale(from, to).ToString();
                permanent14 = ReportAccess.Permanent14(from, to).ToString();

                temporaryDemographic = ReportAccess.TemporaryDemographic(from, to).ToString();
                temporaryHousehold = ReportAccess.TemporaryHousehold(from, to).ToString();
                temporaryMale = ReportAccess.TemporaryMale(from, to).ToString();
                temporaryFemale = ReportAccess.TemporaryFemale(from, to).ToString();
                temporary14 = ReportAccess.Temporary14(from, to).ToString();

                absenceDemographic = ReportAccess.AbsenceDemographic(from, to).ToString();
                absenceHousehold = ReportAccess.AbsenceHousehold(from, to).ToString();
                absenceMale = ReportAccess.AbsenceMale(from, to).ToString();
                absenceFemale = ReportAccess.AbsenceFemale(from, to).ToString();
                absence14 = ReportAccess.Absence14(from, to).ToString();
                NotifyOfPropertyChange(() => TotalHousehold);
                NotifyOfPropertyChange(() => TotalDemographic);
                NotifyOfPropertyChange(() => TotalFemale);
                NotifyOfPropertyChange(() => TotalMale);
                NotifyOfPropertyChange(() => PermanentDemographic);
                NotifyOfPropertyChange(() => PermanentHousehold);
                NotifyOfPropertyChange(() => PermanentMale);
                NotifyOfPropertyChange(() => PermanentFemale);
                NotifyOfPropertyChange(() => Permanent14);
                NotifyOfPropertyChange(() => AbsenceDemographic);
                NotifyOfPropertyChange(() => AbsenceHousehold);
                NotifyOfPropertyChange(() => AbsenceMale);
                NotifyOfPropertyChange(() => AbsenceFemale);
                NotifyOfPropertyChange(() => Absence14);
                NotifyOfPropertyChange(() => AbsenceDemographic);
                NotifyOfPropertyChange(() => AbsenceHousehold);
                NotifyOfPropertyChange(() => AbsenceMale);
                NotifyOfPropertyChange(() => AbsenceFemale);
                NotifyOfPropertyChange(() => Absence14);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khoảng thời gian hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
                
        }
        public void Export()
        {
            ExportToPDFHelper.ExportToPDF();
        }
    }
}
