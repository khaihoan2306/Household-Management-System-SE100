using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Household_Management_System.ViewModels
{
    public class DemographicViewModel : Screen
    {
        private List<string> listVillage;
        private List<DemographicModel> listPeople;
        private string _selectedVillage, textSearch = "";
        private DemographicModel _selectedPerson;
        private LocalPoliceModel currentUser;
        public BindableCollection<string> VillageList { get; set; }
        public BindableCollection<DemographicModel> DemographicList { get; set; }
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
            }
        }
        public DemographicViewModel(string username)
        {
            currentUser = LocalPoliceAccess.LoadPolice(username)[0];
            listPeople = DemographicAccess.LoadPeople("", "");
            listVillage = new List<string>();
            listVillage.Add("-- Tất cả --");
            listVillage.AddRange(ProvinceInfoAccess.LoadVillageList(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage));
            DemographicList = new BindableCollection<DemographicModel>(listPeople);
            VillageList = new BindableCollection<string>(listVillage);
        }
        public void Search()
        {
            if (_selectedVillage == null)
                listPeople = DemographicAccess.LoadPeople(textSearch, "");
            else if (_selectedVillage.Equals("-- Tất cả --"))
                listPeople = DemographicAccess.LoadPeople(textSearch, "");
            else listPeople = DemographicAccess.LoadPeople(textSearch, _selectedVillage);
            DemographicList = new BindableCollection<DemographicModel>(listPeople);
            NotifyOfPropertyChange(() => DemographicList);
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
