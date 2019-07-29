using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;
using S3.Services.Registration.Repositories;

namespace S3.Services.Registration.Schools.Queries
{
    public class BrowseSchoolsQueryHandler : IQueryHandler<BrowseSchoolsQuery, PagedResult<SchoolDto>>
    {
        private readonly ISchoolRepository _schoolRepository;
        public BrowseSchoolsQueryHandler(ISchoolRepository schoolRepository)
        {
           _schoolRepository = schoolRepository;
        }

        public async Task<PagedResult<SchoolDto>> HandleAsync(BrowseSchoolsQuery query)
        {
            var pagedResult = await _schoolRepository.BrowseAsync(query);
            var schools = pagedResult.Items.Select(s => new SchoolDto
            {
                Address = s.Address,
                Category = s.Category,
                CreatedDate = s.CreatedDate,
                Id = s.Id,
                Name = s.Name,
                UpdatedDate = s.UpdatedDate
            });

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
                    case "createddate":
                        schools = ascending ?
                            schools.OrderBy(x => x.CreatedDate).ToList() :
                            schools.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    default:
                        break;
                }
            }
            return PagedResult<SchoolDto>.From(pagedResult, schools);
        }
    }
}