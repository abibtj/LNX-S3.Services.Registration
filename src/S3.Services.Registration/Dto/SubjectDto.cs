using S3.Common;
using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace S3.Services.Registration.Dto
{
    public class SubjectDto : BaseDto
    {
        public string Name { get; set; }
    }
}
