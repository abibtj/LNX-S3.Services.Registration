using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using S3.Services.Registration.Domain;

namespace S3.Services.Registration.Parents.Events
{
    // Immutable
    public class ParentCreatedEvent : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public Address? Address { get; }

        [JsonConstructor]
        public ParentCreatedEvent(Guid id, string name, Address? address)
            => (Id, Name, Address) = (id, name, address);
    }
}