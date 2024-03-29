using System.Collections.Generic;
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

namespace S3.Services.Registration.Classes.Queries
{
    public class GetClassQueryHandler : IQueryHandler<GetClassQuery, ClassDto>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public GetClassQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<ClassDto> HandleAsync(GetClassQuery query)
        {
            IQueryable<Class> set = _db.Classes;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<Class>.IncludeComponents(set, query.IncludeExpressions);

            var _class = await set.FirstOrDefaultAsync(x => x.Id == query.Id);

            return _class is null ? null! : _mapper.Map<ClassDto>(_class);
        }
    }
}