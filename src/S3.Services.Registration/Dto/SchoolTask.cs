using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Registration.Dto
{
    public class SchoolTaskDto : BaseEntityDto
    {
        public Guid SchoolId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
    }
}
