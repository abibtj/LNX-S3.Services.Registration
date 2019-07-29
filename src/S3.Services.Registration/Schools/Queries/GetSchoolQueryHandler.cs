using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using S3.Services.Registration.Repositories;

namespace S3.Services.Registration.Schools.Queries
{
    public class GetSchoolQueryHandler : IQueryHandler<GetSchoolQuery, SchoolDto>
    {
        private readonly ISchoolRepository _schoolRepository;
        public GetSchoolQueryHandler(ISchoolRepository schoolRepository)
        {
           _schoolRepository = schoolRepository;
        }
        
        public async Task<SchoolDto> HandleAsync(GetSchoolQuery query)
        {
            var school = query.Name == string.Empty ? 
                await _schoolRepository.GetAsync(query.Id) :
                await _schoolRepository.GetAsync(query.Name);

            if (school is null)
                return null;

            return new SchoolDto
            {
                Address = school.Address,
                Category = school.Category,
                CreatedDate = school.CreatedDate,
                Id = school.Id,
                Name = school.Name,
                UpdatedDate = school.UpdatedDate
            };
        }
    }
}