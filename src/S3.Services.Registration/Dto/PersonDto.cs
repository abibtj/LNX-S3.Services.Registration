using S3.Common;
using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Dto
{
    public class PersonDto : BaseEntityDto
    {
        public string FirstName { get; set; } 
        public string? MiddleName { get; set; } 
        public string LastName { get; set; } 
        public string Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsSignedUp { get; set; }
        public string Roles { get; set; }
        public string[] RolesArray { get => string.IsNullOrEmpty(Roles) ? Array.Empty<string>() : Roles.Split("|"); } // Readonly property, not saved into database
    }
}
