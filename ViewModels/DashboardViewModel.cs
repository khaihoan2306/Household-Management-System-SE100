using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Household_Management_System.ViewModels
{
    public class DashboardViewModel : Screen
    {
        private string day, time, address;
        private LocalPoliceModel currentUser;
        ShellViewModel _shell;
        public DashboardViewModel(string username, ShellViewModel shell)
        {
            _shell = shell;
            currentUser = LocalPoliceAccess.LoadPolice(username);
            SetTimerAndAddress();
        }
        private void SetTimerAndAddress()
        {
            day = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy");
            time = "Giờ: " + DateTime.Now.ToString("HH:mm");
            LocalAddressModel userAddress = ProvinceInfoAccess.LoadFullInfo(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage);
            address = "Khu vực: " + userAddress.GetFullAddress();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            day = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy");
            time = "Giờ: " + DateTime.Now.ToString("HH:mm");
            NotifyOfPropertyChange(() => Day);
            NotifyOfPropertyChange(() => Time);
        }
        public string Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
                NotifyOfPropertyChange(() => Day);
            }
        }
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                NotifyOfPropertyChange(() => Time);
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
        public void Setting()
        {
            
        }
        public void Household()
        {
            _shell.HouseholdChangeView();
        }
        public void Demographic()
        {
            _shell.DemographicChangeView();
        }
        public void Permanent()
        {
            IWindowManager manager = new WindowManager();
            manager.ShowWindowAsync(new NewDemographicViewModel(), null, null);
        }
    }
    
}
