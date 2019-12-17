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

namespace S3.Services.Registration.Classes.Commands
{
    public class UpdateClassCommandHandler : ICommandHandler<UpdateClassCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateClassCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public UpdateClassCommandHandler(IBusPublisher busPublisher, ILogger<UpdateClassCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(UpdateClassCommand command, ICorrelationContext context)
        {
            if (command.SubjectsArray.Length < 1)
                throw new S3Exception("subjects_required", "Subjects are required to create a class.");

            // Get existing _class
            var _class = await _db.Classes.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (_class is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Class with id: '{command.Id}' was not found.");

            // Check for existence of a class with the same name in this school.
            if (await _db.Classes.AnyAsync(x => (x.SchoolId == command.SchoolId) &&
                (x.Name.ToLowerInvariant() == Normalizer.NormalizeSpaces(command.Name).ToLowerInvariant())
                && (x.Id != command.Id)))
            {
                throw new S3Exception(ExceptionCodes.NameInUse,
                    $"Class name: '{command.Name}' is already in use.");
            }

            _class.Name = Normalizer.NormalizeSpaces(command.Name);
            _class.Category = Normalizer.NormalizeSpaces(command.Category);
            _class.SchoolId = command.SchoolId;
            _class.ClassTeacherId = command.TeacherId;
            _class.Subjects = string.Join("|", command.SubjectsArray);
            _class.SetUpdatedDate();

            //// If the updated _class has no student, nullify the ClassId properties of the existing _class's students (if any)
            //if (command.StudentIds is null || command.StudentIds.Count <= 0)
            //{
            //    if (_class.Students.Count > 0)
            //    {
            //        foreach (var student in _class.Students)
            //            student.ClassId = null;
            //    }
            //}
            //else // The updated _class has some students, update the students' ClassId properties to the updated _class's Id
            //{
            //    if (_class.Students.Count <= 0) // No existing ward for this _class, go ahead and update the wards with this _class
            //    {
            //        foreach (var studentId in command.StudentIds)
            //        {
            //            var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
            //            if (!(student is null))
            //                student.ClassId = _class.Id;
            //        }
            //    }
            //    else // The existing _class has student(s), some of whom might have been removed, so compare the new list of student to the existing list student
            //    {
            //        var existingStudentIds = new HashSet<Guid>(_class.Students.Select(x => x.Id)).ToList();
            //        foreach (var studentId in command.StudentIds)
            //        {
            //            if (!existingStudentIds.Contains(studentId)) // Add this student to this _class
            //            {
            //                var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
            //                if (!(student is null))
            //                    student.ClassId = _class.Id;
            //            }
            //        }

            //        foreach (var studentId in existingStudentIds)
            //        {
            //            if (!command.StudentIds.Contains(studentId)) // Remove this student from this _class
            //            {
            //                var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
            //                if (!(student is null))
            //                    student.ClassId = null;
            //            }
            //        }
            //    }
               
            //}

            await _db.SaveChangesAsync();

            //await _busPublisher.PublishAsync(new ClassUpdatedEvent(command.Id, command.Name), context);
        }
    }
}