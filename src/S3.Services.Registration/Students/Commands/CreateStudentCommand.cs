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
        public Guid? ClassId { get; }
        public Guid? ParentId { get; }
        public StudentAddress? Address { get; }
        public string? SubjectIds { get; } // Concatenated list of subjects (Ids) offered by this student

        [JsonConstructor]
        public CreateStudentCommand(
            string firstName, string middleName, string lastName, string gender, DateTime dateOfBirth, string phoneNumber, Guid schoolId, Guid? classId = null, Guid? parentId = null, StudentAddress? address = null, string? subjectIds = null)
            => (FirstName, MiddleName, LastName, Gender, DateOfBirth, PhoneNumber, SchoolId, ClassId, ParentId, Address, SubjectIds)
            = (firstName, middleName, lastName, gender, dateOfBirth, phoneNumber, schoolId, classId, parentId, address, subjectIds);
    }
}