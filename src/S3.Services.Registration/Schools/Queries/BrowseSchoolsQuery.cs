using S3.Common.Types;
using S3.Services.Registration.Dto;
using System.Collections.Generic;

namespace S3.Services.Registration.Schools.Queries
{
    public class BrowseSchoolsQuery : PagedQueryBase, IQuery<IEnumerable<SchoolDto>>
    {
    }
}