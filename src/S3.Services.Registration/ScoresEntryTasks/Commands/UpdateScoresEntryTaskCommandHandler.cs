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
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.ScoresEntryTasks.Commands
{
    public class UpdateScoresEntryTaskCommandHandler : ICommandHandler<UpdateScoresEntryTaskCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateScoresEntryTaskCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public UpdateScoresEntryTaskCommandHandler(IBusPublisher busPublisher, ILogger<UpdateScoresEntryTaskCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(UpdateScoresEntryTaskCommand command, ICorrelationContext context)
        {
            // Get existing scoresEntryTask
            var scoresEntryTask = await _db.ScoresEntryTasks.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (scoresEntryTask is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"ScoresEntryTask with id: '{command.Id}' was not found.");

            // Check if a task already exists for the class and subject newly specified
            if (await _db.ScoresEntryTasks.AnyAsync(x =>
             (x.SchoolId == command.SchoolId && x.ClassId == command.ClassId && x.Subject == command.Subject && x.Id != command.Id)))
            {
                throw new S3Exception(ExceptionCodes.ItemExists,
                    "A score entry task alredy exists for the specified class and subject.");
            }

            scoresEntryTask.ClassId = command.ClassId;
            scoresEntryTask.ClassName = command.ClassName;
            scoresEntryTask.Description = command.Description;
            scoresEntryTask.DueDate = command.DueDate;
            //scoresEntryTask.SchoolId = command.SchoolId; // This shouldn't change anyway
            scoresEntryTask.Subject = command.Subject;
            scoresEntryTask.TeacherId = command.TeacherId;
            scoresEntryTask.TeacherName = command.TeacherName;
            
            scoresEntryTask.SetUpdatedDate();

            await _db.SaveChangesAsync();

            //await _busPublisher.PublishAsync(new ScoresEntryTaskUpdatedEvent(command.Id, command.Name), context);
        }
    }
}