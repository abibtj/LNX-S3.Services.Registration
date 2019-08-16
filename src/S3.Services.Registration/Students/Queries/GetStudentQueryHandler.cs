using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Students.Queries
{
    public class GetStudentQueryHandler : IQueryHandler<GetStudentQuery, StudentDto>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public GetStudentQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<StudentDto> HandleAsync(GetStudentQuery query)
        {
            var student = await _db.Students.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == query.Id);

            if (student is null)
                return null!;

            return _mapper.Map<StudentDto>(student);
        }
    }
}