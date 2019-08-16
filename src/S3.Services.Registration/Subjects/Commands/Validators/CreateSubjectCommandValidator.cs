
using FluentValidation;
using System;

namespace S3.Services.Registration.Subjects.Commands.Validators
{
    public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2).WithMessage("Name is too short.")
                .MaximumLength(100).WithMessage("Name is too long. Maximum of 100 characters is allowed.");
        }
    }
}
