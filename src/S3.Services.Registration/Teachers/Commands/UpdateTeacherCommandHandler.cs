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
            // Get existing teacher
            var teacher = await _db.Teachers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == command.Id);
            //var teacher = await _db.Teachers.Include(x => x.Address).Include(y => y.ScoresEntryTasks).FirstOrDefaultAsync(x => x.Id == command.Id);
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

            if (!(command.RolesArray is null) && command.RolesArray.Length > 0)
                teacher.Roles = string.Join("|", command.RolesArray);
            else
                teacher.Roles = string.Empty;

            // If the address and addressId have been set to null (remove the existing address from the db (if any))
            if (!(teacher.Address is null) && command.Address is null && command.AddressId is null)
            {
                _db.Address.Remove(teacher.Address);
                teacher.AddressId = null;
            }
            else if (teacher.Address is null && !(command.Address is null)) // No address before, but there's an address now
            {
                command.Address.TeacherId = teacher.Id; // Set owner of the address
                await _db.Address.AddAsync(command.Address); // Add address to get an Id
                teacher.AddressId = command.Address.Id;
            }
            else if (!(teacher.Address is null) && !(command.Address is null)) // An address exists, but changed, modify only the neccessary fields
            {
                teacher.Address.Line1 = command.Address.Line1;
                teacher.Address.Line2 = command.Address.Line2;
                teacher.Address.Town = command.Address.Town;
                teacher.Address.State = command.Address.State;
                teacher.Address.Country = command.Address.Country;
            }

            //// If the teacher's score entry task has been set to null (remove their existing score entry tasks from the db (if any))
            //if (command.ScoresEntries is null)
            //{
            //    if(teacher.ScoresEntryTasks.Count > 0)
            //        _db.ScoresEntryTasks.RemoveRange(teacher.ScoresEntryTasks);
            //}
            //else if(command.ScoresEntries.Count > 0)
            //{
            //    var existingScoresEntryTasks = _db.ScoresEntryTasks.Where(x => x.TeacherId == command.Id);

            //    var scoresEntryTasks = new List<ScoresEntryTask> { };

            //    if (existingScoresEntryTasks.Count() <= 0) // Create new scores entry tasks
            //    {
            //        foreach (var entry in command.ScoresEntries)
            //        {
            //            if (await _db.ScoresEntryTasks.AnyAsync(x => x.ClassId == entry.ClassId && x.SubjectId == entry.SubjectId))
            //                throw new S3Exception("task_already_exists",
            //                    $"The system cannot create duplicate tasks.");

            //            scoresEntryTasks.Add(new ScoresEntryTask
            //            {
            //                ClassId = entry.ClassId,
            //                SubjectId = entry.SubjectId,
            //                TeacherId = teacher.Id
            //            });
            //        }
            //    }
            //    else
            //    {
            //        // Some or all of these scores entry tasks have been previously saved
            //        // Update the changes and/or create the newly added ones.
            //        foreach (var newEntry in command.ScoresEntries)
            //        {
            //            if (!existingScoresEntryTasks.Any(x => x.ClassId == newEntry.ClassId && x.SubjectId == newEntry.SubjectId)) // Add a new scores entry task
            //            {
            //                scoresEntryTasks.Add(new ScoresEntryTask
            //                {
            //                    ClassId = newEntry.ClassId,
            //                    SubjectId = newEntry.SubjectId,
            //                    TeacherId = teacher.Id
            //                });
            //            }
            //        }

            //        foreach (var existingEntry in existingScoresEntryTasks)
            //        {
            //            if (!command.ScoresEntries.Any(x => x.ClassId == existingEntry.ClassId && x.SubjectId == existingEntry.SubjectId)) // This entry has been removed, so delete it
            //            {
            //                _db.ScoresEntryTasks.Remove(existingEntry);
            //            }
            //        }
            //    }
                
            //    if(scoresEntryTasks.Count > 0)
            //        await _db.ScoresEntryTasks.AddRangeAsync(scoresEntryTasks);
            //}

            teacher.SetUpdatedDate();

            await _db.SaveChangesAsync();

            //await _busPublisher.PublishAsync(new TeacherUpdatedEvent(command.Id, command.Name), context);
        }
    }
}