using Caliburn.Micro;
using Household_Management_System.DataAccess;
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
    public class NewDemographicViewModel : Screen
    {
        private string identityCode, iCDate, iCPlace, name, secondName="", householdCode, birthDay, relative, birthPlace, nativeVillage, ethnic, religion, nationality, currentAddress, permanentAddress, educationLevel, technicalLevel, job, workPlace, livingStatus, note;
        private List<string> listGender, listMarital;
        private string _selectedGender, _selectedMarital;
     
        public BindableCollection<string> Gender { get; set; }
        public BindableCollection<string> Marital { get; set; }
        public string IdentityCode
        {
            get
            {
                return identityCode;
            }
            set
            {
                identityCode = value;
                NotifyOfPropertyChange(() => IdentityCode);
            }
        }
        public string ICDate
        {
            get
            {
                return iCDate;
            }
            set
            {
                iCDate = value;
                NotifyOfPropertyChange(() => ICDate);
            }
        }
        public string ICPlace
        {
            get
            {
                return iCPlace;
            }
            set
            {
                iCPlace = value;
                NotifyOfPropertyChange(() => ICPlace);
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public string SecondName
        {
            get
            {
                return secondName;
            }
            set
            {
                secondName = value;
                NotifyOfPropertyChange(() => SecondName);
            }
        }
        public string HouseholdCode
        {
            get
            {
                return householdCode;
            }
            set
            {
                householdCode = value;
                NotifyOfPropertyChange(() => HouseholdCode);
            }
        }
        public string BirthDay
        {
            get
            {
                return birthDay; 
            }
            set
            {
                birthDay = value;
                NotifyOfPropertyChange(() => BirthDay);
            }
        }
        public string Relative
        {
            get
            {
                return relative;
            }
            set
            {
                relative = value;
                NotifyOfPropertyChange(() => Relative);
            }
        }
        public string BirthPlace
        {
            get
            {
                return birthPlace;
            }
            set
            {
                birthPlace = value;
                NotifyOfPropertyChange(() => BirthPlace);
            }
        }
        public string NativeVillage
        {
            get
            {
                return nativeVillage;
            }
            set
            {
                nativeVillage = value;
                NotifyOfPropertyChange(() => NativeVillage);
            }
        }
        public string Ethnic
        {
            get
            {
                return ethnic;
            }
            set
            {
                ethnic = value;
                NotifyOfPropertyChange(() => Ethnic);
            }
        }
        public string Religion
        {
            get
            {
                return religion;
            }
            set
            {
                religion = value;
                NotifyOfPropertyChange(() => Religion);
            }
        }
        public string Nationality
        {
            get
            {
                return nationality;
            }
            set
            {
                nationality = value;
                NotifyOfPropertyChange(() => Nationality);
            }
        }
        public string CurrentAddress
        {
            get
            {
                return currentAddress;
            }
            set
            {
                currentAddress = value;
                NotifyOfPropertyChange(() => CurrentAddress);
            }
        }
        public string PermanentAddress
        {
            get
            {
                return permanentAddress;
            }
            set
            {
                permanentAddress = value;
                NotifyOfPropertyChange(() => PermanentAddress);
            }
        }
        public string EducationLevel
        {
            get
            {
                return educationLevel;
            }
            set
            {
                educationLevel = value;
                NotifyOfPropertyChange(() => EducationLevel);
            }
        }
        public string TechnicalLevel
        {
            get
            {
                return technicalLevel;
            }
            set
            {
                technicalLevel = value;
                NotifyOfPropertyChange(() => TechnicalLevel);
            }
        }
        public string Job
        {
            get
            {
                return job;
            }
            set
            {
                job = value;
                NotifyOfPropertyChange(() => Job);
            }
        }
        public string WorkPlace
        {
            get
            {
                return workPlace;
            }
            set
            {
                workPlace = value;
                NotifyOfPropertyChange(() => WorkPlace);
            }
        }
        public string LivingStatus
        {
            get
            {
                return livingStatus;
            }
            set
            {
                livingStatus = value;
                NotifyOfPropertyChange(() => LivingStatus);
            }
        }
        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
                NotifyOfPropertyChange(() => Note);
            }
        }
        public string SelectedGender
        {
            get
            {
                return _selectedGender;
            }
            set
            {
                _selectedGender = value;
                NotifyOfPropertyChange(() => SelectedGender);
            }
        }
        public string SelectedMarital
        {
            get
            {
                return _selectedMarital;
            }
            set
            {
                _selectedMarital = value;
                NotifyOfPropertyChange(() => SelectedMarital);
            }
        }
        public NewDemographicViewModel(int code = 0)
        {
            if (code == 0) livingStatus = "Thường trú";
            else if (code == 1) livingStatus = "Tạm trú";
            AddListCombobox();
        }
        private void AddListCombobox()
        {
            listGender = new List<string>();
            listGender.Add("Nam");
            listGender.Add("Nữ");
            listMarital = new List<string>();
            listMarital.Add("Độc thân");
            listMarital.Add("Đã kết hôn");
            listMarital.Add("Đã ly hôn");
            Gender = new BindableCollection<string>(listGender);
            Marital = new BindableCollection<string>(listMarital);
        }
        private bool CanSave()
        {
            if (name == null || householdCode == null || relative == null || birthDay == null || _selectedGender == null || birthPlace == null || nativeVillage == null
                || ethnic == null || nationality == null || religion == null || identityCode == null || iCDate == null || iCPlace == null || educationLevel == null
                || job == null || workPlace == null || permanentAddress == null || currentAddress == null || _selectedMarital == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
        public void Save()
        {
            if (CanSave()) 
            {
                DateTime dtBirthDay = DateTime.ParseExact(birthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime dtICDate = DateTime.ParseExact(iCDate, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                DemographicModel person = new DemographicModel(identityCode, dtICDate.ToString("dd/MM/yyyy"), iCPlace, name, secondName, householdCode, _selectedGender, dtBirthDay.ToString("dd/MM/yyyy"), relative, birthPlace, nativeVillage, ethnic, religion, nationality, currentAddress, permanentAddress, educationLevel, technicalLevel, job, workPlace, _selectedMarital, livingStatus, note);
                DemographicAccess.SavePerson(person);
                MessageBox.Show("Đã thêm nhân khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                TryCloseAsync();
            }
        }
        public void Close()
        {
            TryCloseAsync();
        }
    }
}
