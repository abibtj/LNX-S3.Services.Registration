using System;
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

namespace S3.Services.Registration.Parents.Queries
{
    public class GetParentQueryHandler : IQueryHandler<GetParentQuery, ParentDto>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public GetParentQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<ParentDto> HandleAsync(GetParentQuery query)
        {
            IQueryable<Parent> set = _db.Parents;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<Parent>.IncludeComponents(set, query.IncludeExpressions);

            Parent parent;

            if (query.Id != Guid.Empty) // Getting parent by Id
                parent = await set.FirstOrDefaultAsync(x => x.Id == query.Id);
            else // Searching parent by Registration Number
                parent = await set.FirstOrDefaultAsync(x => x.RegNumber == query.RegNumber.Trim());

            return parent is null ? null! : _mapper.Map<ParentDto>(parent);
        }
    }
}