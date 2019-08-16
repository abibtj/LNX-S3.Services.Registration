using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Teachers.Events
{
    public class UpdateTeacherRejectedEvent : IRejectedEvent
    {
        public string TeacherName { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public UpdateTeacherRejectedEvent(string teacherName, string reason, string code)
            => (TeacherName, Reason, Code) = (teacherName, reason, code);
    }
}