using System;
using S3.Common.Types;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Schools.Queries
{
    public class GetSchoolQuery : IQuery<SchoolDetailsDto>
    {
        public Guid Id { get; set; } 
    }
}