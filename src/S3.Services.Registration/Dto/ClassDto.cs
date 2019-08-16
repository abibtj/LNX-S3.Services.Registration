using S3.Common;
using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Dto
{
    public class ClassDto : BaseDto
    {
        public ClassDto()
        {
            Students = new HashSet<StudentDto>();
        }
        public string Name { get; set; }
        public string SubjectIds { get; set; }

        // Navigation properties
        public Guid SchoolId { get; set; }
        public virtual SchoolDto School { get; set; }
        public Guid? TeacherId { get; set; }
        public virtual TeacherDto Teacher { get; set; }

        public virtual ICollection<StudentDto> Students { get; set; }
    }
}
