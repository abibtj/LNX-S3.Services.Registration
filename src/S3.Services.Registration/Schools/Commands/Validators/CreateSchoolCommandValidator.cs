﻿
using FluentValidation;

namespace S3.Services.Registration.Schools.Commands.Validators
{
    public class CreateSchoolCommandValidator : AbstractValidator<CreateSchoolCommand>
    {
        public CreateSchoolCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("School's name is too short.")
                .MaximumLength(100).WithMessage("School's name is too long. Maximum of 100 characters is allowed.");

            RuleFor(x => x.Category)
               .MinimumLength(3).WithMessage("School's category is too short.")
               .MaximumLength(30).WithMessage("School's category is too long. Maximum of 30 characters is allowed.");
        }
    }
}
