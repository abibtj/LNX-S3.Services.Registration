using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Schools.Queries
{
    public class GetSchoolQuery : IQuery<SchoolDto>
    {
        public Guid Id { get; }
        public string Name { get; }

        [JsonConstructor]
        public GetSchoolQuery(Guid id) => (Id, Name) = (id, string.Empty);

        [JsonConstructor]
        public GetSchoolQuery(string name) => (Id, Name) = (Guid.Empty, name);
    }
}