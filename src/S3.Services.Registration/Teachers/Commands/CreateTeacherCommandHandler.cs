using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using Microsoft.Extensions.Logging;
using S3.Services.Registration.Teachers.Events;
using S3.Common;
using System;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Utility;
using System.Collections.Generic;

namespace S3.Services.Registration.Teachers.Commands
{
    public class CreateTeacherCommandHandler : ICommandHandler<CreateTeacherCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateTeacherCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public CreateTeacherCommandHandler(IBusPublisher busPublisher, ILogger<CreateTeacherCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateTeacherCommand command, ICorrelationContext context)
        {
            // Create a new teacher
            var teacher = new Teacher
            {
                Address = command.Address,
                FirstName = Normalizer.NormalizeSpaces(command.FirstName),
                MiddleName = string.IsNullOrEmpty(command.MiddleName) ? null! : Normalizer.NormalizeSpaces(command.MiddleName),
                LastName = Normalizer.NormalizeSpaces(command.LastName),
                PhoneNumber = command.PhoneNumber,
                Gender = command.Gender,
                Position = command.Position,
                GradeLevel = command.GradeLevel,
                DateOfBirth = command.DateOfBirth,
                SchoolId = command.SchoolId
            };

            if (!(command.RolesArray is null) && command.RolesArray.Length > 0)
                teacher.Roles = string.Join("|", command.RolesArray);

            await _db.Teachers.AddAsync(teacher); // Add to get Id and Address Id

            if (!(command.Address is null))
                teacher.AddressId = teacher.Address.Id;

            //if (!(command.ScoresEntries is null) && command.ScoresEntries.Count > 0)
            //{
            //    var scoresEntryTasks = new List<ScoresEntryTask> { };
            //    foreach (var entry in command.ScoresEntries) // Create new scores entry task
            //    {
            //        if (await _db.ScoresEntryTasks.AnyAsync(x => x.ClassId == entry.ClassId && x.SubjectId == entry.SubjectId))
            //            throw new S3Exception("task_already_exists",
            //                $"The system cannot create duplicate tasks.");

            //        scoresEntryTasks.Add(new ScoresEntryTask
            //        {
            //            ClassId = entry.ClassId,
            //            SubjectId = entry.SubjectId,
            //            TeacherId = teacher.Id
            //        });
            //    }


            //    await _db.ScoresEntryTasks.AddRangeAsync(scoresEntryTasks);
            //}

            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new TeacherCreatedEvent(teacher.Id, 
                command.FirstName + " " + command.LastName, command.Address), context);
        }
    }
}