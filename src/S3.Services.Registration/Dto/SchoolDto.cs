//using S3.Common.Types;
using S3.Services.Registration.Domain;
using System;
using System.Collections.Generic;

namespace S3.Services.Registration.Dto
{
    public class SchoolDto : BaseEntityDto
    {
        // School Domain Properties
        public string Name { get; set; }
        public string Category { get; set; } // Primary, Secondary //***TODO: an enumeration might be better
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? AddressId { get; set; }
        public SchoolAddressDto Address { get; set; }

        // More Stats about the school (to be computed)
        public string? Administrator { get; set; }
        public Guid? AdministratorId { get; set; }
        public string? Location { get; set; }
        public int? NumberOfTeachers { get; set; }
        public int? NumberOfClasses { get; set; }
        public int? NumberOfStudents { get; set; }

        // School Domain Navigation Properties
        public virtual ICollection<TeacherDto> Teachers { get; set; }
        public virtual ICollection<ClassDto> Classes { get; set; }
        public virtual ICollection<StudentDto> Students { get; set; }
    }
}