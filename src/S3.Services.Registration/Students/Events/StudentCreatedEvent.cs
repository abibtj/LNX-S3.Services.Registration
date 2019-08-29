using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using S3.Services.Registration.Domain;
//using S3.Common.Types;

namespace S3.Services.Registration.Students.Events
{
    // Immutable
    public class StudentCreatedEvent : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public StudentAddress? Address { get; }

        [JsonConstructor]
        public StudentCreatedEvent(Guid id, string name, StudentAddress? address)
            => (Id, Name, Address) = (id, name, address);
    }
}