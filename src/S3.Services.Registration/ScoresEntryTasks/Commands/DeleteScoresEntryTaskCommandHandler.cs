using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using S3.Common;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.ScoresEntryTasks.Commands
{
    public class DeleteScoresEntryTaskCommandHandler : ICommandHandler<DeleteScoresEntryTaskCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteScoresEntryTaskCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public DeleteScoresEntryTaskCommandHandler(IBusPublisher busPublisher, ILogger<DeleteScoresEntryTaskCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(DeleteScoresEntryTaskCommand command, ICorrelationContext context)
        {
            var scoresEntryTask = await _db.ScoresEntryTasks.FirstOrDefaultAsync(x => x.Id == command.Id);
            
            if (scoresEntryTask is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"ScoresEntryTask with id: '{command.Id}' was not found.");

            _db.ScoresEntryTasks.Remove(scoresEntryTask);

            await _db.SaveChangesAsync();
        }
    }
}