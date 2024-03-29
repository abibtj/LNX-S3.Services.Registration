//using S3.Common.Types;
using S3.Services.Registration.Domain;
using System;

namespace S3.Services.Registration.Dto
{
    public class StudentDto : PersonDto
    {
        public string RegNumber { get; set; }
        public string Subjects { get; set; }
        public string[] SubjectsArray { get => string.IsNullOrEmpty(Subjects)? Array.Empty<string>() : Subjects.Split("|"); } // Readonly property, not saved into database

        public bool OfferingAllClassSubjects { get; set; }
        public Guid SchoolId { get; set; }
        public virtual SchoolDto School { get; set; }
        public Guid? ClassId { get; set; }
        public virtual ClassDto Class { get; set; }
        public Guid? ParentId { get; set; }
        public virtual ParentDto Parent { get; set; }
        public Guid? AddressId { get; set; }
        public virtual StudentAddressDto Address { get; set; }
    }
}