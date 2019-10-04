using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using System.Collections.Generic;

namespace S3.Services.Registration.Classes.Commands
{
    public class UpdateClassCommand : ICommand
    {
        [Required]
        public Guid Id { get; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; }
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }
        [Required]
        public Guid SchoolId { get; }
        public Guid? TeacherId { get; }
        //public List<Guid>? StudentIds { get; } // List of wards
        public string[] SubjectsArray { get; } // List of subjects available for this class

        [JsonConstructor]
        public UpdateClassCommand(Guid id, string name, string category, Guid schoolId, Guid? teacherId, string[] subjectsArray)

            => (Id, Name, Category, SchoolId, TeacherId, SubjectsArray)
            = (id, name, category, schoolId, teacherId, subjectsArray);
    }
}