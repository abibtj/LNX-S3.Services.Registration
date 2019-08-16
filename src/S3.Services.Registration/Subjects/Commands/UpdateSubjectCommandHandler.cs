using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using S3.Common.Mongo;
using S3.Common;
using System.Text.RegularExpressions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Subjects.Commands
{
    public class UpdateSubjectCommandHandler : ICommandHandler<UpdateSubjectCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateSubjectCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public UpdateSubjectCommandHandler(IBusPublisher busPublisher, ILogger<UpdateSubjectCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(UpdateSubjectCommand command, ICorrelationContext context)
        {
            // Get existing subject
            var subject = await _db.Subjects.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (subject is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Subject with id: '{command.Id}' was not found.");

            subject.Name = Normalizer.NormalizeSpaces(command.Name);
            subject.SetUpdatedDate();

            await _db.SaveChangesAsync();
        }
    }
}