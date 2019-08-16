using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Students.Events
{
    public class DeleteStudentRejectedEvent : IRejectedEvent
    {
        public string StudentName { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public DeleteStudentRejectedEvent(string studentName, string reason, string code)
            => (StudentName, Reason, Code) = (studentName, reason, code);
    }
}