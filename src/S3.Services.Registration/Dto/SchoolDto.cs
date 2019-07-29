using S3.Common.Types;
using System;

namespace S3.Services.Registration.Dto
{
    public class SchoolDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // Primary, Secondary //***TODO: an enumeration might be better
        public Address Address { get; set; } //***TODO: create an address object
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}