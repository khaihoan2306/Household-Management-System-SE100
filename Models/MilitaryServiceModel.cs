using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class MilitaryServiceModel
    {
        private string absenceName, identityCode, birthDay, enthnic, otherName, address, status;
        public MilitaryServiceModel(string absenceName, string identityCode, string birthDay, string enthnic, string otherName, string address, string status)
        {
            this.AbsenceName = absenceName;
            this.IdentityCode = identityCode;
            this.BirthDay = birthDay;
            this.Address = address;
            this.Enthnic = enthnic;
            this.OtherName = otherName;
            this.Status = status;
        }

        public string AbsenceName { get => absenceName; set => absenceName = value; }
        public string IdentityCode { get => identityCode; set => identityCode = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string Address { get => address; set => address = value; }
        public string Enthnic { get => enthnic; set => enthnic = value; }
        public string OtherName { get => otherName; set => otherName = value; }
        public string Status { get => status; set => status = value; }

    }
}

