
using FluentValidation;
using S3.Services.Registration.Domain;

namespace S3.Services.Registration.Utility
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
           RuleFor(x => x.Line1)
               .MinimumLength(3).WithMessage("Address first line is too short.")
               .MaximumLength(100).WithMessage("Address first line is too long. Maximum of 100 characters is allowed.");

            RuleFor(x => x.Town)
                .MinimumLength(2).WithMessage("Town name is too short.")
                .MaximumLength(30).WithMessage("Town name is too long. Maximum of 30 characters is allowed.");

            RuleFor(x => x.State)
               .MinimumLength(2).WithMessage("State name is too short.")
               .MaximumLength(20).WithMessage("State name is too long. Maximum of 20 characters is allowed.");

            RuleFor(x => x.Country)
              .MinimumLength(2).WithMessage("Country name is too short.")
              .MaximumLength(20).WithMessage("Country name is too long. Maximum of 20 characters is allowed.");
        }
    }
}
