using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using System.Collections.Generic;

namespace S3.Services.Registration.Teachers.Commands
{
    public class UpdateTeacherCommand : ICommand
    {
        [Required]
        public Guid Id { get; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; }
        public string? MiddleName { get; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; }
        public string Gender { get; }
        public string PhoneNumber { get; }
        public string Position { get; }
        public double GradeLevel { get; }
        public DateTime? DateOfBirth { get; }
        [Required]
        public Guid SchoolId { get; }
        public Guid? AddressId { get; }
        public TeacherAddress? Address { get; }
        //public List<ScoresEntry>? ScoresEntries { get; }
        public string[]? RolesArray { get; } // Array of roles of this person


        [JsonConstructor]
        public UpdateTeacherCommand(Guid id, string firstName, string middleName, string lastName,
            string gender, string phoneNumber, string position, double gradeLevel,
            DateTime? dateOfBirth, Guid schoolId, Guid? addressId, TeacherAddress? address, string[]? rolesArray)

            => (Id, FirstName, MiddleName, LastName, Gender, PhoneNumber, Position, GradeLevel, 
            DateOfBirth, SchoolId, AddressId, Address, RolesArray)

            = (id, firstName, middleName, lastName, gender, phoneNumber, position, gradeLevel, 
            dateOfBirth, schoolId, addressId, address, rolesArray);
    }
}