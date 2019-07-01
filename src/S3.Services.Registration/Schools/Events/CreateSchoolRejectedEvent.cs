using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Schools.Events
{
    public class CreateSchoolRejectedEvent : IRejectedEvent
    {
        public string SchoolName { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateSchoolRejectedEvent(string schoolName, string reason, string code)
        {
            SchoolName = schoolName;
            Reason = reason;
            Code = code;
        }
    }
}