using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Household_Management_System.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private string userInfo;
        private LocalPoliceModel currentUser;
        public ShellViewModel(string username)
        {
            ActivateItemAsync(new DashboardViewModel(username));
            currentUser = LocalPoliceAccess.LoadPolice(username);
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
        public void LogOut()
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                IWindowManager manager = new WindowManager();
                manager.ShowWindowAsync(new LoginViewModel(), null, null);
                TryCloseAsync();
            }           
        }
    }
}
