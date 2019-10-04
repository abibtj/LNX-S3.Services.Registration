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

namespace S3.Services.Registration.Parents.Queries
{
    public class BrowseParentsQueryHandler : IQueryHandler<BrowseParentsQuery, IEnumerable<ParentDto>>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public BrowseParentsQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<IEnumerable<ParentDto>> HandleAsync(BrowseParentsQuery query)
        {
            IQueryable<Parent> set = _db.Parents;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<Parent>.IncludeComponents(set, query.IncludeExpressions);

            var parents = query.SchoolId is null ?
               _mapper.Map<IEnumerable<ParentDto>>(set) :
                    // Get all the parents who have kids in this school.
               _mapper.Map<IEnumerable<ParentDto>>(set.Where(x => x.Students.Any(y => y.SchoolId == query.SchoolId)));

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
                    case "firstname":
                        parents = ascending ?
                            parents.OrderBy(x => x.FirstName).ToList() :
                            parents.OrderByDescending(x => x.FirstName).ToList();
                        break;
                    case "lastname":
                        parents = ascending ?
                            parents.OrderBy(x => x.LastName).ToList() :
                            parents.OrderByDescending(x => x.LastName).ToList();
                        break;
                    case "createddate":
                        parents = ascending ?
                            parents.OrderBy(x => x.CreatedDate).ToList() :
                            parents.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                parents = parents.OrderByDescending(x => x.UpdatedDate).ToList();
            }

            return parents;
        }
    }
}
