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

namespace S3.Services.Registration.ScoresEntryTasks.Queries
{
    public class GetScoresEntryTaskQueryHandler : IQueryHandler<GetScoresEntryTaskQuery, ScoresEntryTaskDto>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public GetScoresEntryTaskQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<ScoresEntryTaskDto> HandleAsync(GetScoresEntryTaskQuery query)
        {
            IQueryable<ScoresEntryTask> set = _db.ScoresEntryTasks;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<ScoresEntryTask>.IncludeComponents(set, query.IncludeExpressions);

            var scoresEntryTask = await set.FirstOrDefaultAsync(x => x.Id == query.Id);

            return scoresEntryTask is null ? null! : _mapper.Map<ScoresEntryTaskDto>(scoresEntryTask);
        }
    }
}