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

namespace S3.Services.Registration.Classes.Queries
{
    public class BrowseClassesQueryHandler : IQueryHandler<BrowseClassesQuery, IEnumerable<ClassDto>>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public BrowseClassesQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<IEnumerable<ClassDto>> HandleAsync(BrowseClassesQuery query)
        {
            IQueryable<Class> set = _db.Classes;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<Class>.IncludeComponents(set, query.IncludeExpressions);

            var classes = query.SchoolId is null ?
               _mapper.Map<IEnumerable<ClassDto>>(set) :
               _mapper.Map<IEnumerable<ClassDto>>(set.Where(x => x.SchoolId == query.SchoolId));

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
                        classes = ascending ?
                            classes.OrderBy(x => x.Name).ToList() :
                            classes.OrderByDescending(x => x.Name).ToList();
                        break;
                    case "createddate":
                        classes = ascending ?
                            classes.OrderBy(x => x.CreatedDate).ToList() :
                            classes.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                classes = classes.OrderByDescending(x => x.UpdatedDate).ToList();
            }

            return classes;
        }
    }
}
