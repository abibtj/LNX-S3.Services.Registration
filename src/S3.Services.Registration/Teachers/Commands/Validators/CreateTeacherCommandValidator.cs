
using FluentValidation;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Teachers.Commands.Validators
{
    public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .MinimumLength(2).WithMessage("First name is too short.")
                .MaximumLength(30).WithMessage("First name is too long. Maximum of 30 characters is allowed.");

            RuleFor(x => x.MiddleName)
                .MinimumLength(2).WithMessage("Middle name is too short.")
                .MaximumLength(30).WithMessage("Middle name is too long. Maximum of 30 characters is allowed.");

            RuleFor(x => x.LastName)
               .MinimumLength(2).WithMessage("Last name is too short.")
               .MaximumLength(30).WithMessage("Last name is too long. Maximum of 30 characters is allowed.");

            RuleFor(x => x.SchoolId)
                  .NotEmpty().WithMessage("School's Id is required.");

            RuleFor(x => x.Address).SetValidator(new AddressValidator());

            RuleForEach(x => x.ScoresEntries).SetValidator(new ScoresEntryValidator());
        }
    }
}
