using System;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace S3.Services.Registration.Students.Queries
{
    public class BrowseStudentsQuery : BrowseQuery<Student>, IQuery<IEnumerable<StudentDto>>
    {
        public Guid? SchoolId { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? ParentId { get; set; }
        public string? Subject { get; set; }

        [JsonConstructor]
        public BrowseStudentsQuery(string[]? includeArray, Guid? schoolId, Guid? classId, Guid? parentId, string? subject,
            int page, int results, string orderBy, string sortOrder)
            : base(includeArray, page, results, orderBy, sortOrder)

            => (SchoolId, ClassId, ParentId, Subject) 
            = (schoolId, classId, parentId, subject);
    }
}