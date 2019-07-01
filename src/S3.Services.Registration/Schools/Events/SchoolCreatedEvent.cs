using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Schools.Events
{
    // Immutable
    public class SchoolCreatedEvent : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public string Code { get; }
        public double Percentage { get; }

        [JsonConstructor]
        public SchoolCreatedEvent(Guid id, Guid customerId,
            string code, double percentage)
        {
            Id = id;
            CustomerId = customerId;
            Code = code;
            Percentage = percentage;
        }
    }
}