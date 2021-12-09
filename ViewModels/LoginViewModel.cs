using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Household_Management_System.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string username;
        private string password;
        LocalPoliceModel currentUser;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                NotifyOfPropertyChange(() => Username);
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        private bool CanLogin(string username="", string password="")
        {
            if (!username.Equals("") && !password.Equals("")) return true;
            return false;
        }
        public void Login()
        {
            if (CanLogin(username, password))
                CheckAccount(username, password);
            else
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private string ConvertToMD5(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        private void CheckAccount(string user, string pass)
        {
            if (LocalPoliceAccess.LoadPolice(user) != null)
            {
                currentUser = LocalPoliceAccess.LoadPolice(user);
                if (currentUser.Password.Equals(ConvertToMD5(pass)))
                {
                    IWindowManager manager = new WindowManager();
                    manager.ShowWindowAsync(new ShellViewModel(user), null, null);
                    TryCloseAsync();
                }                  
                else
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
