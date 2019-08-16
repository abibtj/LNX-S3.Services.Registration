
using FluentValidation;
using S3.Services.Registration.Classes.Commands;
using S3.Services.Registration.Domain;

namespace S3.Services.Registration.Utility
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator()
        {
            RuleFor(x => x.SchoolId)
                .NotEmpty().WithMessage("School's Id is required.");
        }
    }
}
