using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using Microsoft.Extensions.Logging;
using S3.Services.Registration.Classes.Events;
using S3.Common;
using System;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Classes.Commands
{
    public class CreateClassCommandHandler : ICommandHandler<CreateClassCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateClassCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public CreateClassCommandHandler(IBusPublisher busPublisher, ILogger<CreateClassCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateClassCommand command, ICorrelationContext context)
        {
            // Create a new _class
            var _class = new Class
            {
                Name = Normalizer.NormalizeSpaces(command.Name),
                SchoolId = command.SchoolId,
                ClassTeacherId = command.TeacherId,
                SubjectIds = string.Join(",", command.SubjectIds)
            };

            await _db.Classes.AddAsync(_class);

            // If this _class has some students, update the students' ClassId properties to the new _class's Id
            if(command.StudentIds?.Count > 0)
            {
                foreach (var studentId in command.StudentIds)
                {
                    var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
                    if (!(student is null))
                        student.ClassId = _class.Id;
                }
            }

            await _db.SaveChangesAsync();

            var school = await _db.Schools.FirstOrDefaultAsync(x => x.Id == command.SchoolId);

            await _busPublisher.PublishAsync(new ClassCreatedEvent(_class.Id, 
               command.Name, school), context);
        }
    }
}