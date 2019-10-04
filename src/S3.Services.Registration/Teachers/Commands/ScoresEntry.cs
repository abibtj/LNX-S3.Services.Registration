using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Common.Types;
using S3.Services.Registration.Domain;

namespace S3.Services.Registration.Teachers.Commands
{
    public class ScoresEntry
    {
        [Required]
        public Guid SubjectId { get; set; }
        [Required]
        public Guid? ClassId { get; set; }
    }
}