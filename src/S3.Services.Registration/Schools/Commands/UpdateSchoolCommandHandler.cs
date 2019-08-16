using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using S3.Common.Mongo;
using S3.Common;
using System.Text.RegularExpressions;
using System;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Schools.Commands
{
    public class UpdateSchoolCommandHandler : ICommandHandler<UpdateSchoolCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateSchoolCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public UpdateSchoolCommandHandler(IBusPublisher busPublisher, ILogger<UpdateSchoolCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(UpdateSchoolCommand command, ICorrelationContext context)
        {
            // Get existing school
            var school = await _db.Schools.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (school is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"School with id: '{command.Id}' was not found.");

            school.Name = Normalizer.NormalizeSpaces(command.Name);
            school.Category = command.Category;
            school.SetUpdatedDate();

            school.Address = command.Address;

            await _db.SaveChangesAsync();
        }
    }
}