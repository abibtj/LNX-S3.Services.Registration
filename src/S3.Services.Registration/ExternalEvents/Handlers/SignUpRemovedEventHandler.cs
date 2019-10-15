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

namespace S3.Services.Identity.Users.Events
{
    public class SignUpRemovedEventHandler : IEventHandler<SignUpRemovedEvent>
    {
        private readonly RegistrationDbContext _db;
        private readonly ILogger<SignUpRemovedEventHandler> _logger;

        public SignUpRemovedEventHandler(RegistrationDbContext db, ILogger<SignUpRemovedEventHandler> logger)
            => (_db, _logger) = (db, logger);

        public async Task HandleAsync(SignUpRemovedEvent @event, ICorrelationContext context)
        {
            var person = await _db.Teachers.FirstOrDefaultAsync(x => x.Id == @event.UserId);
           
            if (!(person is null))
            {
                person.IsSignedUp = false;
                person.Roles = string.Empty;
                await _db.SaveChangesAsync();

                _logger.LogInformation($"Updated user: '{person.FirstName} {person.LastName}' in the registration db, after a successful sign up removal in the identity db.");
            }
            else
            {
                _logger.LogInformation($"User with id: '{@event.UserId}', was not found for update in the registration db, after a successful sign up removal in the identity db.");
            }
        }
    }
}