using S3.Common.Types;
using S3.Services.Registration.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Registration.Domain
{
    public class ScoresEntryTaskDto : BaseEntityDto
    {
        public Guid TeacherId { get; set; }
        public virtual TeacherDto Teacher{ get; set; }
        public Guid SubjectId { get; set; }
        //public virtual SubjectDto Subject{ get; set; }
        public Guid ClassId { get; set; }
        //public virtual ClassDto Class{ get; set; }
    }
}
