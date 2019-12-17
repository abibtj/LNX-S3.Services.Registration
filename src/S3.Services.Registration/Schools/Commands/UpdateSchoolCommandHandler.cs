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

            // Check for existence of a school with the same name
            if (await _db.Schools.AnyAsync(x => 
            (x.Name.ToLowerInvariant() == Normalizer.NormalizeSpaces(command.Name).ToLowerInvariant())
            && (x.Id != command.Id)))
            {
                throw new S3Exception(ExceptionCodes.NameInUse,
                    $"School name: '{command.Name}' is already in use.");
            }

            school.Name = Normalizer.NormalizeSpaces(command.Name);
            school.Category = command.Category;
            school.Email = command.Email;
            school.PhoneNumber = command.PhoneNumber;

            // Schools must always have an address
            if (!(command.Address is null)) // An address exists, but changed, modify only the neccessary fields
            {
                school.Address.Line1 = command.Address.Line1;
                school.Address.Line2 = command.Address.Line2;
                school.Address.Town = command.Address.Town;
                school.Address.State = command.Address.State;
                school.Address.Country = command.Address.Country;
            }

            school.SetUpdatedDate();

            await _db.SaveChangesAsync();
        }
    }
}