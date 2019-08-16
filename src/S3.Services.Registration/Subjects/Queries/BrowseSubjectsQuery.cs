using S3.Common.Types;
using S3.Services.Registration.Dto;
using System.Collections.Generic;

namespace S3.Services.Registration.Subjects.Queries
{
    public class BrowseSubjectsQuery : PagedQueryBase, IQuery<IEnumerable<SubjectDto>>
    {
    }
}