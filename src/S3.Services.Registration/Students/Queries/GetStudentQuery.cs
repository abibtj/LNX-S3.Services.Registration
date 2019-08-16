using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Students.Queries
{
    public class GetStudentQuery : IQuery<StudentDto>
    {
        public Guid Id { get; }

        [JsonConstructor]
        public GetStudentQuery(Guid id) => Id = id;
    }
}