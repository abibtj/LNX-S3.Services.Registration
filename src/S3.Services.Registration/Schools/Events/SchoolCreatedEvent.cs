using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using S3.Services.Registration.Domain;
//using S3.Common.Types;

namespace S3.Services.Registration.Schools.Events
{
    // Immutable
    public class SchoolCreatedEvent : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public SchoolAddress Address { get; }

        [JsonConstructor]
        public SchoolCreatedEvent(Guid id, string name, SchoolAddress address)
            => (Id, Name, Address) = (id, name, address);
    }
}