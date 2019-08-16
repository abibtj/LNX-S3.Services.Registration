
using FluentValidation;
using System;

namespace S3.Services.Registration.Parents.Commands.Validators
{
    public class CreateParentCommandValidator : AbstractValidator<CreateParentCommand>
    {
        public CreateParentCommandValidator()
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

            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Invalid date of birth.");
        }
    }
}
