using System;
using S3.Common.Messages;
using Newtonsoft.Json;
//using S3.Common.Types;
using System.ComponentModel.DataAnnotations;
using S3.Services.Registration.Domain;
using System.Collections.Generic;

namespace S3.Services.Registration.Students.Commands
{
    // Immutable
    // Custom routing key: #.registration.create_school
    public class CreateStudentCommand : ICommand
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; }
        public string MiddleName { get; }
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
        public Address? Address { get; }
        public List<Guid>? SubjectIds { get; } // List of subjects offered by this student

        [JsonConstructor]
        public CreateStudentCommand(
            string firstName, string middleName, string lastName, string gender, DateTime dateOfBirth, string phoneNumber, Guid schoolId, Guid? classId, Guid? parentId, Address? address, List<Guid>? subjectIds)
            => (FirstName, MiddleName, LastName, Gender, DateOfBirth, PhoneNumber, SchoolId, ClassId, ParentId, Address, SubjectIds)
            = (firstName, middleName, lastName, gender, dateOfBirth, phoneNumber, schoolId, classId, parentId, address, subjectIds);
    }
}