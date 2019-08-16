using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using S3.Common.Mongo;
using S3.Common;
using System.Linq;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Subjects.Commands
{
    public class DeleteSubjectCommandHandler : ICommandHandler<DeleteSubjectCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteSubjectCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public DeleteSubjectCommandHandler(IBusPublisher busPublisher, ILogger<DeleteSubjectCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(DeleteSubjectCommand command, ICorrelationContext context)
        {
            var subject = await _db.Subjects.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (subject is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Subject with id: '{command.Id}' was not found.");

            _db.Subjects.Remove(subject); 

            await _db.SaveChangesAsync();
        }
    }
}