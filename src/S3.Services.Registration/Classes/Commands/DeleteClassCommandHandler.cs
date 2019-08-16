using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using S3.Common.Mongo;
using S3.Common;
using S3.Services.Registration.Classes.Events;
using System.Linq;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Classes.Commands
{
    public class DeleteClassCommandHandler : ICommandHandler<DeleteClassCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteClassCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public DeleteClassCommandHandler(IBusPublisher busPublisher, ILogger<DeleteClassCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(DeleteClassCommand command, ICorrelationContext context)
        {
            var _class = await _db.Classes.Include(x => x.Students).FirstOrDefaultAsync(y => y.Id == command.Id);
            if (_class is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Class with id: '{command.Id}' was not found.");

            _db.Classes.Remove(_class); // The students ClassId properties has been configured to be set to null on _class removal

            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new ClassDeletedEvent(command.Id), context);
        }
    }
}