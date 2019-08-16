﻿using S3.Common;
using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Dto
{
    public class PersonDto : BaseDto
    {
        public string FirstName { get; set; } 
        public string MiddleName { get; set; } 
        public string LastName { get; set; } 
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public AddressDto? Address { get; set; }
    }
}