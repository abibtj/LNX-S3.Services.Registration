using System;
using S3.Common.Messages;
using Newtonsoft.Json;
//using S3.Common.Types;
using System.ComponentModel.DataAnnotations;
using S3.Services.Registration.Domain;

namespace S3.Services.Registration.Schools.Commands
{
    public class CreateSchoolCommand : ICommand
    {
        [Required(ErrorMessage = "School's name is required.")]
        public string Name { get; }
        [Required(ErrorMessage = "School's category is required.")]
        public string Category { get; } // Nursery, Primary, Secondary //***TODO: an enumeration might be better
        [Required(ErrorMessage = "School's address is required.")]
        public SchoolAddress Address { get; } 

        [JsonConstructor]
        public CreateSchoolCommand(string name, string category, SchoolAddress address)
            => (Name, Category, Address) = (name, category, address);
    }
}