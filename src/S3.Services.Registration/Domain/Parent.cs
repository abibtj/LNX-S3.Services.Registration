using S3.Common;
using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Domain
{
    public class Parent : Person
    {
        public Parent()
        {
            Students = new HashSet<Student>();
        }
        public ParentAddress? Address { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
