using S3.Common;
using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Domain
{
    public class School : BaseEntity
    {
        public School()
        {
            Classes = new HashSet<Class>();
            Teachers = new HashSet<Teacher>();
            Students = new HashSet<Student>();
        }
        //public Guid Id { get; private set; }
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!; // Primary, Secondary //***TODO: an enumeration might be better
        public Address Address { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
