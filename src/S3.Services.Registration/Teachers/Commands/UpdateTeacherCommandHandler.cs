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
using System.Collections.Generic;
using System.Linq;
using S3.Services.Registration.Domain;

namespace S3.Services.Registration.Teachers.Commands
{
    public class UpdateTeacherCommandHandler : ICommandHandler<UpdateTeacherCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateTeacherCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public UpdateTeacherCommandHandler(IBusPublisher busPublisher, ILogger<UpdateTeacherCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(UpdateTeacherCommand command, ICorrelationContext context)
        {
            // Get existing school
            var teacher = await _db.Teachers.Include(x => x.Address).Include(y => y.ScoresEntryTasks).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (teacher is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Teacher with id: '{command.Id}' was not found.");

            teacher.FirstName = Normalizer.NormalizeSpaces(command.FirstName);
            teacher.MiddleName = string.IsNullOrEmpty(command.MiddleName) ? null! : Normalizer.NormalizeSpaces(command.MiddleName);
            teacher.LastName = Normalizer.NormalizeSpaces(command.LastName);
            teacher.PhoneNumber = command.PhoneNumber;
            teacher.Gender = command.Gender;
            teacher.Position = command.Position;
            teacher.GradeLevel = command.GradeLevel;
            teacher.DateOfBirth = command.DateOfBirth;
            teacher.SchoolId = command.SchoolId;
            teacher.SetUpdatedDate();

            // If the teacher's address has been set to null (remove their existing address from the db (if any))
            if (teacher.Address != null && command.Address == null)
            {
                _db.Address.Remove(teacher.Address);
            }
            teacher.Address = command.Address;

            // If the teacher's score entry task has been set to null (remove their existing score entry tasks from the db (if any))
            if (command.ScoresEntries is null)
            {
                if(teacher.ScoresEntryTasks.Count > 0)
                    _db.ScoresEntryTasks.RemoveRange(teacher.ScoresEntryTasks);
            }
            else if(command.ScoresEntries.Count > 0)
            {
                var existingScoresEntryTasks = _db.ScoresEntryTasks.Where(x => x.TeacherId == command.Id);

                var scoresEntryTasks = new List<ScoresEntryTask> { };

                if (existingScoresEntryTasks.Count() <= 0) // Create new scores entry tasks
                {
                    foreach (var entry in command.ScoresEntries)
                    {
                        if (await _db.ScoresEntryTasks.AnyAsync(x => x.ClassId == entry.ClassId && x.SubjectId == entry.SubjectId))
                            throw new S3Exception("task_already_exists",
                                $"The system cannot create duplicate tasks.");

                        scoresEntryTasks.Add(new ScoresEntryTask
                        {
                            ClassId = entry.ClassId,
                            SubjectId = entry.SubjectId,
                            TeacherId = teacher.Id
                        });
                    }
                }
                else
                {
                    // Some or all of these scores entry tasks have been previously saved
                    // Update the changes and/or create the newly added ones.
                    foreach (var newEntry in command.ScoresEntries)
                    {
                        if (!existingScoresEntryTasks.Any(x => x.ClassId == newEntry.ClassId && x.SubjectId == newEntry.SubjectId)) // Add a new scores entry task
                        {
                            scoresEntryTasks.Add(new ScoresEntryTask
                            {
                                ClassId = newEntry.ClassId,
                                SubjectId = newEntry.SubjectId,
                                TeacherId = teacher.Id
                            });
                        }
                    }

                    foreach (var existingEntry in existingScoresEntryTasks)
                    {
                        if (!command.ScoresEntries.Any(x => x.ClassId == existingEntry.ClassId && x.SubjectId == existingEntry.SubjectId)) // This entry has been removed, so delete it
                        {
                            _db.ScoresEntryTasks.Remove(existingEntry);
                        }
                    }
                }
                
                if(scoresEntryTasks.Count > 0)
                    await _db.ScoresEntryTasks.AddRangeAsync(scoresEntryTasks);
            }

            await _db.SaveChangesAsync();

            //await _busPublisher.PublishAsync(new TeacherUpdatedEvent(command.Id, command.Name), context);
        }
    }
}