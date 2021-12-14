using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class AbsenceModel
    {
        private string absenceName, identityCode, birthDay, address, gender, toDay, fromDay, reasonAbsence;
        public AbsenceModel(string absenceName, string identityCode, string birthDay,  string gender, string address, string toDay, string fromDay, string reasonAbsence)
        {
            this.AbsenceName = absenceName;
            this.IdentityCode = identityCode;
            this.BirthDay = birthDay;
            this.Address = address;
            this.Gender = gender;
            this.ToDay = toDay;
            this.FromDay = fromDay;
            this.ReasonAbsence = reasonAbsence;
        }

        public string AbsenceName { get => absenceName; set => absenceName = value; }
        public string IdentityCode { get => identityCode; set => identityCode = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string Address { get => address; set => address = value; }
        public string Gender { get => gender; set => gender = value; }
        public string ToDay { get => toDay; set => toDay = value; }
        public string FromDay { get => fromDay; set => fromDay = value; }
        public string ReasonAbsence { get => reasonAbsence; set => reasonAbsence = value; }
    }
}

