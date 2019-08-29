using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using S3.Common.Mongo;
using S3.Common;
using S3.Services.Registration.Students.Events;
using System.Linq;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Students.Commands
{
    public class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteStudentCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public DeleteStudentCommandHandler(IBusPublisher busPublisher, ILogger<DeleteStudentCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(DeleteStudentCommand command, ICorrelationContext context)
        {
            var student = await _db.Students.Include(x => x.Parent).ThenInclude(y => y.Students).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (student is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Student with id: '{command.Id}' was not found.");

            _db.Students.Remove(student);

            if (!student.Parent.Students.Any(x => x.Id != student.Id)) // This parent has no more student, remove them
                _db.Parents.Remove(student.Parent);

            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new StudentDeletedEvent(command.Id), context);
        }
    }
}