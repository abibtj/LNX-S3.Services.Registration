using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Subjects.Queries
{
    public class BrowseSubjectsQueryHandler : IQueryHandler<BrowseSubjectsQuery, IEnumerable<SubjectDto>>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public BrowseSubjectsQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<IEnumerable<SubjectDto>> HandleAsync(BrowseSubjectsQuery query)
        {
            var subjects = _mapper.Map<IEnumerable<SubjectDto>>(_db.Subjects);
           
            bool ascending = true;
            if (!string.IsNullOrEmpty(query.SortOrder) &&
                (query.SortOrder.ToLowerInvariant() == "desc" || query.SortOrder.ToLowerInvariant() == "descending"))
            {
                ascending = false;
            }

            if (!string.IsNullOrEmpty(query.OrderBy))
            {
                switch (query.OrderBy.ToLowerInvariant())
                {
                    case "name":
                        subjects = ascending ?
                            subjects.OrderBy(x => x.Name).ToList() :
                            subjects.OrderByDescending(x => x.Name).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                subjects = subjects.OrderByDescending(x => x.UpdatedDate).ToList();
            }

            return subjects;
        }
    }
}
