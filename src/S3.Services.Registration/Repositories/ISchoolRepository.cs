using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Schools.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3.Services.Registration.Repositories
{
    public interface ISchoolRepository
    {
        Task AddAsync(School school);
        Task<PagedResult<School>> BrowseAsync(BrowseSchoolsQuery query);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<School> GetAsync(Guid id);
        Task<School> GetAsync(string name);
        Task UpdateAsync(School school);
    }
   
}
