using S3.Services.Registration.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3.Services.Registration.Domain.Repositories
{
    public interface ISchoolRepository
    {
        Task<School> GetAsync(string name);
        Task<School> GetAsync(Guid id);
        Task<IEnumerable<School>> GetAllAsync();
        Task AddAsync(School school);
        Task UpdateAsync(School school);
        Task DeleteAsync(Guid id);
    }
}
