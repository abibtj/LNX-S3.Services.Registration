﻿using S3.Common;
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
        public string Category { get; set; }  // Primary, Secondary //***TODO: an enumeration might be better
        public string Subjects { get; set; }

        // Navigation properties 
        public Guid SchoolId { get; set; }
        public virtual School School { get; set; }
        public Guid? ClassTeacherId { get; set; }
        public virtual Teacher ClassTeacher { get; set; }
        public Guid? AssistantTeacherId { get; set; }
        public virtual Teacher AssistantTeacher { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
