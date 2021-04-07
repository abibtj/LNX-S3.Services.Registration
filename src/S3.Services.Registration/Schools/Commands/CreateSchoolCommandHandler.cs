using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using Microsoft.Extensions.Logging;
using S3.Services.Registration.Schools.Events;
using S3.Common;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Schools.Commands
{
    public class CreateSchoolCommandHandler : ICommandHandler<CreateSchoolCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateSchoolCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public CreateSchoolCommandHandler(IBusPublisher busPublisher, ILogger<CreateSchoolCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateSchoolCommand command, ICorrelationContext context)
        {
            // Check for existence of a school with the same name
            var schoolName = Normalizer.NormalizeSpaces(command.Name).ToLowerInvariant();
            if (await _db.Schools.AnyAsync())
            {
                var existingSchool = _db.Schools.AsEnumerable().FirstOrDefault(x => x.Name.ToLowerInvariant() == schoolName);
                if (!(existingSchool is null))
                {
                    throw new S3Exception(ExceptionCodes.NameInUse,
                        $"School name: '{command.Name}' is already in use.");
                }
            }
            //if (await _db.Schools.AnyAsync(x => x.Name.ToLowerInvariant()
            //== Normalizer.NormalizeSpaces(command.Name).ToLowerInvariant()))
            //{
            //    throw new S3Exception(ExceptionCodes.NameInUse,
            //        $"School name: '{command.Name}' is already in use.");
            //}

            // Create a new school
            var school = new School
            {
                Address = command.Address,
                Category = command.Category,
                Name = Normalizer.NormalizeSpaces(command.Name),
                Email = command.Email,
                PhoneNumber = command.PhoneNumber
            };

            await _db.Schools.AddAsync(school); // Add to get Id and Address Id

            if (!(command.Address is null))
                school.AddressId = school.Address.Id;

            await _db.SaveChangesAsync();

            await _busPublisher.PublishAsync(new SchoolCreatedEvent(school.Id, 
                command.Name, command.Address), context);
        }
    }
}