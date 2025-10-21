using BLL.DTO;
using FluentValidation;

namespace BLL.Validators
{
    public class AuthorValidator : AbstractValidator<AuthorDTO>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(100).WithMessage("Author name must be less than 100 characters.");

            RuleFor(a => a.DateOfBirth)
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.")
                .GreaterThan(new DateTime(1900, 1, 1)).WithMessage("Date of birth must be after 1900.");
        }
    }
}
