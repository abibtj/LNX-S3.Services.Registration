using S3.Common.Types;
using S3.Services.Registration.Dto;
using System.Collections.Generic;

namespace S3.Services.Registration.Teachers.Queries
{
    public class BrowseTeachersQuery : PagedQueryBase, IQuery<IEnumerable<TeacherDto>>
    {
    }
}