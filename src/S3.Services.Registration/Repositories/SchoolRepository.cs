using S3.Services.Registration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S3.Common.Mongo;
using S3.Common.Types;
using S3.Services.Registration.Schools.Queries;

namespace S3.Services.Registration.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly IMongoRepository<School> _repository;

        public SchoolRepository(IMongoRepository<School> repository)
        {
            _repository = repository;
        }

        public async Task<School> GetAsync(Guid id)
        => await _repository.GetAsync(id);

        public async Task<School> GetAsync(string name)
        => await _repository.GetAsync(x => x.Name == name);

        //public async Task<IEnumerable<School>> BrowseAsync()
        //=> await _repository.BrowseAsync();
        public async Task<PagedResult<School>> BrowseAsync(BrowseSchoolsQuery query)
       => await _repository.BrowseAsync(x => x.Id != Guid.Empty, query);

        public async Task AddAsync(School school)
        => await _repository.AddAsync(school);

        public async Task UpdateAsync(School school)
        => await _repository.UpdateAsync(school);

        public async Task DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);

        public async Task<bool> ExistsAsync(Guid id)
            => await _repository.ExistsAsync(x => x.Id == id);

        public async Task<bool> ExistsAsync(string name)
            => await _repository.ExistsAsync(x => x.Name == name.ToLowerInvariant());
    }
}
