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

namespace S3.Services.Registration.ScoresEntryTasks.Commands
{
    public class CreateScoresEntryTaskCommandHandler : ICommandHandler<CreateScoresEntryTaskCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateScoresEntryTaskCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public CreateScoresEntryTaskCommandHandler(IBusPublisher busPublisher, ILogger<CreateScoresEntryTaskCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateScoresEntryTaskCommand command, ICorrelationContext context)
        {
            // Check if a task already exists for the class and subject specified
            if (await _db.ScoresEntryTasks.AnyAsync(x =>
             (x.SchoolId == command.SchoolId && x.ClassId == command.ClassId && x.Subject == command.Subject)))
            {
                throw new S3Exception(ExceptionCodes.ItemExists,
                    "A score entry task alredy exists for the specified class and subject.");
            }

            // Create a new scoresEntryTask
            var scoresEntryTask = new ScoresEntryTask
            {
               ClassId = command.ClassId,
               ClassName = command.ClassName,
               Description = command.Description,
               DueDate = command.DueDate,
               SchoolId = command.SchoolId,
               Subject = command.Subject,
               TeacherId = command.TeacherId,
               TeacherName = command.TeacherName
            };

            await _db.ScoresEntryTasks.AddAsync(scoresEntryTask);

            await _db.SaveChangesAsync();
        }
    }
}