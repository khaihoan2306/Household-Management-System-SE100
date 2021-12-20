using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Household_Management_System.ViewModels
{
    public class ResidenceViewModel : Screen
    {
        private List<string> listVillage;
        private List<ResidenceModel> listPeople;
        private string _selectedVillage, textSearch = "";
        private ResidenceModel _selectedPerson;
        private LocalPoliceModel currentUser;
        public BindableCollection<string> VillageList { get; set; }
        public BindableCollection<ResidenceModel> ResidenceList { get; set; }
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
        public ResidenceModel SelectedPerson
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
        public ResidenceViewModel(string username)
        {
            currentUser = LocalPoliceAccess.LoadPolice(username);
            listPeople = ResidenceAccess.LoadPeople();
            listVillage = new List<string>();
            listVillage.Add("-- Tất cả --");
            listVillage.AddRange(ProvinceInfoAccess.LoadVillageList(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage));
            ResidenceList = new BindableCollection<ResidenceModel>(listPeople);
            VillageList = new BindableCollection<string>(listVillage);
        }
        public void Search()
        {
            if (_selectedVillage == null)
                listPeople = ResidenceAccess.LoadPeople(textSearch, "");
            else if (_selectedVillage.Equals("-- Tất cả --"))
                listPeople = ResidenceAccess.LoadPeople(textSearch, "");
            else listPeople = ResidenceAccess.LoadPeople(textSearch, _selectedVillage);
            ResidenceList = new BindableCollection<ResidenceModel>(listPeople);
            NotifyOfPropertyChange(() => ResidenceList);
        }
        public void CreateNewResidence()
        {
            IWindowManager manager = new WindowManager();
            manager.ShowWindowAsync(new NewDemographicViewModel(1), null, null);
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
                    ResidenceAccess.DeletePerson(_selectedPerson.IdentityCode);
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
    }
}
