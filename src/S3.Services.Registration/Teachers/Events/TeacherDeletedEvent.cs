using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Teachers.Events
{
    public class TeacherDeletedEvent : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public TeacherDeletedEvent(Guid id) => Id = id;
    }
}
