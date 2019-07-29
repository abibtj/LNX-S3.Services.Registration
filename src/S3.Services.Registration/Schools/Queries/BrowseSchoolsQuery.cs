using S3.Common.Types;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Schools.Queries
{
    public class BrowseSchoolsQuery : PagedQueryBase, IQuery<PagedResult<SchoolDto>>
    {
    }
}