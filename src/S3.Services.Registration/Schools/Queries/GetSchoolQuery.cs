using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Schools.Queries
{
    public class GetSchoolQuery : IQuery<SchoolDto>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        [JsonConstructor]
        public GetSchoolQuery(Guid id)
        {
            Id = id;
            Name = string.Empty;
        }

        [JsonConstructor]
        public GetSchoolQuery(string name)
        {
            Id = Guid.Empty;
            Name = name;
        }
    }
}