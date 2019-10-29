using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Registration.Domain
{
    public class SchoolTask : BaseEntity
    {
        public Guid SchoolId { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Description { get; set; }
    }
}
