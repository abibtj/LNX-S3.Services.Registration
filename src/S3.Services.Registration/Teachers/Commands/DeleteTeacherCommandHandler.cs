using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using S3.Common.Mongo;
using S3.Common;
using S3.Services.Registration.Teachers.Events;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Teachers.Commands
{
    public class DeleteTeacherCommandHandler : ICommandHandler<DeleteTeacherCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteTeacherCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public DeleteTeacherCommandHandler(IBusPublisher busPublisher, ILogger<DeleteTeacherCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(DeleteTeacherCommand command, ICorrelationContext context)
        {
            var teacher = await _db.Teachers.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (teacher is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Teacher with id: '{command.Id}' was not found.");

            _db.Teachers.Remove(teacher);

            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new TeacherDeletedEvent(command.Id), context);
        }
    }
}