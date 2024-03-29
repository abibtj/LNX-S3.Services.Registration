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
using System.Linq;
using System.Collections.Generic;
using S3.Common.Utility;

namespace S3.Services.Registration.ExternalEvents.Handlers
{
    public class SignedUpEventHandler : IEventHandler<SignedUpEvent>
    {
        private readonly RegistrationDbContext _db;
        private readonly ILogger<SignedUpEventHandler> _logger;

        public SignedUpEventHandler(RegistrationDbContext db, ILogger<SignedUpEventHandler> logger)
            => (_db, _logger) = (db, logger);

        public async Task HandleAsync(SignedUpEvent @event, ICorrelationContext context)
        {
            var person = await PersonFinder.FindAsync(@event.Roles.ToList(), @event.UserId, _db);

            if (!(person is null))
            {
                // Check if this teacher has been given the school admin role
                // then update their school's administrator to this teacher
                if (person is Teacher teacher && @event.Roles.ToList().Contains(Role.SchoolAdmin))
                {
                    var school = await _db.Schools.FirstOrDefaultAsync(x => x.Id == teacher.SchoolId);
                    school.AdministratorId = teacher.Id;
                }

                person.IsSignedUp = true;
                person.Roles = string.Join("|", @event.Roles);
                await _db.SaveChangesAsync();

                _logger.LogInformation($"Updated user: '{person.FirstName} {person.LastName}' in the registration db, after a successful sign up in the identity db.");
            }
            else
            {
                _logger.LogInformation($"User with id: '{@event.UserId}', was not found for update in the registration db, after a successful sign up in the identity db.");
            }
        }
    }
}