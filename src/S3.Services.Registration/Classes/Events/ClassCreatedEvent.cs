using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using S3.Services.Registration.Domain;

namespace S3.Services.Registration.Classes.Events
{
    // Immutable
    public class ClassCreatedEvent : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public School School { get; }

        [JsonConstructor]
        public ClassCreatedEvent(Guid id, string name, School school)
            => (Id, Name, School) = (id, name, school);
    }
}