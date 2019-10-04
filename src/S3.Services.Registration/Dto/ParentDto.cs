using S3.Common;
using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Dto
{
    public class ParentDto : PersonDto
    {
        public string RegNumber { get; set; }
        public ParentAddressDto? Address { get; set; }
        public virtual ICollection<StudentDto> Students { get; set; }
    }
}
