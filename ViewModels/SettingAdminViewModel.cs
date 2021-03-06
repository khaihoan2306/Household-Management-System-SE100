using Caliburn.Micro;
using Household_Management_System.DataAccess;
using Household_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Household_Management_System.ViewModels
{
    public class SettingAdminViewModel : Screen
    {
        private string name, identityCode, birthDay, phone, address, gender, position, provinceManage, districtManage, wardManage, oldPassword, newPassword, confirmPassword;
        private LocalPoliceModel currentUser, _selectedPolice;
        private string _province, _district, _ward, _username;
        private List<LocalPoliceModel> listPolice;
        public BindableCollection<LocalPoliceModel> PoliceList { get; set; }
        public LocalPoliceModel SelectedPolice
        {
            get
            {
                return _selectedPolice;
            }
            set
            {
                _selectedPolice = value;
                NotifyOfPropertyChange(() => SelectedPolice);
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public string IdentityCode
        {
            get
            {
                return identityCode;
            }
            set
            {
                identityCode = value;
                NotifyOfPropertyChange(() => IdentityCode);
            }
        }
        public string BirthDay
        {
            get
            {
                return birthDay;
            }
            set
            {
                birthDay = value;
                NotifyOfPropertyChange(() => BirthDay);
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
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                NotifyOfPropertyChange(() => Gender);
            }
        }
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                NotifyOfPropertyChange(() => Position);
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                NotifyOfPropertyChange(() => Phone);
            }
        }
        public string ProvinceManage
        {
            get
            {
                return provinceManage;
            }
            set
            {
                provinceManage = value;
                NotifyOfPropertyChange(() => ProvinceManage);
            }
        }
        public string DistrictManage
        {
            get
            {
                return districtManage;
            }
            set
            {
                districtManage = value;
                NotifyOfPropertyChange(() => DistrictManage);
            }
        }
        public string WardManage
        {
            get
            {
                return wardManage;
            }
            set
            {
                wardManage = value;
                NotifyOfPropertyChange(() => WardManage);
            }
        }
        public string OldPassword
        {
            get
            {
                return oldPassword;
            }
            set
            {
                oldPassword = value;
                NotifyOfPropertyChange(() => OldPassword);
            }
        }
        public string NewPassword
        {
            get
            {
                return newPassword;
            }
            set
            {
                newPassword = value;
                NotifyOfPropertyChange(() => NewPassword);
            }
        }
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                confirmPassword = value;
                NotifyOfPropertyChange(() => ConfirmPassword);
            }
        }
        public void RefreshInfo()
        {
            listPolice = LocalPoliceAccess.LoadListPolice(_province, _district, _ward);
            PoliceList = new BindableCollection<LocalPoliceModel>(listPolice);
            NotifyOfPropertyChange(() => PoliceList);
            SetInfo();
        }
        public SettingAdminViewModel(string username)
        {
            _username = username;
            currentUser = LocalPoliceAccess.LoadPolice(_username);
            _province = currentUser.ProvinceManage;
            _district = currentUser.DistrictManage;
            _ward = currentUser.WardManage;
            RefreshInfo();
            
        }
        public void SetInfo()
        {
            
            currentUser = LocalPoliceAccess.LoadPolice(_username);
            name = currentUser.Name;
            birthDay = currentUser.BirthDay;
            DateTime dtBirthDay = DateTime.ParseExact(birthDay, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            birthDay = dtBirthDay.ToString("dd/MM/yyyy");
            gender = currentUser.Gender;
            phone = currentUser.Phone;
            address = currentUser.Address;
            position = currentUser.Position;
            identityCode = currentUser.IdentityCode;
            provinceManage = ProvinceInfoAccess.LoadProvinceName(_province, _district, _ward);
            districtManage = ProvinceInfoAccess.LoadDistrictName(_province, _district, _ward);
            wardManage = ProvinceInfoAccess.LoadWardName(_province, _district, _ward);
            NotifyOfPropertyChange(() => Name);
            NotifyOfPropertyChange(() => BirthDay);
            NotifyOfPropertyChange(() => Gender);
            NotifyOfPropertyChange(() => Phone);
            NotifyOfPropertyChange(() => Address);
            NotifyOfPropertyChange(() => Position);
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
        private bool CanChangePassword()
        {
            if (oldPassword == null || newPassword == null || confirmPassword == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
        public void ChangePassword()
        {
            if (CanChangePassword())
            {
                if (currentUser.Password.Equals(ConvertToMD5(oldPassword)))
                {
                    if (newPassword.Equals(confirmPassword))
                    {
                        LocalPoliceAccess.ChangePassword(ConvertToMD5(newPassword), currentUser.Username);
                        MessageBox.Show("Bạn đã đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        oldPassword = "";
                        newPassword = "";
                        confirmPassword = "";
                        NotifyOfPropertyChange(() => OldPassword);
                        NotifyOfPropertyChange(() => NewPassword);
                        NotifyOfPropertyChange(() => ConfirmPassword);
                    }
                    else
                    {
                        MessageBox.Show("Xác nhận mật khẩu chưa chính xác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        public void AddAccount()
        {
            IWindowManager manager = new WindowManager();
            manager.ShowWindowAsync(new NewPoliceViewModel(currentUser.Username, this, "none"), null, null);
        }
        public void ViewDetail()
        {
            if (_selectedPolice == null)
            {
                MessageBox.Show("Vui lòng chọn một đối tượng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                IWindowManager manager = new WindowManager();
                manager.ShowWindowAsync(new NewPoliceViewModel(currentUser.Username, this, _selectedPolice.Username), null, null);
            }
        }
    }
}
