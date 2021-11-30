using Domain.Abstractions.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.Validators;

public abstract class EntityValidator<TEntity, TId> : AbstractValidator<TEntity>
    where TEntity : IEntity<TId>
    where TId : struct
{
    protected EntityValidator()
        => RuleFor(entity => entity.Id)
            .NotEqual(default(TId));
}
