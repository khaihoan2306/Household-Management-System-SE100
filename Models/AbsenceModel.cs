using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class AbsenceModel
    {
        private string name, identityCode, birthDay, gender, permanentAddress, shelterAddress, currentAddress, reasonAbsence, fromDay, toDay, destination, note;

        public AbsenceModel(string name, string identityCode, string birthDay, string gender, string permanentAddress, string shelterAddress, string currentAddress, string reasonAbsence, string fromDay, string toDay, string destination, string note)
        {
            this.Name = name;
            this.IdentityCode = identityCode;
            this.BirthDay = birthDay;
            this.Gender = gender;
            this.PermanentAddress = permanentAddress;
            this.ShelterAddress = shelterAddress;
            this.CurrentAddress = currentAddress;
            this.ReasonAbsence = reasonAbsence;
            this.FromDay = fromDay;
            this.ToDay = toDay;
            this.Destination = destination;
            this.Note = note;
        }

        public string Name { get => name; set => name = value; }
        public string IdentityCode { get => identityCode; set => identityCode = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string Gender { get => gender; set => gender = value; }
        public string PermanentAddress { get => permanentAddress; set => permanentAddress = value; }
        public string ShelterAddress { get => shelterAddress; set => shelterAddress = value; }
        public string CurrentAddress { get => currentAddress; set => currentAddress = value; }
        public string ReasonAbsence { get => reasonAbsence; set => reasonAbsence = value; }
        public string FromDay { get => fromDay; set => fromDay = value; }
        public string ToDay { get => toDay; set => toDay = value; }
        public string Destination { get => destination; set => destination = value; }
        public string Note { get => note; set => note = value; }
    }
}

