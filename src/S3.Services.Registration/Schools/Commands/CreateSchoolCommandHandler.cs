using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Repositories;
using Microsoft.Extensions.Logging;

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
            // Create a new school
            var school = new School(command.Id, command.Name,
                command.Category, command.Address, command.CreatedAt);
            await _schoolRepository.AddAsync(school);
        }
    }
}