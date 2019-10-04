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

namespace S3.Services.Registration.Teachers.Queries
{
    public class BrowseTeachersQueryHandler : IQueryHandler<BrowseTeachersQuery, IEnumerable<TeacherDto>>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public BrowseTeachersQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<IEnumerable<TeacherDto>> HandleAsync(BrowseTeachersQuery query)
        {
            IQueryable<Teacher> set = _db.Teachers;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<Teacher>.IncludeComponents(set, query.IncludeExpressions);

            var teachers = query.SchoolId is null ?
               _mapper.Map<IEnumerable<TeacherDto>>(set) :
               _mapper.Map<IEnumerable<TeacherDto>>(set.Where(x => x.SchoolId == query.SchoolId));

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
                        teachers = ascending ?
                            teachers.OrderBy(x => x.FirstName).ToList() :
                            teachers.OrderByDescending(x => x.FirstName).ToList();
                        break;
                    case "lastname":
                        teachers = ascending ?
                            teachers.OrderBy(x => x.LastName).ToList() :
                            teachers.OrderByDescending(x => x.LastName).ToList();
                        break;
                    case "createddate":
                        teachers = ascending ?
                            teachers.OrderBy(x => x.CreatedDate).ToList() :
                            teachers.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                teachers = teachers.OrderByDescending(x => x.UpdatedDate).ToList();
            }

            return teachers;
        }
    }
}
