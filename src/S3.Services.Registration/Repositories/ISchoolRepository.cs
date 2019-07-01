using S3.Services.Registration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3.Services.Registration.Repositories
{
    public interface ISchoolRepository
    {
        //Task<School> GetAsync(string name);
        Task<School> GetAsync(Guid id);
        Task<IEnumerable<School>> BrowseAsync();
        Task AddAsync(School school);
        Task UpdateAsync(School school);
        Task DeleteAsync(Guid id);
    }
   
}
