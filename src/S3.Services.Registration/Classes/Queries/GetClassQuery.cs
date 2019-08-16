using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Classes.Queries
{
    public class GetClassQuery : IQuery<ClassDto>
    {
        public Guid Id { get; }

        [JsonConstructor]
        public GetClassQuery(Guid id) => Id = id;
    }
}