using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using Microsoft.Extensions.Logging;
using S3.Common;
using System;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Subjects.Commands
{
    public class CreateSubjectCommandHandler : ICommandHandler<CreateSubjectCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateSubjectCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public CreateSubjectCommandHandler(IBusPublisher busPublisher, ILogger<CreateSubjectCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateSubjectCommand command, ICorrelationContext context)
        {
            // Check for existence of a subject with the same name
            if (await _db.Subjects.AnyAsync(x => x.Name.ToLowerInvariant()
            == Normalizer.NormalizeSpaces(command.Name).ToLowerInvariant()))
            {
                throw new S3Exception(ExceptionCodes.SchoolNameInUse,
                    $"Subject: '{command.Name}' already exists.");
            }

            // Create a new subject
            var subject = new Subject
            {
                Name = Normalizer.NormalizeSpaces(command.Name)
            };

            await _db.Subjects.AddAsync(subject);

            await _db.SaveChangesAsync();
        }
    }
}