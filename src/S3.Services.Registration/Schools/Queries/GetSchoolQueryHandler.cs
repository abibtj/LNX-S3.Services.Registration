using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Schools.Queries
{
    public class GetSchoolQueryHandler : IQueryHandler<GetSchoolQuery, SchoolDetailsDto>
    {
        private readonly IMongoRepository<School> _schoolRepository;

        public GetSchoolQueryHandler(IMongoRepository<School> schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }
        
        public async Task<SchoolDetailsDto> HandleAsync(GetSchoolQuery query)
        {
            var school = await _schoolRepository.GetAsync(query.Id);
            if (school is null)
            {
                return null;
            }


            return new SchoolDetailsDto
            {
                
            };
        }
    }
}