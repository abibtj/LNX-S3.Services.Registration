using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace S3.Services.Registration.ScoresEntryTasks.Commands
{
    public class DeleteScoresEntryTaskCommand : ICommand
    {
        [Required]
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteScoresEntryTaskCommand(Guid id) => Id = id;
    }
}