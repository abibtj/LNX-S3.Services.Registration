using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using S3.Common.Mongo;
using S3.Common;
using S3.Services.Registration.Parents.Events;
using System.Linq;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Parents.Commands
{
    public class DeleteParentCommandHandler : ICommandHandler<DeleteParentCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteParentCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public DeleteParentCommandHandler(IBusPublisher busPublisher, ILogger<DeleteParentCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(DeleteParentCommand command, ICorrelationContext context)
        {
            var parent = await _db.Parents.Include(y => y.Students).FirstOrDefaultAsync(z => z.Id == command.Id); // Include the students so that their ParentId properties can be set to null
            if (parent is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Parent with id: '{command.Id}' was not found.");

            _db.Parents.Remove(parent); // The students ParentId properties has been configured to be set to null on parent removal

            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new PersonDeletedEvent(command.Id), context);
        }
    }
}