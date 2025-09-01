using Application.Property.Commands;
using FluentValidation;

namespace Application.Property.Validators;

public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("The name is mandatory.")
            .MinimumLength(5).WithMessage("The name must be at least 5 characters long.");

        RuleFor(cmd => cmd.Address)
            .NotEmpty().WithMessage("The address is mandatory.")
            .MinimumLength(8).WithMessage("The address must be at least 8 characters long.");

        RuleFor(p => p.Year)
            .NotNull().WithMessage("The year is mandatory.")
            .GreaterThan(0).WithMessage("The year must be greater than zero.")
            .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("The year must be less than the current year.");

        RuleFor(p => p.Price)
            .NotNull().WithMessage("The price is mandatory.")
            .GreaterThan(0).WithMessage("The price must be greater than zero.");

        RuleFor(p => p.CodeInternational)
            .NotEmpty().WithMessage("The international code is mandatory.")
            .MinimumLength(2).WithMessage("The international code must be at least 2 characters long.");
    }
}