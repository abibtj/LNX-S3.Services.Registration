//using S3.Common.Types;
using S3.Services.Registration.Domain;
using System;

namespace S3.Services.Registration.Dto
{
    public class SchoolDto : BaseDto
    {
        public string Name { get; set; }
        public string Category { get; set; } // Primary, Secondary //***TODO: an enumeration might be better
        public AddressDto Address { get; set; } //***TODO: create an address object
    }
}