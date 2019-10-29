using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.ScoresEntryTasks.Queries
{
    public class GetScoresEntryTaskQuery : GetQuery<ScoresEntryTask>, IQuery<ScoresEntryTaskDto>
    {
        [JsonConstructor]
        public GetScoresEntryTaskQuery(Guid id, string[]? includeArray) : base(id, includeArray) { }
    }
}
