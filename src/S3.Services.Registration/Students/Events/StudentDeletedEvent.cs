using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Students.Events
{
    public class StudentDeletedEvent : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public StudentDeletedEvent(Guid id) => Id = id;
    }
}
