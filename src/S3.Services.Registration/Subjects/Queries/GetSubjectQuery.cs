using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Subjects.Queries
{
    public class GetSubjectQuery : IQuery<SubjectDto>
    {
        public Guid Id { get; }

        [JsonConstructor]
        public GetSubjectQuery(Guid id) => Id = id;
    }
}