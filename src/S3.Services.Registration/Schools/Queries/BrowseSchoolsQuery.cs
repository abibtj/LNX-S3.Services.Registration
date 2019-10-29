using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using System.Collections.Generic;

namespace S3.Services.Registration.Schools.Queries
{
    public class BrowseSchoolsQuery : BrowseQuery<School>, IQuery<IEnumerable<SchoolDto>>
    {
        public bool ReturnSchoolStats { get; set; }

        [JsonConstructor]
        public BrowseSchoolsQuery(string[]? includeArray, int page, int results, string orderBy, string sortOrder, bool returnSchoolStats = false)
          : base(includeArray, page, results, orderBy, sortOrder)
            => ReturnSchoolStats = returnSchoolStats;
    }
}