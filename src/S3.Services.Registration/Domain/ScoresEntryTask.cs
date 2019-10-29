using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Registration.Domain
{
    public class ScoresEntryTask : SchoolTask
    {
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public virtual Teacher Teacher{ get; set; }
        public string Subject { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public virtual Class Class { get; set; }
    }
}
