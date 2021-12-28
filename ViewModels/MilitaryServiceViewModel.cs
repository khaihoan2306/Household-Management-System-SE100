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
    public class MilitaryServiceViewModel : Screen
    {
        private List<string> listVillage, listFilter;
        private List<MilitaryModel> listPeople;
        private string _selectedVillage="", _selectedFilter="Đủ tuổi gia nhập", textSearch = "";
        private MilitaryModel _selectedPerson;
        private LocalPoliceModel currentUser;
        public BindableCollection<MilitaryModel> MilitaryList { get; set; }
        public BindableCollection<string> VillageList { get; set; }
        public BindableCollection<string> FilterList { get; set; }
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
        public string SelectedFilter
        {
            get
            {
                return _selectedFilter;
            }
            set
            {
                _selectedFilter = value;
                NotifyOfPropertyChange(() => SelectedFilter);
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
        public MilitaryModel SelectedPerson
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
        public MilitaryServiceViewModel(string username)
        {
            currentUser = LocalPoliceAccess.LoadPolice(username);
            MilitaryAccess.UpdateMilitaryList();
            listVillage = new List<string>();
            listVillage.Add("-- Tất cả --");
            listVillage.AddRange(ProvinceInfoAccess.LoadVillageList(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage));
            listFilter = new List<String>();
            listFilter.Add("Đủ tuổi gia nhập");
            listFilter.Add("Đủ điều kiện nhập ngũ");
            listFilter.Add("Tạm hoãn nghĩa vụ");
            listFilter.Add("Miễn nghĩa vụ");
            FilterList = new BindableCollection<string>(listFilter);
            VillageList = new BindableCollection<string>(listVillage);
            Filter();
        }
        public void Filter()
        {
            if (_selectedVillage == null)
            {
                listPeople = MilitaryAccess.LoadPeople("", _selectedFilter);
            }
            else if (_selectedVillage.Equals("-- Tất cả --"))
                listPeople = MilitaryAccess.LoadPeople("", _selectedFilter);
            else listPeople = MilitaryAccess.LoadPeople(_selectedVillage, _selectedFilter);
            
            MilitaryList = new BindableCollection<MilitaryModel>(listPeople);
            NotifyOfPropertyChange(() => MilitaryList);
        }
        public void UpdateStatus()
        {
            if (_selectedPerson == null)
            {
                MessageBox.Show("Vui lòng chọn một đối tượng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                IWindowManager manager = new WindowManager();
                manager.ShowWindowAsync(new UpdateMilitaryViewModel(_selectedPerson.IdentityCode, this), null, null);
            }
        }
    }
}
