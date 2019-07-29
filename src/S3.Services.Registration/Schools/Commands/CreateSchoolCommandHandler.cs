using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Repositories;
using Microsoft.Extensions.Logging;
using S3.Common.Mongo;
using S3.Services.Registration.Schools.Events;
using S3.Common;

namespace S3.Services.Registration.Schools.Commands
{
    public class CreateSchoolCommandHandler : ICommandHandler<CreateSchoolCommand>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateSchoolCommandHandler> _logger;

        public CreateSchoolCommandHandler(ISchoolRepository schoolRepository,
            IBusPublisher busPublisher, ILogger<CreateSchoolCommandHandler> logger)
        {
            _schoolRepository = schoolRepository;
            _busPublisher = busPublisher;
            _logger = logger;
        }
        

        public async Task HandleAsync(CreateSchoolCommand command, ICorrelationContext context)
        {
            // Check for existence of a school with the same name
            if (await _schoolRepository.ExistsAsync(command.Name))
            {
                throw new S3Exception(ExceptionCodes.SchoolNameInUse,
                    $"Name: '{command.Name}' is already in use.");
            }

            // Create a new school
            var newSchool = new School(command.Id, command.Name,
                command.Category, command.Address);
            await _schoolRepository.AddAsync(newSchool);

            await _busPublisher.PublishAsync(new SchoolCreatedEvent(command.Id, 
                command.Name, command.Address), context);
        }
    }
}