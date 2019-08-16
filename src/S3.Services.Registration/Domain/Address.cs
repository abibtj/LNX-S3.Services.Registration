using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Registration.Domain
{
    public class Address
    {
        public Guid Id { get; private set; }
        [Required(ErrorMessage = "Address first line is required.")]
        public string Line1 { get; set; }
        public string Line2 { get; set; } 
        [Required(ErrorMessage = "Town name is required.")]
        public string Town { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; } 
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } 
    }
}
