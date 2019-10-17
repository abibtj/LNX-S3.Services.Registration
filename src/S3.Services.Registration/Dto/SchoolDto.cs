//using S3.Common.Types;
using S3.Services.Registration.Domain;
using System;
using System.Collections.Generic;

namespace S3.Services.Registration.Dto
{
    public class SchoolDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Category { get; set; } // Primary, Secondary //***TODO: an enumeration might be better
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? AddressId { get; set; }
        public SchoolAddressDto Address { get; set; }

        public virtual ICollection<TeacherDto> Teachers { get; set; }
        public virtual ICollection<ClassDto> Classes { get; set; }
        public virtual ICollection<StudentDto> Students { get; set; }
    }
}