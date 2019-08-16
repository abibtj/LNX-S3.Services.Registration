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
                MiddleName = Normalizer.NormalizeSpaces(command.MiddleName),
                LastName = Normalizer.NormalizeSpaces(command.LastName),
                PhoneNumber = command.PhoneNumber,
                Gender = command.Gender,
                Position = command.Position,
                GradeLevel = command.GradeLevel,
                DateOfBirth = command.DateOfBirth,
                SchoolId = command.SchoolId
            };

            await _db.Teachers.AddAsync(teacher);
            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new TeacherCreatedEvent(teacher.Id, 
                command.FirstName + " " + command.LastName, command.Address), context);
        }
    }
}