using S3.Common.Types;
using S3.Services.Registration.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Registration.Domain
{
    public class ScoresEntryTaskDto : SchoolTaskDto
    {
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public virtual TeacherDto Teacher { get; set; }
        public string Subject { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public virtual ClassDto Class { get; set; }
        public Guid RuleId { get; set; } // The rule to be used to determine the weight of the scores and grades obtained in this subject
    }
}