using S3.Common;
using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Dto
{
    public class ClassDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Subjects { get; set; }
        public string[] SubjectsArray { get => string.IsNullOrEmpty(Subjects) ? Array.Empty<string>() : Subjects.Split("|"); } // Readonly property, not saved into database

        // Navigation properties
        public Guid SchoolId { get; set; }
        public virtual SchoolDto School { get; set; }
        public Guid? ClassTeacherId { get; set; }
        public virtual TeacherDto ClassTeacher { get; set; }
        public Guid? AssistantTeacherId { get; set; }
        public virtual TeacherDto AssistantTeacher { get; set; }

        public virtual ICollection<StudentDto> Students { get; set; }
    }
}
