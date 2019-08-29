using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using Microsoft.Extensions.Logging;
using S3.Services.Registration.Students.Events;
using S3.Common;
using System;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Students.Commands
{
    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateStudentCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public CreateStudentCommandHandler(IBusPublisher busPublisher, ILogger<CreateStudentCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateStudentCommand command, ICorrelationContext context)
        {
            // Create a new student
            var student = new Student
            {
                Address = command.Address,
                FirstName = Normalizer.NormalizeSpaces(command.FirstName),
                MiddleName = string.IsNullOrEmpty(command.MiddleName)? null! : Normalizer.NormalizeSpaces(command.MiddleName),
                LastName = Normalizer.NormalizeSpaces(command.LastName),
                Gender = command.Gender,
                DateOfBirth = command.DateOfBirth,
                PhoneNumber = command.PhoneNumber,
                SchoolId = command.SchoolId,
                ClassId = command.ClassId,
                ParentId = command.ParentId,
                SubjectIds = command.SubjectIds
                //SubjectIds = string.Join(",", command.SubjectIds)
            };

            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new StudentCreatedEvent(student.Id, 
                command.FirstName + " " + command.LastName, command.Address), context);
        }
    }
}