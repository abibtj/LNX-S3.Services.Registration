using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Services.Registration.Domain;
using System.Collections.Generic;

namespace S3.Services.Registration.Parents.Commands
{
    public class CreateParentCommand : ICommand
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; }
        public string? MiddleName { get; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; }
        [Required(ErrorMessage = "Gender name is required.")]
        public string Gender { get; }
        public DateTime? DateOfBirth { get; }
        public string PhoneNumber { get; }
        public List<Guid>? StudentIds { get; } // List of wards
        public ParentAddress? Address { get; }
        public string[]? RolesArray { get; } // Array of roles of this person


        [JsonConstructor]
        public CreateParentCommand(
            string firstName, string middleName, string lastName, string gender, DateTime? dateOfBirth, string phoneNumber, 
            List<Guid>? studentIds, ParentAddress? address, string[]? rolesArray)
           
            => (FirstName, MiddleName, LastName, Gender, DateOfBirth, PhoneNumber, StudentIds, Address, RolesArray)
            = (firstName, middleName, lastName, gender, dateOfBirth, phoneNumber, studentIds, address, rolesArray);
    }
}