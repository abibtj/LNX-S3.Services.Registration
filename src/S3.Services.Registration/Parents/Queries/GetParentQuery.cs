using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Parents.Queries
{
    public class GetParentQuery : IQuery<ParentDto>
    {
        public Guid Id { get; }

        [JsonConstructor]
        public GetParentQuery(Guid id) => Id = id;
    }
}