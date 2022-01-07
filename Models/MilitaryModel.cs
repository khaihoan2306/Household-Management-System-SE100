using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class MilitaryModel
    {
        private string identityCode, name, gender, birthDay, ethnic, currentAddress, status, note;

        public MilitaryModel(string identityCode, string name, string gender, string birthDay, string ethnic, string currentAddress, string status, string note)
        {
            this.IdentityCode = identityCode;
            this.Name = name;
            this.Gender = gender;
            this.BirthDay = birthDay;
            this.Ethnic = ethnic;
            this.CurrentAddress = currentAddress;
            this.Status = status;
            this.Note = note;
        }

        public string IdentityCode { get => identityCode; set => identityCode = value; }
        public string Name { get => name; set => name = value; }
        public string Gender { get => gender; set => gender = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string Ethnic { get => ethnic; set => ethnic = value; }
        public string CurrentAddress { get => currentAddress; set => currentAddress = value; }
        public string Status { get => status; set => status = value; }
        public string Note { get => note; set => note = value; }
    }
}
