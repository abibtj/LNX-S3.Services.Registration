using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Schools.Events
{
    public class SchoolDeletedEvent : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public SchoolDeletedEvent(Guid id) => Id = id;
    }
}
