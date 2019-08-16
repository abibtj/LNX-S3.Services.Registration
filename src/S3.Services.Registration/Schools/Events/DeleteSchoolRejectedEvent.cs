using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Schools.Events
{
    public class DeleteSchoolRejectedEvent : IRejectedEvent
    {
        public string SchoolName { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public DeleteSchoolRejectedEvent(string schoolName, string reason, string code)
            => (SchoolName, Reason, Code) = (schoolName, reason, code);
    }
}