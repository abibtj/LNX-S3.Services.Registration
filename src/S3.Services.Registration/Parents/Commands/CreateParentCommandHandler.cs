using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using Microsoft.Extensions.Logging;
using S3.Services.Registration.Parents.Events;
using S3.Common;
using System;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Parents.Commands
{
    public class CreateParentCommandHandler : ICommandHandler<CreateParentCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateParentCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public CreateParentCommandHandler(IBusPublisher busPublisher, ILogger<CreateParentCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateParentCommand command, ICorrelationContext context)
        {
            // Create a new parent
            var parent = new Parent
            {
                Address = command.Address,
                FirstName = Normalizer.NormalizeSpaces(command.FirstName),
                MiddleName = Normalizer.NormalizeSpaces(command.MiddleName),
                LastName = Normalizer.NormalizeSpaces(command.LastName),
                Gender = command.Gender,
                DateOfBirth = command.DateOfBirth,
                PhoneNumber = command.PhoneNumber
            };

            await _db.Parents.AddAsync(parent);

            // If this parent has some wards, update the wards' ParentId properties to the new parent's Id
            if(command.StudentIds?.Count > 0)
            {
                foreach (var studentId in command.StudentIds)
                {
                    var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
                    if (!(student is null))
                        student.ParentId = parent.Id;
                }
            }

            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new ParentCreatedEvent(parent.Id, 
                command.FirstName + " " + command.LastName, command.Address), context);
        }
    }
}