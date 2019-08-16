using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using System.Collections.Generic;

namespace S3.Services.Registration.Subjects.Commands
{
    public class UpdateSubjectCommand : ICommand
    {
        [Required]
        public Guid Id { get; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; }

        [JsonConstructor]
        public UpdateSubjectCommand(Guid id, string name) => (Id, Name) = (id, name);
    }
}