using FluentValidation;

namespace Domain.Entities.Ppe;

public class PpeValidator : AbstractValidator<Ppe>
{
    public PpeValidator()
    {
        RuleFor(ppe => ppe.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Inform a valid name.");

        RuleFor(ppe => ppe.Name.Length)
            .GreaterThan(3)
            .WithMessage("Inform a valid PPE name.");
    }
}