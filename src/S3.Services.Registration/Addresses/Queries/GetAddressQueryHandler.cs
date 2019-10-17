using System.Collections.Generic;
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

namespace S3.Services.Registration.Addresses.Queries
{
    public class GetAddressQueryHandler : IQueryHandler<GetAddressQuery, AddressDto>
    {
        private readonly IMapper _mapper;
        private readonly RegistrationDbContext _db;

        public GetAddressQueryHandler(RegistrationDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<AddressDto> HandleAsync(GetAddressQuery query)
        {
            var address = await _db.Address.FirstOrDefaultAsync(x => x.Id == query.Id);

            return address is null ? null! : _mapper.Map<AddressDto>(address);
        }
    }
}