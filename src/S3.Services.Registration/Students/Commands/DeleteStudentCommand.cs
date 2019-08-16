using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace S3.Services.Registration.Students.Commands
{
    public class DeleteStudentCommand : ICommand
    {
        [Required]
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteStudentCommand(Guid id) => Id = id;
    }
}