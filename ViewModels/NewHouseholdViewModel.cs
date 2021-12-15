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
    public class NewHouseholdViewModel : Screen
    {
        private LocalPoliceModel currentUser;
        private string householdCode = "";
        private string hostName = "", address = "", note = "";
        private List<string> listVillage;
        private string _selectedVillage;
        private HouseholdViewModel _householdVM;
        public BindableCollection<string> VillageList { get; set; }
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
        public string HostName
        {
            get
            {
                return hostName;
            }
            set
            {
                hostName = value;
                NotifyOfPropertyChange(() => HostName);
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                NotifyOfPropertyChange(() => Address);
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
        public NewHouseholdViewModel(string username, HouseholdViewModel householdVM)
        {
            _householdVM = householdVM;
            currentUser = LocalPoliceAccess.LoadPolice(username);
            GenerateHouseholdCode();
            listVillage = ProvinceInfoAccess.LoadVillageList(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage);
            VillageList = new BindableCollection<string>(listVillage);
        }
        public void Close()
        {
            TryCloseAsync();
        }
        private string GenerateHouseholdCode()
        {
            string provinceCode = currentUser.ProvinceManage;
            string districtCode = currentUser.DistrictManage;
            string wardCode = currentUser.WardManage;
            string addressCode = provinceCode + districtCode + wardCode;
            int i = 0;
            while (i < 10000)
            {
                if (i < 10) householdCode = addressCode + "000" + i;
                else if (i < 100) householdCode = addressCode + "00" + i;
                else if (i < 1000) householdCode = addressCode + "0" + i;
                else householdCode = addressCode + i;
                if (HouseholdAccess.CheckHouseholdCode(householdCode)) i++;
                else break;
            }
            return householdCode;
        }
        private bool CanSave()
        {
            if (hostName == null || address == null || _selectedVillage == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false; 
            }
            return true;
        }
        public void Save()
        {
            string ward = ProvinceInfoAccess.LoadWardName(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage);
            string district = ProvinceInfoAccess.LoadDistrictName(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage);
            string province = ProvinceInfoAccess.LoadProvinceName(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage);
            HouseholdModel household = new HouseholdModel(householdCode, hostName, address, _selectedVillage, ward, district, province, note);
            if (CanSave())
            {
                HouseholdAccess.SaveHousehold(household);
                MessageBox.Show("Đã thêm hộ khẩu thành công!", "Thông báo", MessageBoxButton.OK);
                _householdVM.Search();
                TryCloseAsync();
            }
        }
    }
    
}
