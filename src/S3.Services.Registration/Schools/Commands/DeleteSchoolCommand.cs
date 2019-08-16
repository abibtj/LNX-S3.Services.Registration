using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace S3.Services.Registration.Schools.Commands
{
    // Immutable
    // Custom routing key: #.registration.create_school
    public class DeleteSchoolCommand : ICommand
    {
        [Required]
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteSchoolCommand(Guid id) => Id = id;
    }
}