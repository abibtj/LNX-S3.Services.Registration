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
        public ParentDto()
        {
            Students = new HashSet<StudentDto>();
        }
       
        public virtual ICollection<StudentDto> Students { get; set; }
    }
}
