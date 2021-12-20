using Caliburn.Micro;
using Household_Management_System.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.ViewModels
{
    public class UpdateMilitaryViewModel : Screen
    {
        private string _selectedFilter, _identityCode;
        private MilitaryServiceViewModel _militaryVM;
        private List<string> listFilter;
        public BindableCollection<string> FilterList { get; set; }
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
        public UpdateMilitaryViewModel(string identityCode, MilitaryServiceViewModel militaryVM)
        {
            _militaryVM = militaryVM;
            _identityCode = identityCode;
            _selectedFilter = "Đủ tuổi gia nhập";
            listFilter = new List<string>();
            listFilter.Add("Đủ tuổi gia nhập");
            listFilter.Add("Đủ điều kiện nhập ngũ");
            listFilter.Add("Tạm hoãn nghĩa vụ");
            listFilter.Add("Miễn nghĩa vụ");
            FilterList = new BindableCollection<string>(listFilter);
        }
        public void Save()
        {
            MilitaryAccess.UpdateStatus(_identityCode, _selectedFilter);
            _militaryVM.Filter();
            TryCloseAsync();
        }
        public void Close()
        {
            TryCloseAsync();
        }
    }
}
