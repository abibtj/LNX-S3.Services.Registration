using S3.Common;
using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Name { get; set; }
        public string Category { get; set; }  // Primary, Secondary //***TODO: an enumeration might be better
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? AddressId { get; set; }
        public virtual SchoolAddress Address { get; set; }
        public Guid? AdministratorId { get; set; }
        [ForeignKey("AdministratorId")]
        public virtual Teacher Administrator { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
