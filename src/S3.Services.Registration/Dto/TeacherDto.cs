//using S3.Common.Types;
using S3.Services.Registration.Domain;
using System;
using System.Collections.Generic;

namespace S3.Services.Registration.Dto
{
    public class TeacherDto : PersonDto
    {
        public string? Position { get; set; }
        public int? GradeLevel { get; set; }
        public int? Step { get; set; }
        public Guid SchoolId { get; set; }
        public virtual SchoolDto School { get; set; }
        public Guid? AddressId { get; set; }
        public virtual TeacherAddressDto Address { get; set; }

        public virtual ICollection<ScoresEntryTaskDto> ScoresEntryTasks { get; set; }
    }
}