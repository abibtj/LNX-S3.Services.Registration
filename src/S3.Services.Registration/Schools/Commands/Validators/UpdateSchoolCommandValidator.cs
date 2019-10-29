
using FluentValidation;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Schools.Commands.Validators
{
    public class UpdateSchoolCommandValidator : AbstractValidator<UpdateSchoolCommand>
    {
        public UpdateSchoolCommandValidator()
        {
            RuleFor(x => x.Id)
                     .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Name)
               .MinimumLength(3).WithMessage("School's name is too short.")
               .MaximumLength(100).WithMessage("School's name is too long. Maximum of 100 characters is allowed.");

            RuleFor(x => x.Category)
               .MinimumLength(3).WithMessage("School's category is too short.")
               .MaximumLength(30).WithMessage("School's category is too long. Maximum of 30 characters is allowed.");

            RuleFor(x => x.Email)
                 .EmailAddress().WithMessage("Invalid email address.")
                 .MaximumLength(30).WithMessage("Email address is too long. Maximum of 30 characters is allowed.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(50).WithMessage("Phone number is too long. Maximum of 30 characters is allowed.");

            RuleFor(x => x.Address)
               .SetValidator(new AddressValidator());
        }
    }
}
