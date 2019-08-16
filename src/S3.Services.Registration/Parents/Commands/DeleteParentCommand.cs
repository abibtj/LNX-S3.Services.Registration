using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace S3.Services.Registration.Parents.Commands
{
    public class DeleteParentCommand : ICommand
    {
        [Required]
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteParentCommand(Guid id) => Id = id;
    }
}