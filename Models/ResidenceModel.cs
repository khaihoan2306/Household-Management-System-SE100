using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class ResidenceModel
    {
        private string identityCode, name, birthDay, gender, permanentAddress, shelterAddress, arrivalDay, note;

        public ResidenceModel(string identityCode, string name, string birthDay, string gender, string permanentAddress, string shelterAddress, string arrivalDay, string note)
        {
            this.IdentityCode = identityCode;
            this.Name = name;
            this.BirthDay = birthDay;
            this.Gender = gender;
            this.PermanentAddress = permanentAddress;
            this.ShelterAddress = shelterAddress;
            this.ArrivalDay = arrivalDay;
            this.Note = note;
        }

        public string IdentityCode { get => identityCode; set => identityCode = value; }
        public string Name { get => name; set => name = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string Gender { get => gender; set => gender = value; }
        public string PermanentAddress { get => permanentAddress; set => permanentAddress = value; }
        public string ShelterAddress { get => shelterAddress; set => shelterAddress = value; }
        public string ArrivalDay { get => arrivalDay; set => arrivalDay = value; }
        public string Note { get => note; set => note = value; }
    }
}

