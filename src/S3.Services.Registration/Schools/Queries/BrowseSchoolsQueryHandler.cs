using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Common.Types;
using S3.Common.Utility;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Schools.Queries
{
    public class BrowseSchoolsQueryHandler : IQueryHandler<BrowseSchoolsQuery, IEnumerable<SchoolDto>>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public BrowseSchoolsQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<IEnumerable<SchoolDto>> HandleAsync(BrowseSchoolsQuery query)
        {
            IQueryable<School> set = _db.Schools;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<School>.IncludeComponents(set, query.IncludeExpressions);

            var schools = _mapper.Map<IEnumerable<SchoolDto>>(set);
           
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
                        schools = ascending ?
                            schools.OrderBy(x => x.Name).ToList() :
                            schools.OrderByDescending(x => x.Name).ToList();
                        break;
                    case "category":
                        schools = ascending ?
                            schools.OrderBy(x => x.Category).ToList() :
                            schools.OrderByDescending(x => x.Category).ToList();
                        break;
                    case "address":
                        schools = ascending ?
                            schools.OrderBy(x => x.Address.State).ToList() :
                            schools.OrderByDescending(x => x.Address.State).ToList();
                        break;
                    case "createddate":
                        schools = ascending ?
                            schools.OrderBy(x => x.CreatedDate).ToList() :
                            schools.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                schools = schools.OrderByDescending(x => x.UpdatedDate).ToList();
            }

            return schools;
        }
    }
}
