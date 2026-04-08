using FluentValidation;

namespace Commands.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
        RuleFor(x => x.TotalCost).GreaterThan(0).WithMessage("Cost must be greater than zero.");
    }
}