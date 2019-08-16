using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Parents.Events
{
    public class CreateParentRejectedEvent : IRejectedEvent
    {
        public string ParentName { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateParentRejectedEvent(string parentName, string reason, string code)
            => (ParentName, Reason, Code) = (parentName, reason, code);
    }
}