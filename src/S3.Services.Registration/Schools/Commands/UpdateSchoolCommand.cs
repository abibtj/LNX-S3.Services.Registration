using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Common.Types;

namespace S3.Services.Registration.Schools.Commands
{
    // Immutable
    // Custom routing key: #.registration.create_school
    public class UpdateSchoolCommand : ICommand
    {
        [Required]
        public Guid Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string Category { get; private set; } // Primary, Secondary //***TODO: an enumeration might be better
        [Required]
        public Address Address { get; private set; } //***TODO: create an address object

        [JsonConstructor]
        public UpdateSchoolCommand(Guid id, string name, string category, Address address)
        {
            //***TODO: Do proper validation of the parameters before assignment
            Id = id;
            Name = name;
            Category = category;
            Address = address;
        }
    }
}