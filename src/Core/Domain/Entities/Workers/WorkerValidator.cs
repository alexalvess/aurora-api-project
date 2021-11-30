using FluentValidation;
using System;

namespace Domain.Entities.Workers;

public class WorkerValidator : AbstractValidator<Worker>
{
    public WorkerValidator()
    {
        RuleFor(worker => worker.BirthDate)
            .GreaterThan(new DateOnly(1900, 12, 1))
                .WithMessage("You are very old!")
            .LessThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-8)))
                .WithMessage("You are very young!");
    }
}