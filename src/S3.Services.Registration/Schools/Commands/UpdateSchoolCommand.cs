using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Common.Types;
using S3.Services.Registration.Domain;

namespace S3.Services.Registration.Schools.Commands
{
    public class UpdateSchoolCommand : ICommand
    {
        [Required]
        public Guid Id { get; }
        [Required(ErrorMessage = "School's name is required.")]
        public string Name { get; }
        [Required(ErrorMessage = "School's category is required.")]
        public string Category { get; } // Primary, Secondary //***TODO: an enumeration might be better
        [Required(ErrorMessage = "School's address is required.")]
        public string? Email { get; }
        public string? PhoneNumber { get; }
        public Guid? AddressId { get; }
        public SchoolAddress Address { get; }

        [JsonConstructor]
        public UpdateSchoolCommand(Guid id, string name, string category, string? email, string? phoneNumber, 
            Guid? addressId, SchoolAddress address)

            => (Id, Name, Category, Email, PhoneNumber, AddressId, Address)
            = (id, name, category, email, phoneNumber, addressId, address);
    }
}