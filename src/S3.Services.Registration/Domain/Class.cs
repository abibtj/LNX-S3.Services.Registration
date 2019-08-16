using S3.Common;
using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Domain
{
    public class Class : BaseEntity
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }
        public string Name { get; set; }
        public string SubjectIds { get; set; }

        // Navigation properties 
        public Guid SchoolId { get; set; }
        public virtual School School { get; set; }
        public Guid? TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
