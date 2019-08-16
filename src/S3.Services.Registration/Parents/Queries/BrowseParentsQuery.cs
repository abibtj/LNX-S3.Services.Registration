using S3.Common.Types;
using S3.Services.Registration.Dto;
using System.Collections.Generic;

namespace S3.Services.Registration.Parents.Queries
{
    public class BrowseParentsQuery : PagedQueryBase, IQuery<IEnumerable<ParentDto>>
    {
    }
}