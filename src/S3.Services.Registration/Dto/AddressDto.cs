using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Registration.Dto
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Line1 { get; set; } 
        public string Line2 { get; set; }
        public string Town { get; set; }
        public string State { get; set; } 
        public string Country { get; set; }
    }
}
