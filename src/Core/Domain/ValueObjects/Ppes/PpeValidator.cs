using FluentValidation;
using System;

namespace Domain.ValueObjects.Ppes;

public class PpeValidator : AbstractValidator<Ppe>
{
    public PpeValidator()
    {
        RuleFor(epi => epi.InventoryId)
            .NotEmpty().NotNull()
                .WithMessage("Inform a valid EPI!");

        When(epi => epi.InventoryId != default, () =>
        {
            RuleFor(epi => epi.Expiration)
                .NotNull().NotEmpty().LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                    .WithMessage(epi => $"Inform a valid Expiration Date for EPI {epi.InventoryId}");
        });
    }
}
