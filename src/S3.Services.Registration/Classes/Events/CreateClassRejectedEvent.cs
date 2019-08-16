using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Classes.Events
{
    public class CreateClassRejectedEvent : IRejectedEvent
    {
        public string ClassName { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateClassRejectedEvent(string className, string reason, string code)
            => (ClassName, Reason, Code) = (className, reason, code);
    }
}