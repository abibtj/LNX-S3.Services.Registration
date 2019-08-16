using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using Microsoft.Extensions.Logging;
using S3.Services.Registration.Schools.Events;
using S3.Common;
using System;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Schools.Commands
{
    public class CreateSchoolCommandHandler : ICommandHandler<CreateSchoolCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateSchoolCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public CreateSchoolCommandHandler(IBusPublisher busPublisher, ILogger<CreateSchoolCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateSchoolCommand command, ICorrelationContext context)
        {
            // Check for existence of a school with the same name
            if (await _db.Schools.AnyAsync(x => x.Name.ToLowerInvariant()
            == Normalizer.NormalizeSpaces(command.Name).ToLowerInvariant()))
            {
                throw new S3Exception(ExceptionCodes.SchoolNameInUse,
                    $"School name: '{command.Name}' is already in use.");
            }

            // Create a new school
            var school = new School
            {
                Address = command.Address,
                Category = command.Category,
                Name = Normalizer.NormalizeSpaces(command.Name),
            };

            await _db.Schools.AddAsync(school);
            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new SchoolCreatedEvent(school.Id, 
                command.Name, command.Address), context);
        }
    }
}