using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Parents.Queries
{
    public class GetParentQuery : GetQuery<Parent>, IQuery<ParentDto>
    {
        public string RegNumber { get; set; } = string.Empty;

        [JsonConstructor]
        public GetParentQuery(Guid id, string[]? includeArray) : base(id, includeArray) { }

        [JsonConstructor]
        public GetParentQuery(string regNumber) : base(id: Guid.Empty)
            => RegNumber = regNumber;
    }
}
