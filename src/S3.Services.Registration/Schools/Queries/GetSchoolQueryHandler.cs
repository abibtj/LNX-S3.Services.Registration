using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Common.Utility;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Schools.Queries
{
    public class GetSchoolQueryHandler : IQueryHandler<GetSchoolQuery, SchoolDto>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public GetSchoolQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<SchoolDto> HandleAsync(GetSchoolQuery query)
        {
            IQueryable<School> set = _db.Schools;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<School>.IncludeComponents(set, query.IncludeExpressions);

            var school = await set.FirstOrDefaultAsync(x => x.Id == query.Id);

            return school is null ? null! : _mapper.Map<SchoolDto>(school);
        }
    }
}