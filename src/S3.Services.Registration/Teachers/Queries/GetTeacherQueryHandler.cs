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

namespace S3.Services.Registration.Teachers.Queries
{
    public class GetTeacherQueryHandler : IQueryHandler<GetTeacherQuery, TeacherDto>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public GetTeacherQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<TeacherDto> HandleAsync(GetTeacherQuery query)
        {
            IQueryable<Teacher> set = _db.Teachers;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<Teacher>.IncludeComponents(set, query.IncludeExpressions);

            var teacher = await set.FirstOrDefaultAsync(x => x.Id == query.Id);

            return teacher is null ? null! : _mapper.Map<TeacherDto>(teacher);
        }
    }
}