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
    public class TransferViewModel : Screen
    {
        private string textSearch;
        private DemographicModel _selectedPerson;
        private HouseholdModel _selectedHousehold;
        private List<DemographicModel> listMember;
        private List<HouseholdModel> listHousehold;
        public BindableCollection<DemographicModel> MemberList { get; set; }
        public BindableCollection<HouseholdModel> HouseholdList { get; set; }
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
                listMember = DemographicAccess.LoadPeople(_selectedHousehold.HouseholdCode);
                MemberList = new BindableCollection<DemographicModel>(listMember);
                NotifyOfPropertyChange(() => MemberList);
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
        
        public TransferViewModel()
        {
            listHousehold = HouseholdAccess.LoadHousehold("", "");
            HouseholdList = new BindableCollection<HouseholdModel>(listHousehold);
        }
        
        public void Transfer()
        {
            if (_selectedPerson == null)
                MessageBox.Show("Vui lòng chọn một nhân khẩu để chuyển khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                MessageBoxResult mr = MessageBox.Show("Bạn có muốn cắt khẩu cho nhân khẩu này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mr == MessageBoxResult.Yes)
                {
                    DemographicAccess.DeletePerson(_selectedPerson.IdentityCode);
                    MessageBox.Show("Đã cắt khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    listMember = DemographicAccess.LoadPeople(_selectedHousehold.HouseholdCode);
                    MemberList = new BindableCollection<DemographicModel>(listMember);
                    NotifyOfPropertyChange(() => MemberList);
                }
            }
        }
        public void Search()
        {
            listHousehold = HouseholdAccess.LoadHousehold("", textSearch);
            HouseholdList = new BindableCollection<HouseholdModel>(listHousehold);
            NotifyOfPropertyChange(() => HouseholdList);
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
