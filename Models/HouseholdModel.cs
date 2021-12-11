using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class HouseholdModel
    {
        private string householdCode = "", hostCode="", address="", village="", ward="", district="", province="", note="";

        public HouseholdModel(string householdCode, string hostCode, string address, string village, string ward, string district, string province, string note)
        {
            this.HouseholdCode = householdCode;
            this.HostCode = hostCode;
            this.Address = address;
            this.Village = village;
            this.Ward = ward;
            this.District = district;
            this.Province = province;
            this.Note = note;
        }

        public string HouseholdCode { get => householdCode; set => householdCode = value; }
        public string HostCode { get => hostCode; set => hostCode = value; }
        public string Address { get => address; set => address = value; }
        public string Village { get => village; set => village = value; }
        public string Ward { get => ward; set => ward = value; }
        public string District { get => district; set => district = value; }
        public string Province { get => province; set => province = value; }
        public string Note { get => note; set => note = value; }
    }
}
