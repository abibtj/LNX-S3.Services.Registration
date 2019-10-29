
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

            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Name is required.")
              .MinimumLength(2).WithMessage("Name is too short.")
              .MaximumLength(100).WithMessage("Name is too long. Maximum of 100 characters is allowed.");

            RuleFor(x => x.Category)
               .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.SubjectsArray)
              .NotEmpty().WithMessage("Supply the subjects offered in this class.");
        }
    }
}
