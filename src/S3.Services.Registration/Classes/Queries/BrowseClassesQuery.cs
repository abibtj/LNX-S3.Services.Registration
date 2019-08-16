using S3.Common.Types;
using S3.Services.Registration.Dto;
using System.Collections.Generic;

namespace S3.Services.Registration.Classes.Queries
{
    public class BrowseClassesQuery : PagedQueryBase, IQuery<IEnumerable<ClassDto>>
    {
    }
}