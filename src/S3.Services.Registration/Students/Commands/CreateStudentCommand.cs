using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Services.Registration.Domain;
using System.Collections.Generic;

namespace S3.Services.Registration.Students.Commands
{
    public class CreateStudentCommand : ICommand
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; }
        public string? MiddleName { get; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; }
        [Required(ErrorMessage = "Gender name is required.")]
        public string Gender { get; }
        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; }
        public string PhoneNumber { get; }
        [Required]
        public Guid SchoolId { get; }
        [Required]
        public Guid ClassId { get; }
        public Guid? ParentId { get; }
        public StudentAddress? Address { get; }
        public string[]? SubjectsArray { get; } // Array of subjects offered by this student
        public bool OfferingAllClassSubjects { get; }
        public string[]? RolesArray { get; } // Array of roles of this person

        [JsonConstructor]
        public CreateStudentCommand(
            string firstName, string middleName, string lastName, string gender, DateTime dateOfBirth,
            string phoneNumber, Guid schoolId, bool offeringAllClassSubjects, string[]? subjectsArray, 
            Guid classId, Guid? parentId, StudentAddress? address, string[]? rolesArray)

            => (FirstName, MiddleName, LastName, Gender, DateOfBirth,
            PhoneNumber, SchoolId, ClassId, ParentId, Address, SubjectsArray, 
            OfferingAllClassSubjects, RolesArray)

            = (firstName, middleName, lastName, gender, dateOfBirth,
            phoneNumber, schoolId, classId, parentId, address, subjectsArray, 
            OfferingAllClassSubjects, rolesArray);
    }
}