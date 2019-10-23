using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using System.Collections.Generic;

namespace S3.Services.Registration.Parents.Commands
{
    public class UpdateParentCommand : ICommand
    {
        [Required]
        public Guid Id { get; }
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
        public Guid? AddressId { get; }
        public ParentAddress? Address { get; }
        public string[]? RolesArray { get; } // Array of roles of this person


        [JsonConstructor]
        public UpdateParentCommand(
            Guid id, string firstName, string? middleName, string lastName, string gender, DateTime? dateOfBirth, string phoneNumber, 
            List<Guid>? studentIds, Guid? addressId, ParentAddress? address, string[]? rolesArray)
           
            => (Id, FirstName, MiddleName, LastName, Gender, DateOfBirth, PhoneNumber, StudentIds, AddressId, Address, RolesArray)
            = (id, firstName, middleName, lastName, gender, dateOfBirth, phoneNumber, studentIds, addressId, address, rolesArray);
    }
}