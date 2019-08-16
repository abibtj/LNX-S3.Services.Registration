using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace S3.Services.Registration.Teachers.Commands
{
    public class DeleteTeacherCommand : ICommand
    {
        [Required]
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteTeacherCommand(Guid id) => Id = id;
    }
}