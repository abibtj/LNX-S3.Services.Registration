
using FluentValidation;
using System;

namespace S3.Services.Registration.Subjects.Commands.Validators
{
    public class UpdateSubjectCommandValidator : AbstractValidator<UpdateSubjectCommand>
    {
        public UpdateSubjectCommandValidator()
        {
            RuleFor(x => x.Id)
                  .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Name)
                .MinimumLength(2).WithMessage("Name is too short.")
                .MaximumLength(100).WithMessage("Name is too long. Maximum of 100 characters is allowed.");
        }
    }
}
