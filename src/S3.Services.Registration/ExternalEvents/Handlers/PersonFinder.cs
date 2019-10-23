using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using S3.Common.Handlers;
using S3.Services.Registration.ExternalEvents;
using S3.Common.RabbitMq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using S3.Services.Registration.Utility;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Teachers.Commands;
using S3.Services.Registration.Domain;
using System.Collections.Generic;
using System.Linq;

namespace S3.Services.Registration.ExternalEvents.Handlers
{
    public class PersonFinder
    {
        public static async Task<Person> FindAsync(List<string> roles, Guid userId, RegistrationDbContext db)
        {
            Person person = null!;

            if (roles.Contains("School Admin") || roles.Contains("Assistant School Admin")
                || roles.Contains("Teacher"))
            {
                person = await db.Teachers.FirstOrDefaultAsync(x => x.Id == userId);
            }
           
            if ((person is null) && roles.Contains("Parent"))
            {
                person = await db.Parents.FirstOrDefaultAsync(x => x.Id == userId);
            }

            if ((person is null) && roles.Contains("Student"))
            {
                person = await db.Students.FirstOrDefaultAsync(x => x.Id == userId);
            }

            return person;
        }
    }
}