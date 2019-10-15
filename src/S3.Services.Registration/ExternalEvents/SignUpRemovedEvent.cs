using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.ExternalEvents
{
    [MessageNamespace("identity")]
    public class SignUpRemovedEvent : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public SignUpRemovedEvent(Guid userId) => (UserId) = (userId);
    }
}