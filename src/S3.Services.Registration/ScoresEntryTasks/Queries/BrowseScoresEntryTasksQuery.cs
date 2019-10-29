using System;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace S3.Services.Registration.ScoresEntryTasks.Queries
{
    public class BrowseScoresEntryTasksQuery : BrowseQuery<ScoresEntryTask>, IQuery<IEnumerable<ScoresEntryTaskDto>>
    {
        public Guid? SchoolId { get; set; }
        public Guid? TeacherId { get; set; }

        [JsonConstructor]
        public BrowseScoresEntryTasksQuery(string[]? includeArray, Guid? schoolId, Guid? teacherId, int page, int results, string orderBy, string sortOrder)
            : base(includeArray, page, results, orderBy, sortOrder)
            => (SchoolId, TeacherId) = (schoolId, teacherId);
    }
}