using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Subjects.Queries
{
    public class GetSubjectQueryHandler : IQueryHandler<GetSubjectQuery, SubjectDto>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public GetSubjectQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<SubjectDto> HandleAsync(GetSubjectQuery query)
        {
            var subject = await _db.Subjects.FirstOrDefaultAsync(x => x.Id == query.Id);

            if (subject is null)
                return null!;

            return _mapper.Map<SubjectDto>(subject);
        }
    }
}