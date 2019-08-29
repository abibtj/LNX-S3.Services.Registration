using System;
using S3.Common.Types;
using S3.Services.Registration.Dto;
using System.Collections.Generic;

namespace S3.Services.Registration.Students.Queries
{
    public class BrowseStudentsQuery : PagedQueryBase, IQuery<IEnumerable<StudentDto>>
    {
        public Guid? SchoolId { get; set; }
    }
}