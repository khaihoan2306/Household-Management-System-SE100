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
using System.Windows.Input;

namespace Household_Management_System.ViewModels
{
    public class AbsenceViewModel : Screen
    {
        private List<string> listVillage;
        private List<AbsenceModel> listPeople;
        private string _selectedVillage, textSearch = "";
        private AbsenceModel _selectedPerson;
        private LocalPoliceModel currentUser;
        public BindableCollection<string> VillageList { get; set; }
        public BindableCollection<AbsenceModel> AbsenceList { get; set; }
        public string SelectedVillage
        {
            get
            {
                return _selectedVillage;
            }
            set
            {
                _selectedVillage = value;
                NotifyOfPropertyChange(() => SelectedVillage);
            }
        }
        public string TextSearch
        {
            get
            {
                return textSearch;
            }
            set
            {
                textSearch = value;
                NotifyOfPropertyChange(() => TextSearch);
            }
        }
        public AbsenceModel SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
            }
        }
        public AbsenceViewModel(string username)
        {
            currentUser = LocalPoliceAccess.LoadPolice(username);
            listPeople = AbsenceAccess.LoadPeople();
            for (int i = 0; i < listPeople.Count; i++)
            {
                DateTime dtBirthDay = DateTime.ParseExact(listPeople[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                listPeople[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
            }
            listVillage = new List<string>();
            listVillage.Add("-- Tất cả --");
            listVillage.AddRange(ProvinceInfoAccess.LoadVillageList(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage));
            AbsenceList = new BindableCollection<AbsenceModel>(listPeople);
            VillageList = new BindableCollection<string>(listVillage);
        }
        public void Search()
        {
            if (_selectedVillage == null)
                listPeople = AbsenceAccess.LoadPeople(textSearch, "");
            else if (_selectedVillage.Equals("-- Tất cả --"))
                listPeople = AbsenceAccess.LoadPeople(textSearch, "");
            else listPeople = AbsenceAccess.LoadPeople(textSearch, _selectedVillage);
            for (int i = 0; i < listPeople.Count; i++)
            {
                DateTime dtBirthDay = DateTime.ParseExact(listPeople[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                listPeople[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
            }
            AbsenceList = new BindableCollection<AbsenceModel>(listPeople);
            NotifyOfPropertyChange(() => AbsenceList);
        }
        public void NewAbsence()
        {
            IWindowManager manager = new WindowManager();
            manager.ShowWindowAsync(new NewAbsenceViewModel(), null, null);
        }
        public void Delete()
        {
            if (SelectedPerson == null)
                MessageBox.Show("Vui lòng chọn một nhân khẩu để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                MessageBoxResult mr = MessageBox.Show("Bạn có muốn xóa nhân khẩu tạm vắng này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mr == MessageBoxResult.Yes)
                {
                    AbsenceAccess.DeletePerson(_selectedPerson.IdentityCode);
                    Search();
                }
            }
        }
        public void ExecuteFilterView(KeyEventArgs keyArgs)
        {
            if (keyArgs.Key == Key.Enter)
            {
                Search();
            }
        }
        public void Extend()
        {
            if (_selectedPerson == null)
                MessageBox.Show("Vui lòng chọn một nhân khẩu để gia hạn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                MessageBoxResult mr = MessageBox.Show("Bạn có muốn gia hạn cho người này thêm 2 năm?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mr == MessageBoxResult.Yes)
                {
                    DateTime extendDay = DateTime.Now.AddDays(730);
                    AbsenceAccess.UpdateDate(_selectedPerson.IdentityCode, extendDay.ToString("dd/MM/yyyy"));
                }
            }
        }
        public void ViewDetail()
        {
            if (_selectedPerson == null)
                MessageBox.Show("Vui lòng chọn một đối tượng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                IWindowManager manager = new WindowManager();
                manager.ShowWindowAsync(new NewAbsenceViewModel(_selectedPerson.IdentityCode), null, null);
            }
        }
    }
}
