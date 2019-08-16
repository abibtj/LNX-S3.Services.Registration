using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Parents.Events
{
    public class ParentDeletedEvent : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ParentDeletedEvent(Guid id) => Id = id;
    }
}
