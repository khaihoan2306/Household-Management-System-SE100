using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Household_Management_System.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private string userInfo, exit = "Đăng xuất", _username;
        private LocalPoliceModel currentUser;
    
        public ShellViewModel(string username)
        {
            _username = username;
            ActivateItemAsync(new DashboardViewModel(_username, this));
            currentUser = LocalPoliceAccess.LoadPolice(_username);
            SetUserInfo();
        }
        private void SetUserInfo()
        {
            userInfo = "User: " + currentUser.Name + ", Chức vụ: " + currentUser.Position;
        }
        public string UserInfo
        {
            get
            {
                return userInfo;
            }
            set
            {
                userInfo = value;
                NotifyOfPropertyChange(() => UserInfo);
            }
        }
        public string Exit
        {
            get
            {
                return exit;
            }
            set
            {
                exit = value;
                NotifyOfPropertyChange(() => Exit);
            }
        }
        public void LogOut()
        {         
            if (ActiveItem.ToString().Equals("Household_Management_System.ViewModels.DashboardViewModel"))
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    TryCloseAsync();
                    IWindowManager manager = new WindowManager();
                    manager.ShowWindowAsync(new LoginViewModel(), null, null);    
                }
            }
            else
            {
                ActivateItemAsync(new DashboardViewModel(_username, this));
                exit = "Đăng xuất";
                NotifyOfPropertyChange(() => Exit);
            } 
        }
        public void LogOutMenu()
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                TryCloseAsync();
                IWindowManager manager = new WindowManager();
                manager.ShowWindowAsync(new LoginViewModel(), null, null);
            }
        }
        public void HouseholdChangeView()
        {
            ActivateItemAsync(new HouseholdViewModel(_username, this));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void DemographicChangeView()
        {
            ActivateItemAsync(new DemographicViewModel(_username, this));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void TemporaryAbsenceChangeView()
        {
            ActivateItemAsync(new AbsenceViewModel(_username));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void TemporaryResidenceChangeView()
        {
            ActivateItemAsync(new ResidenceViewModel(_username));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void MilitaryServiceChangeView()
        {
            ActivateItemAsync(new MilitaryServiceViewModel(_username));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void SettingChangeView()
        {
            ActivateItemAsync(new SettingViewModel(_username));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void SettingAdminChangeView()
        {
            ActivateItemAsync(new SettingAdminViewModel(_username));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void HouseholdDetailChangeView(string householdCode)
        {
            ActivateItemAsync(new HouseholdDetailViewModel(householdCode));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void TransferChangeView()
        {
            ActivateItemAsync(new TransferViewModel());
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void ReportChangeView()
        {
            ActivateItemAsync(new ReportViewModel(_username));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void NewDemographicChangeView()
        {
            IWindowManager manager = new WindowManager();
            manager.ShowWindowAsync(new NewDemographicViewModel(), null, null);
        }
    }
}
