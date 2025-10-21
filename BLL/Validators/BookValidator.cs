using BLL.DTO;
using FluentValidation;

namespace BLL.Validators
{
    public class BookValidator : AbstractValidator<BookDTO>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must be less than 200 characters.");

            RuleFor(b => b.PublishedYear)
                .InclusiveBetween(1450, DateTime.Now.Year)
                .WithMessage($"Published year must be between 1450 and {DateTime.Now.Year}.");

            RuleFor(b => b.AuthorId)
                .NotEmpty().WithMessage("AuthorId is required.");
        }
    }
}
