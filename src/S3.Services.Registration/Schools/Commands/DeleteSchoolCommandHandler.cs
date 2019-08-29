using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using S3.Common.Mongo;
using S3.Common;
using S3.Services.Registration.Schools.Events;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Schools.Commands
{
    public class DeleteSchoolCommandHandler : ICommandHandler<DeleteSchoolCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteSchoolCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public DeleteSchoolCommandHandler(IBusPublisher busPublisher, ILogger<DeleteSchoolCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(DeleteSchoolCommand command, ICorrelationContext context)
        {
            var school = await _db.Schools.Include(x => x.Teachers).Include(y => y.Students).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (school is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"School with id: '{command.Id}' was not found.");

            _db.Schools.Remove(school);

            if (school.Teachers.Count > 0) // Removing manully because no cascade delete has been set
                _db.Teachers.RemoveRange(school.Teachers);

            if (school.Students.Count > 0) // Removing manully because no cascade delete has been set
                _db.Students.RemoveRange(school.Students);

            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new SchoolDeletedEvent(command.Id), context);
        }
    }
}