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

namespace S3.Services.Registration.Students.Commands
{
    public class UpdateStudentCommandHandler : ICommandHandler<UpdateStudentCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateStudentCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public UpdateStudentCommandHandler(IBusPublisher busPublisher, ILogger<UpdateStudentCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(UpdateStudentCommand command, ICorrelationContext context)
        {
            // Get existing student
            var student = await _db.Students.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (student is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Student with id: '{command.Id}' was not found.");

            student.FirstName = Normalizer.NormalizeSpaces(command.FirstName);
            student.MiddleName = string.IsNullOrEmpty(command.MiddleName) ? null! : Normalizer.NormalizeSpaces(command.MiddleName);
            student.LastName = Normalizer.NormalizeSpaces(command.LastName);
            student.Gender = command.Gender;
            student.DateOfBirth = command.DateOfBirth;
            student.PhoneNumber = command.PhoneNumber;
            student.SchoolId = command.SchoolId;
            student.ClassId = command.ClassId;
            student.ParentId = command.ParentId;
            student.OfferingAllClassSubjects = command.OfferingAllClassSubjects;

            if (student.OfferingAllClassSubjects)
            {
                student.Subjects = string.Empty;
            }
            else // If the student is not offering all the class subject, SubjectsArray must not be empty.
            {
                if (!(command.SubjectsArray is null) && command.SubjectsArray.Length > 0)
                {
                    student.Subjects = string.Join("|", command.SubjectsArray);
                }
                else
                {
                    student.OfferingAllClassSubjects = true;
                    student.Subjects = string.Empty;
                }
            }

            // If the student's address has been set to null (remove their existing address from the db (if any))
            if (student.Address != null && command.Address == null)
            {
                _db.Address.Remove(student.Address);
            }
            student.Address = command.Address;
            student.SetUpdatedDate();

            await _db.SaveChangesAsync();

            //await _busPublisher.PublishAsync(new StudentUpdatedEvent(command.Id, command.Name), context);
        }
    }
}