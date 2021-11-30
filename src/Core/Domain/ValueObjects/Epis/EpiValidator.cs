using FluentValidation;
using System;

namespace Domain.ValueObjects.Epis;

public class EpiValidator : AbstractValidator<Epi>
{
    public EpiValidator()
    {
        RuleFor(epi => epi.Id)
            .NotEmpty().NotNull()
                .WithMessage("Inform a valid EPI!");

        When(epi => epi.Id != default, () =>
        {
            RuleFor(epi => epi.Expiration)
                .NotNull().NotEmpty().LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                    .WithMessage(epi => $"Inform a valid Expiration Date for EPI {epi.Id}");
        });
    }
}
