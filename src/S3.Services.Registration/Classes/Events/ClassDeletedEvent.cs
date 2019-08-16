using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Classes.Events
{
    public class ClassDeletedEvent : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ClassDeletedEvent(Guid id) => Id = id;
    }
}
