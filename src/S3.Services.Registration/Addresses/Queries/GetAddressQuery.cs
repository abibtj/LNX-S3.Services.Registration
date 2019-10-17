using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Addresses.Queries
{
    public class GetAddressQuery : GetQuery<Address>, IQuery<AddressDto>
    {
        [JsonConstructor]
        public GetAddressQuery(Guid id) : base(id) { }
    }
}
