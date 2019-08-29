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

namespace S3.Services.Registration.Students.Queries
{
    public class BrowseStudentsQueryHandler : IQueryHandler<BrowseStudentsQuery, IEnumerable<StudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public BrowseStudentsQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<IEnumerable<StudentDto>> HandleAsync(BrowseStudentsQuery query)
        {
            var students = query.SchoolId is null ?
                _mapper.Map<IEnumerable<StudentDto>>(_db.Students.Include(x => x.Address).Include(y => y.Class).Include(z => z.Parent).AsEnumerable()):
                _mapper.Map<IEnumerable<StudentDto>>(_db.Students.Where(x => x.SchoolId == query.SchoolId).Include(x => x.Address).Include(y => y.Class).Include(z => z.Parent).AsEnumerable());
           
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
                        students = ascending ?
                            students.OrderBy(x => x.FirstName).ToList() :
                            students.OrderByDescending(x => x.FirstName).ToList();
                        break;
                    case "lastname":
                        students = ascending ?
                            students.OrderBy(x => x.LastName).ToList() :
                            students.OrderByDescending(x => x.LastName).ToList();
                        break;
                    case "createddate":
                        students = ascending ?
                            students.OrderBy(x => x.CreatedDate).ToList() :
                            students.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    default:
                        break;
                }
            }
            return students;
        }
    }
}
