using S3.Services.Registration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S3.Common.Mongo;

namespace S3.Services.Registration.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly IMongoRepository<School> _repository;

        public SchoolRepository(IMongoRepository<School> repository)
        {
            _repository = repository;
        }

        //public async Task<School> GetAsync(string name)
        //=> await _repository.GetAsync(name);

        public async Task<School> GetAsync(Guid id)
        => await _repository.GetAsync(id);

        //public async Task<IEnumerable<School>> BrowseAsync()
        //=> await _repository.BrowseAsync();
        public Task<IEnumerable<School>> BrowseAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(School school)
        => await _repository.AddAsync(school);

        public async Task UpdateAsync(School school)
        => await _repository.UpdateAsync(school);

        public async Task DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);

       
    }
}
