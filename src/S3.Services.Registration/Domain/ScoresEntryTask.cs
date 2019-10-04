using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Registration.Domain
{
    public class ScoresEntryTask : BaseEntity
    {
        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher{ get; set; }
        public Guid SubjectId { get; set; }
        //public virtual Subject Subject{ get; set; }
        public Guid? ClassId { get; set; }
        //public virtual Class Class{ get; set; }
    }
}
