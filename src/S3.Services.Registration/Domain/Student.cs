using S3.Common;
using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Domain
{
    public class Student : Person
    {
        public string Subjects { get; set; }
        //public string[] SubjectsArray { get => Subjects.Split("|"); } // Readonly property, not saved into database
        public bool OfferingAllClassSubjects { get; set; } = true;
        public Guid SchoolId { get; set; }
        public virtual School School { get; set; }
        public Guid? ClassId { get; set; }
        public virtual Class Class { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Parent Parent { get; set; }
        public StudentAddress? Address { get; set; }
    }
}
