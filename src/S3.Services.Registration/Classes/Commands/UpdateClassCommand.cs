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
        [Required]
        public Guid SchoolId { get; }
        public Guid? TeacherId { get; }
        public List<Guid>? StudentIds { get; } // List of wards
        public List<Guid>? SubjectIds { get; } // List of subjects available for this class

        [JsonConstructor]
        public UpdateClassCommand(Guid id, string name, Guid schoolId, Guid? teacherId, List<Guid>? studentIds, List<Guid>? subjectIds)

            => (Id, Name, SchoolId, TeacherId, StudentIds, SubjectIds)
            = (id, name, schoolId, teacherId, studentIds, subjectIds);
    }
}