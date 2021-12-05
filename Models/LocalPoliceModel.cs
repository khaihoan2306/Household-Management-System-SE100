using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class LocalPoliceModel
    {
        private string name, identityCode, birthDay, phone, address, gender, position, provinceManage, districtManage, wardManage, username, password;
        private int role;

        public LocalPoliceModel() { }
        public LocalPoliceModel(string identityCode, string name, string birthDay, string position, string phone, string address, string provinceManage, string districtManage, string wardManage, string gender, int role, string username, string password)
        {
            this.IdentityCode = identityCode;
            this.Name = name; 
            this.BirthDay = birthDay;
            this.Phone = phone;
            this.Address = address;
            this.Gender = gender;
            this.Position = position;
            this.ProvinceManage = provinceManage;
            this.DistrictManage = districtManage;
            this.WardManage = wardManage;
            this.Role = role;
            this.Username = username;
            this.Password = password;
        }

        public string Name { get => name; set => name = value; }
        public string IdentityCode { get => identityCode; set => identityCode = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Position { get => position; set => position = value; }
        public string ProvinceManage { get => provinceManage; set => provinceManage = value; }
        public string DistrictManage { get => districtManage; set => districtManage = value; }
        public string WardManage { get => wardManage; set => wardManage = value; }
        public int Role { get => role; set => role = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}
