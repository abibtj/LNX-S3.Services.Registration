using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Teachers.Queries
{
    public class GetTeacherQuery : IQuery<TeacherDto>
    {
        public Guid Id { get; }

        [JsonConstructor]
        public GetTeacherQuery(Guid id) => Id = id;
    }
}