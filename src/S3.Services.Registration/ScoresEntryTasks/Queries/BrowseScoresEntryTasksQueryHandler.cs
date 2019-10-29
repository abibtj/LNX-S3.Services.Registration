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

namespace S3.Services.Registration.ScoresEntryTasks.Queries
{
    public class BrowseScoresEntryTasksQueryHandler : IQueryHandler<BrowseScoresEntryTasksQuery, IEnumerable<ScoresEntryTaskDto>>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public BrowseScoresEntryTasksQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<IEnumerable<ScoresEntryTaskDto>> HandleAsync(BrowseScoresEntryTasksQuery query)
        {
             IQueryable<ScoresEntryTask> set = _db.ScoresEntryTasks;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<ScoresEntryTask>.IncludeComponents(set, query.IncludeExpressions);

            set = query.SchoolId is null ?
                set : set.Where(x => x.SchoolId == query.SchoolId);
           
            set = query.TeacherId is null ?
                set : set.Where(x => x.TeacherId == query.TeacherId);

            var scoresEntryTasks = _mapper.Map<IEnumerable<ScoresEntryTaskDto>>(set);
           
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
                    case "createddate":
                        scoresEntryTasks = ascending ?
                            scoresEntryTasks.OrderBy(x => x.CreatedDate).ToList() :
                            scoresEntryTasks.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                scoresEntryTasks = scoresEntryTasks.OrderByDescending(x => x.UpdatedDate).ToList();
            }

            return scoresEntryTasks;
        }
    }
}
