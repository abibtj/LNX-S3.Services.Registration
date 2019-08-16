using S3.Common;
using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Domain
{
    public class Teacher : Person
    {
        public string Position { get; set; }
        public double GradeLevel { get; set; }
        public Guid SchoolId { get; set; }
        public virtual School School { get; set; }
    }
}
