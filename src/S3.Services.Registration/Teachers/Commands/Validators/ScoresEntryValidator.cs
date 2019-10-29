
////using FluentValidation;
////using S3.Services.Registration.Utility;

////namespace S3.Services.Registration.Teachers.Commands.Validators
////{
////    public class ScoresEntryValidator : AbstractValidator<ScoresEntry>
////    {
////        public ScoresEntryValidator()
////        {
////            RuleFor(x => x.ClassId)
////                  .NotEmpty().WithMessage("Class's Id is required.");

////            RuleFor(x => x.TeacherId)
////                  .NotEmpty().WithMessage("Teacher's Id is required.");
           
////            RuleFor(x => x.Subjects)
////                  .NotEmpty().WithMessage("Minimum of one subject is required.");
////        }
////    }
////}
