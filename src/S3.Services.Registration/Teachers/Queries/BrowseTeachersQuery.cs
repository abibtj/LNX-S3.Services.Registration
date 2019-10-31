using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using System;
using System.Collections.Generic;

namespace S3.Services.Registration.Teachers.Queries
{
    public class BrowseTeachersQuery : BrowseQuery<Teacher>, IQuery<IEnumerable<TeacherDto>>
    {
        public Guid? SchoolId { get; set; }
        public bool ReturnSignedUpTeachers { get; set; }

        public BrowseTeachersQuery(string[]? includeArray, Guid? schoolId, int page, int results, string orderBy, string sortOrder,
            bool returnSignedUpTeachers)
            : base(includeArray, page, results, orderBy, sortOrder)
           
            => (SchoolId, ReturnSignedUpTeachers) = (schoolId, returnSignedUpTeachers);
    }
}