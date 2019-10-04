using System;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using System.Collections.Generic;

namespace S3.Services.Registration.Classes.Queries
{
    public class BrowseClassesQuery : BrowseQuery<Class>, IQuery<IEnumerable<ClassDto>>
    {
        public Guid? SchoolId { get; set; }

        public BrowseClassesQuery(string[]? includeArray, Guid? schoolId, int page, int results, string orderBy, string sortOrder)
           : base(includeArray, page, results, orderBy, sortOrder)
           => SchoolId = schoolId;
    }
}