using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using System;
using System.Collections.Generic;

namespace S3.Services.Registration.Parents.Queries
{
    public class BrowseParentsQuery : BrowseQuery<Parent>, IQuery<IEnumerable<ParentDto>>
    {
        public Guid? SchoolId { get; set; }

        [JsonConstructor]
        public BrowseParentsQuery(string[]? includeArray, Guid? schoolId, int page, int results, string orderBy, string sortOrder)
           : base(includeArray, page, results, orderBy, sortOrder)
            => SchoolId = schoolId;
    }
}