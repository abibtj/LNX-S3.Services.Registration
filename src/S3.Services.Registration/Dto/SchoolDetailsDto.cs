using System;

namespace S3.Services.Registration.Dto
{
    public class SchoolDetailsDto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; } // Primary, Secondary //***TODO: an enumeration might be better
        public string Address { get; private set; } //***TODO: create an address object
        public DateTime CreatedAt { get; private set; }
    }
}