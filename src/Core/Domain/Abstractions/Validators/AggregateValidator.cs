using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.Validators;

public abstract class AggregateValidator<TAggregateRoot, TId> : AbstractValidator<TAggregateRoot>
    where TAggregateRoot : IAggregateRoot<TId>
    where TId : struct
{
    protected AggregateValidator()
        => RuleFor(entity => entity.Id)
            .NotEqual(default(TId));
}
