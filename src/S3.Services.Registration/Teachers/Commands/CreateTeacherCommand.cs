using System;
using S3.Common.Messages;
using Newtonsoft.Json;
//using S3.Common.Types;
using System.ComponentModel.DataAnnotations;
using S3.Services.Registration.Domain;
using System.Collections.Generic;

namespace S3.Services.Registration.Teachers.Commands
{
    // Immutable
    // Custom routing key: #.registration.create_school
    public class CreateTeacherCommand : ICommand
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; }
        public string? MiddleName { get; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; }
        public string Gender { get; }
        public string PhoneNumber { get; }
        public string? Position { get; set; }
        public int? GradeLevel { get; set; }
        public int? Step { get; set; }
        public DateTime? DateOfBirth { get; }
        [Required]
        public Guid SchoolId { get; }
        public TeacherAddress? Address { get; }
        //public List<ScoresEntry>? ScoresEntries { get; }
        public string[]? RolesArray { get; } // Array of roles of this person


        [JsonConstructor]
        public CreateTeacherCommand(string firstName, string? middleName, string lastName,
            string gender, string phoneNumber, string position, int? gradeLevel, int? step,
            DateTime? dateOfBirth, Guid schoolId, TeacherAddress? address, string[]? rolesArray)

            => (FirstName, MiddleName, LastName, Gender, PhoneNumber, Position, GradeLevel, 
            Step, DateOfBirth, SchoolId, Address, RolesArray)

            = (firstName, middleName, lastName, gender, phoneNumber, position, gradeLevel, 
            step, dateOfBirth, schoolId, address, rolesArray);
    }
}