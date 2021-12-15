using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Household_Management_System.ViewModels
{
    public class HouseholdViewModel : Screen
    {
        private List<HouseholdModel> listHousehold;
        private List<string> listVillage;
        private string _selectedVillage, textSearch = "";
        private HouseholdModel _selectedHousehold;
        private LocalPoliceModel currentUser;
        public BindableCollection<HouseholdModel> HouseholdList { get; set; }
        public BindableCollection<string> VillageList { get; set; }
        public HouseholdModel SelectedHousehold
        {
            get
            {
                return _selectedHousehold;
            }
            set
            {
                _selectedHousehold = value;
                NotifyOfPropertyChange(() => SelectedHousehold);
            }
        }
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
        public HouseholdViewModel(string username)
        {
            currentUser = LocalPoliceAccess.LoadPolice(username);
            listHousehold = HouseholdAccess.LoadHousehold();
            listVillage = new List<string>();
            listVillage.Add("-- Tất cả --");
            listVillage.AddRange(ProvinceInfoAccess.LoadVillageList(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage));
            HouseholdList = new BindableCollection<HouseholdModel>(listHousehold);
            VillageList = new BindableCollection<string>(listVillage);
           
        }
        public void CreateNewHousehold()
        {
            IWindowManager manager = new WindowManager();
            manager.ShowWindowAsync(new NewHouseholdViewModel(currentUser.Username, this), null, null);
        }
        public void Search()
        {
            if (_selectedVillage == null) 
                listHousehold = HouseholdAccess.LoadHousehold("", textSearch);
            else if (_selectedVillage.Equals("-- Tất cả --"))
                    listHousehold = HouseholdAccess.LoadHousehold("", textSearch);
                 else listHousehold = HouseholdAccess.LoadHousehold(_selectedVillage, textSearch);
            HouseholdList = new BindableCollection<HouseholdModel>(listHousehold);
            NotifyOfPropertyChange(() => HouseholdList);
        }
        public void Delete()
        {
            if (SelectedHousehold == null) 
                MessageBox.Show("Vui lòng chọn một hộ khẩu để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (HouseholdDetailAccess.CheckMemberHousehold(_selectedHousehold.HouseholdCode))
                    MessageBox.Show("Vui lòng cắt khẩu hết thành viên trong hộ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    MessageBoxResult mr = MessageBox.Show("Bạn có muốn xóa hộ khẩu này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (mr == MessageBoxResult.Yes)
                    {
                        HouseholdAccess.DeleteHousehold(_selectedHousehold.HouseholdCode);
                        Search();
                    }
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
