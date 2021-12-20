using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Household_Management_System.ViewModels
{
    public class HouseholdDetailViewModel : Screen
    {
        private string name, identityCode, gender, ethnic, birthPlace, birthDay, job, workPlace, relative, educationLevel, technicalLevel;
        private string _householdCode;
        private DemographicModel _selectedPerson;
        private List<DemographicModel> listMember;
        public BindableCollection<DemographicModel> MemberList { get; set; }
        public DemographicModel SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
                SetInfo();
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
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                NotifyOfPropertyChange(() => Gender);
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
        public HouseholdDetailViewModel(string householdCode)
        {
            _householdCode = householdCode;
            listMember = DemographicAccess.LoadPeople(_householdCode);
            MemberList = new BindableCollection<DemographicModel>(listMember);
        }
        private void SetInfo()
        {
            name = _selectedPerson.Name;
            identityCode = _selectedPerson.IdentityCode;
            birthDay = _selectedPerson.BirthDay;
            birthPlace = _selectedPerson.BirthPlace;
            gender = _selectedPerson.Gender;
            ethnic = _selectedPerson.Ethnic;
            job = _selectedPerson.Job;
            relative = _selectedPerson.Relative;
            workPlace = _selectedPerson.WorkPlace;
            educationLevel = _selectedPerson.EducationLevel;
            technicalLevel = _selectedPerson.TechnicalLevel;
            NotifyOfPropertyChange(() => Name);
            NotifyOfPropertyChange(() => IdentityCode);
            NotifyOfPropertyChange(() => BirthDay);
            NotifyOfPropertyChange(() => Gender);
            NotifyOfPropertyChange(() => BirthPlace);
            NotifyOfPropertyChange(() => Ethnic);
            NotifyOfPropertyChange(() => Job);
            NotifyOfPropertyChange(() => Relative);
            NotifyOfPropertyChange(() => WorkPlace);
            NotifyOfPropertyChange(() => EducationLevel);
            NotifyOfPropertyChange(() => TechnicalLevel);
        }
        public void Transfer()
        {
            if (_selectedPerson == null)
                MessageBox.Show("Vui lòng chọn một nhân khẩu để chuyển khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                MessageBoxResult mr = MessageBox.Show("Bạn có muốn cắt khẩu cho nhân khẩu này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mr == MessageBoxResult.Yes)
                {
                    DemographicAccess.DeletePerson(_selectedPerson.IdentityCode);
                    MessageBox.Show("Đã cắt khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    listMember = DemographicAccess.LoadPeople(_householdCode);
                    MemberList = new BindableCollection<DemographicModel>(listMember);
                    NotifyOfPropertyChange(() => MemberList);
                }
            }
        }
    }
}
