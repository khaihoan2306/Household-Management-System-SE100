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

namespace Household_Management_System.ViewModels
{
    public class NewPoliceViewModel : Screen
    {
        private LocalPoliceModel currentUser;
        private string name, identityCode, birthDay, phone, address, position, provinceManage, districtManage, wardManage, username, password;
        private string _selectedGender, title;
        private List<string> listGender;
        private string _code;
        private SettingAdminViewModel _settingVM;
        public BindableCollection<string> Gender { get; set; }
        public string SelectedGender
        {
            get
            {
                return _selectedGender;
            }
            set
            {
                _selectedGender = value;
                NotifyOfPropertyChange(() => SelectedGender);
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
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }
        public NewPoliceViewModel(string user, SettingAdminViewModel settingVM, string code)
        {
            _code = code;
            _settingVM = settingVM;
            if (_code == "none")
            {
                title = "Thêm mới công an";
            }
            else
            {
                title = "Thông tin công an";
                LocalPoliceModel p = LocalPoliceAccess.LoadPolice(code);
                name = p.Name;
                identityCode = p.IdentityCode;
                birthDay = p.BirthDay;
                _selectedGender = p.Gender;
                position = p.Position;
                phone = p.Phone;
                address = p.Address;
                username = p.Username;
            }
            currentUser = LocalPoliceAccess.LoadPolice(user);
            listGender = new List<string>();
            listGender.Add("Nam");
            listGender.Add("Nữ");
            Gender = new BindableCollection<string>(listGender);
        }
        private bool CanSave()
        {
            if (name == null || username == null || password == null || position == null || phone == null ||  address == null || birthDay == null || _selectedGender == null || identityCode == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
        public void Save()
        {
            if (_code == "none")
            {
                if (CanSave())
                {
                    if (LocalPoliceAccess.LoadPolice(username) != null)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại trong hệ thống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        LocalPoliceModel police = new LocalPoliceModel(identityCode, name, birthDay, position, phone, address, currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage, _selectedGender, 2, username, ConvertToMD5(password));
                        //LocalPoliceAccess.SavePolice(police);
                        MessageBox.Show(birthDay);
                    }
                }
            }
            else
            {
                LocalPoliceModel police = new LocalPoliceModel(identityCode, name, birthDay, position, phone, address, currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage, _selectedGender, 2, _code, "");
                LocalPoliceAccess.UpdatePolice(police);
            }
            _settingVM.Refresh(currentUser.ProvinceManage, currentUser.DistrictManage, currentUser.WardManage);
            TryCloseAsync();
        }
        public void Close()
        {
            TryCloseAsync();
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
    }
}
