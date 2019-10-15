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
using System.Collections.Generic;
using System.Linq;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Parents.Commands
{
    public class UpdateParentCommandHandler : ICommandHandler<UpdateParentCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateParentCommandHandler> _logger;
        private readonly RegistrationDbContext _db;

        public UpdateParentCommandHandler(IBusPublisher busPublisher, ILogger<UpdateParentCommandHandler> logger, RegistrationDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(UpdateParentCommand command, ICorrelationContext context)
        {
            // Get existing parent
            var parent = await _db.Parents.Include(x => x.Address).Include(y => y.Students).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (parent is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Parent with id: '{command.Id}' was not found.");

            parent.FirstName = Normalizer.NormalizeSpaces(command.FirstName);
            parent.MiddleName = string.IsNullOrEmpty(command.MiddleName) ? null! : Normalizer.NormalizeSpaces(command.MiddleName);
            parent.LastName = Normalizer.NormalizeSpaces(command.LastName);
            parent.Gender = command.Gender;
            parent.DateOfBirth = command.DateOfBirth;
            parent.PhoneNumber = command.PhoneNumber;

            if (!(command.RolesArray is null) && command.RolesArray.Length > 0)
                parent.Roles = string.Join("|", command.RolesArray);
            else
                parent.Roles = string.Empty;

            // If the address and addressId have been set to null (remove the existing address from the db (if any))
            if (!(parent.Address is null) && command.Address is null && command.AddressId is null)
            {
                _db.Address.Remove(parent.Address);
                parent.AddressId = null;
            }
            else if (parent.Address is null && !(command.Address is null)) // No address before, but there's an address now
            {
                command.Address.ParentId = parent.Id; // Set owner of the address
                await _db.Address.AddAsync(command.Address); // Add address to get an Id
                parent.AddressId = command.Address.Id;
            }
            else if (!(parent.Address is null) && !(command.Address is null)) // An address exists, but changed, modify only the neccessary fields
            {
                parent.Address.Line1 = command.Address.Line1;
                parent.Address.Line2 = command.Address.Line2;
                parent.Address.Town = command.Address.Town;
                parent.Address.State = command.Address.State;
                parent.Address.Country = command.Address.Country;
            }


            // If the updated parent has no ward, nullify the ParentId properties of the existing parent's wards (if any)
            if (command.StudentIds is null || command.StudentIds.Count <= 0)
            {
                if (parent.Students.Count > 0)
                {
                    foreach (var student in parent.Students)
                        student.ParentId = null;
                }
            }
            else // The updated parent has some wards, update the wards' ParentId properties to the updated parent's Id
            {
                if (parent.Students.Count <= 0) // No existing ward for this parent, go ahead and update the wards with this parent
                {
                    foreach (var studentId in command.StudentIds)
                    {
                        var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
                        if (!(student is null))
                            student.ParentId = parent.Id;
                    }
                }
                else // The existing parent has ward(s), some of whom might have been removed, so compare the new list of wards to the existing list wards
                {
                    var existingStudentIds = new HashSet<Guid>(parent.Students.Select(x => x.Id)).ToList();
                    foreach (var studentId in command.StudentIds)
                    {
                        if (!existingStudentIds.Contains(studentId)) // Add this student to this parent
                        {
                            var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
                            if (!(student is null))
                                student.ParentId = parent.Id;
                        }
                    }

                    foreach (var studentId in existingStudentIds)
                    {
                        if (!command.StudentIds.Contains(studentId)) // Remove this student from this parent
                        {
                            var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
                            if (!(student is null))
                                student.ParentId = null;
                        }
                    }
                }
               
            }

            parent.SetUpdatedDate();
            await _db.SaveChangesAsync();

            //await _busPublisher.PublishAsync(new ParentUpdatedEvent(command.Id, command.Name), context);
        }
    }
}