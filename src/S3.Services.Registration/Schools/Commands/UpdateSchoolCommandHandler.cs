using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Repositories;
using Microsoft.Extensions.Logging;
using S3.Common.Mongo;
using S3.Common;

namespace S3.Services.Registration.Schools.Commands
{
    public class UpdateSchoolCommandHandler : ICommandHandler<UpdateSchoolCommand>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateSchoolCommandHandler> _logger;

        public UpdateSchoolCommandHandler(ISchoolRepository schoolRepository,
            IBusPublisher busPublisher, ILogger<UpdateSchoolCommandHandler> logger)
        {
            _schoolRepository = schoolRepository;
            _busPublisher = busPublisher;
            _logger = logger;
        }
        

        public async Task HandleAsync(UpdateSchoolCommand command, ICorrelationContext context)
        {
            // Get existing school
            var school = await _schoolRepository.GetAsync(command.Id);
            if (school is null)
            {
                throw new S3Exception(ExceptionCodes.SchoolNotFound,
                    $"School with id: '{command.Id}' was not found.");
            }

            school.SetName(command.Name);
            school.SetCategory(command.Category);
            school.SetAddress(command.Address);

            await _schoolRepository.UpdateAsync(school);

            //await _busPublisher.PublishAsync(new SchoolUpdatedEvent(command.Id, command.Name), context);
        }
    }
}