using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.ViewModels
{
    public class HouseholdViewModel : Screen
    {
        private List<HouseholdModel> listHousehold;
        private List<string> listVillage;
        private string _selectedVillage;
        private LocalPoliceModel currentUser;
        public BindableCollection<HouseholdModel> HouseholdList { get; set; }
        public BindableCollection<string> VillageList { get; set; }
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
        public HouseholdViewModel(string username)
        {
            currentUser = LocalPoliceAccess.LoadPolice(username)[0];
            listHousehold = HouseholdAccess.LoadHousehold();         
            listVillage = ProvinceInfoAccess.LoadVillageList(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage);
            listVillage.Add("-- Tất cả --");
            HouseholdList = new BindableCollection<HouseholdModel>(listHousehold);
            VillageList = new BindableCollection<string>(listVillage);
        }
    }
}
