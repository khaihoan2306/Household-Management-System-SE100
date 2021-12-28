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
    public class NewAbsenceViewModel : Screen
    {
        private string name, identityCode, birthDay, permanentAddress, shelterAddress, currentAddress, reasonAbsence, fromDay, toDay, destination, note, title;
        private List<string> listGender;
        private string _selectedGender, _code;

        public BindableCollection<string> Gender { get; set; }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                NotifyOfPropertyChange(() => Title);
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
        public string ShelterAddress
        {
            get
            {
                return shelterAddress;
            }
            set
            {
                shelterAddress = value;
                NotifyOfPropertyChange(() => ShelterAddress);
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
        public string ReasonAbsence
        {
            get
            {
                return reasonAbsence;
            }
            set
            {
                reasonAbsence = value;
                NotifyOfPropertyChange(() => ReasonAbsence);
            }
        }
        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = value;
                NotifyOfPropertyChange(() => Destination);
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
        public NewAbsenceViewModel(string code="")
        {
            _code = code;
            if (_code == "") title = "Thêm mới nhân khẩu tạm vắng";
            else title = "Thông tin tạm vắng";
            listGender = new List<string>();
            listGender.Add("Nam");
            listGender.Add("Nữ");
            Gender = new BindableCollection<string>(listGender);
            if (_code != "")
            {
                SetInfo(); 
            }
        }
        private void SetInfo()
        {
            AbsenceModel a = AbsenceAccess.LoadPeople(_code, "")[0];
            name = a.Name;
            identityCode = a.IdentityCode;
            birthDay = a.BirthDay;
            permanentAddress = a.PermanentAddress;
            shelterAddress = a.ShelterAddress;
            currentAddress = a.CurrentAddress;
            reasonAbsence = a.ReasonAbsence;
            fromDay = a.FromDay;
            toDay = a.ToDay;
            destination = a.Destination;
            note = a.Note;
        }
        public void Close()
        {
            TryCloseAsync();
        }
        private bool CanSave()
        {
            if (name == null || fromDay == null || permanentAddress == null || birthDay == null || _selectedGender == null || shelterAddress == null || currentAddress == null
                || reasonAbsence == null || toDay == null || identityCode == null || destination == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
        public void Save()
        {
            if (_code == "")
            {
                if (CanSave())
                {
                    if (DemographicAccess.CheckPerson(identityCode, name, birthDay, _selectedGender))
                    {
                        AbsenceModel person = new AbsenceModel(name, identityCode, birthDay, _selectedGender, permanentAddress, shelterAddress, currentAddress, reasonAbsence, fromDay, toDay, destination, note);
                        AbsenceAccess.SavePerson(person);
                        MessageBox.Show("Đã thêm nhân khẩu tạm vắng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        TryCloseAsync();
                    }
                    else
                    {
                        MessageBox.Show("Thông tin nhân khẩu không khớp với cơ sở dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                AbsenceModel a = new AbsenceModel(name, identityCode, birthDay, _selectedGender, permanentAddress, shelterAddress, currentAddress, reasonAbsence, fromDay, toDay, destination, note);
                AbsenceAccess.UpdatePerson(a);
                TryCloseAsync();
            }
        }
    }
    
}
