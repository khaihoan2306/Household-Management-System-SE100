using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
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
        private readonly IEventAggregator _events;
        private SettingViewModel _settingVM;
        private SimpleContainer _container;
    
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
                    IWindowManager manager = new WindowManager();
                    manager.ShowWindowAsync(new LoginViewModel(), null, null);
                    TryCloseAsync();
                }
            }
            else
            {
                ActivateItemAsync(new DashboardViewModel(_username, this));
                exit = "Đăng xuất";
                NotifyOfPropertyChange(() => Exit);
            } 
        }
        public void HouseholdChangeView()
        {
            ActivateItemAsync(new HouseholdViewModel(_username));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        public void DemographicChangeView()
        {
            ActivateItemAsync(new DemographicViewModel(_username));
            exit = "Quay lại";
            NotifyOfPropertyChange(() => Exit);
        }
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            Environment.Exit(0);
            return base.OnDeactivateAsync(close, cancellationToken);
        }
    }
}
