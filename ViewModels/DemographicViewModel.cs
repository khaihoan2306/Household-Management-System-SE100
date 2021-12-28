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
    public class DemographicViewModel : Screen
    {
        private List<string> listVillage;
        private List<DemographicModel> listPeople;
        private string _selectedVillage, textSearch = "";
        private DemographicModel _selectedPerson;
        private LocalPoliceModel currentUser;
        private ShellViewModel _shell;
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
        public DemographicViewModel(string username, ShellViewModel shell)
        {
            _shell = shell;
            currentUser = LocalPoliceAccess.LoadPolice(username);
            listPeople = DemographicAccess.LoadPeople("", "");
            for (int i = 0; i < listPeople.Count; i++)
            {
                DateTime dtBirthDay = DateTime.ParseExact(listPeople[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                listPeople[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
            }
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
            for (int i = 0; i < listPeople.Count; i++)
            {
                DateTime dtBirthDay = DateTime.ParseExact(listPeople[i].BirthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                listPeople[i].BirthDay = dtBirthDay.ToString("dd/MM/yyyy");
            }
            DemographicList = new BindableCollection<DemographicModel>(listPeople);
            NotifyOfPropertyChange(() => DemographicList);
        }
        public void ViewDetail()
        {
            if (_selectedPerson == null)
            {
                MessageBox.Show("Vui lòng chọn nhân khẩu để xem!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                IWindowManager manager = new WindowManager();
                manager.ShowWindowAsync(new NewDemographicViewModel(2, _selectedPerson.IdentityCode, this), null, null);
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
