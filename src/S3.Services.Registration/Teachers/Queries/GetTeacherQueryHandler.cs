using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common;
using S3.Common.Handlers;
using S3.Common.Mongo;
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
            var teacher = await _db.Teachers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == query.Id);

            if (teacher is null)
                return null!;

            return _mapper.Map<TeacherDto>(teacher);
        }
    }
}