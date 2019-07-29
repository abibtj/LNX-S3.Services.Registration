using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Repositories;
using Microsoft.Extensions.Logging;
using S3.Common.Mongo;
using S3.Common;
using S3.Services.Registration.Schools.Events;

namespace S3.Services.Registration.Schools.Commands
{
    public class DeleteSchoolCommandHandler : ICommandHandler<DeleteSchoolCommand>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteSchoolCommandHandler> _logger;

        public DeleteSchoolCommandHandler(ISchoolRepository schoolRepository,
            IBusPublisher busPublisher, ILogger<DeleteSchoolCommandHandler> logger)
        {
            _schoolRepository = schoolRepository;
            _busPublisher = busPublisher;
            _logger = logger;
        }
        
        public async Task HandleAsync(DeleteSchoolCommand command, ICorrelationContext context)
        {
            if (!await _schoolRepository.ExistsAsync(command.Id))
            {
                throw new S3Exception(ExceptionCodes.SchoolNotFound,
                    $"School with id: '{command.Id}' was not found.");
            }

            await _schoolRepository.DeleteAsync(command.Id);

            await _busPublisher.PublishAsync(new SchoolDeletedEvent(command.Id), context);
        }
    }
}