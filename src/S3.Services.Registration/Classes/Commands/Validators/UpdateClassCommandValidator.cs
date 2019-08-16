
using FluentValidation;
using S3.Services.Registration.Classes.Commands;
using S3.Services.Registration.Domain;

namespace S3.Services.Registration.Utility
{
    public class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
        public UpdateClassCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.SchoolId)
                .NotEmpty().WithMessage("School's Id is required.");
        }
    }
}
