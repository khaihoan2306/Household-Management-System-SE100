using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class ResidenceModel
    {
        private string absenceName, identityCode, birthDay, address, gender, shelter, arrivalDate;
        public ResidenceModel(string absenceName, string identityCode, string birthDay, string gender, string address, string shelter, string arrivalDate)
        {
            this.AbsenceName = absenceName;
            this.IdentityCode = identityCode;
            this.BirthDay = birthDay;
            this.Address = address;
            this.Gender = gender;
            this.Shelter = shelter;
            this.ArrivalDate = arrivalDate;
        }

        public string AbsenceName { get => absenceName; set => absenceName = value; }
        public string IdentityCode { get => identityCode; set => identityCode = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string Address { get => address; set => address = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Shelter { get => shelter; set => shelter = value; }
        public string ArrivalDate { get => arrivalDate; set => arrivalDate = value; }
    }
}

