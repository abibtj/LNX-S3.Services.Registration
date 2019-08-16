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
            var teacher = await _db.Teachers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (teacher is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Teacher with id: '{command.Id}' was not found.");

            teacher.Address = command.Address;
            teacher.FirstName = Normalizer.NormalizeSpaces(command.FirstName);
            teacher.MiddleName = Normalizer.NormalizeSpaces(command.MiddleName);
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
            await _db.SaveChangesAsync();

            //await _busPublisher.PublishAsync(new TeacherUpdatedEvent(command.Id, command.Name), context);
        }
    }
}