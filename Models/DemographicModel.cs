﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class DemographicModel
    {
        private string identityCode, name, secondName, householdCode, gender, birthDay, relative, birthPlace, nativeVillage, ethnic, religion, nationality, currentAddress, educationLevel, technicalLevel, job, workPlace, maritalStatus, livingStatus, note;

        public DemographicModel(string identityCode, string name, string secondName, string householdCode, string gender, string birthDay, string relative, string birthPlace, string nativeVillage, string ethnic, string religion, string nationality, string currentAddress, string educationLevel, string technicalLevel, string job, string workPlace, string maritalStatus, string livingStatus, string note)
        {
            this.IdentityCode = identityCode;
            this.Name = name;
            this.SecondName = secondName;
            this.HouseholdCode = householdCode;
            this.Gender = gender;
            this.BirthDay = birthDay;
            this.Relative = relative;
            this.BirthPlace = birthPlace;
            this.NativeVillage = nativeVillage;
            this.Ethnic = ethnic;
            this.Religion = religion;
            this.Nationality = nationality;
            this.CurrentAddress = currentAddress;
            this.EducationLevel = educationLevel;
            this.TechnicalLevel = technicalLevel;
            this.Job = job;
            this.WorkPlace = workPlace;
            this.MaritalStatus = maritalStatus;
            this.LivingStatus = livingStatus;
            this.Note = note;
        }
        public string IdentityCode { get => identityCode; set => identityCode = value; }
        public string Name { get => name; set => name = value; }
        public string SecondName { get => secondName; set => secondName = value; }
        public string HouseholdCode { get => householdCode; set => householdCode = value; }
        public string Gender { get => gender; set => gender = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string Relative { get => relative; set => relative = value; }
        public string BirthPlace { get => birthPlace; set => birthPlace = value; }
        public string NativeVillage { get => nativeVillage; set => nativeVillage = value; }
        public string Ethnic { get => ethnic; set => ethnic = value; }
        public string Religion { get => religion; set => religion = value; }
        public string Nationality { get => nationality; set => nationality = value; }
        public string CurrentAddress { get => currentAddress; set => currentAddress = value; }
        public string EducationLevel { get => educationLevel; set => educationLevel = value; }
        public string TechnicalLevel { get => technicalLevel; set => technicalLevel = value; }
        public string Job { get => job; set => job = value; }
        public string WorkPlace { get => workPlace; set => workPlace = value; }
        public string MaritalStatus { get => maritalStatus; set => maritalStatus = value; }
        public string LivingStatus { get => livingStatus; set => livingStatus = value; }
        public string Note { get => note; set => note = value; }
    }
}
