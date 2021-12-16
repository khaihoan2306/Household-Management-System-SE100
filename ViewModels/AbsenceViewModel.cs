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
                MessageBox.Show("Vui lòng chọn một hộ khẩu để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
