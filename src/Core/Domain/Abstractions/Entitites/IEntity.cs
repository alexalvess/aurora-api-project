using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.Entities;

public interface IEntity
{
    bool IsDeleted { get; }

    bool IsValid { get; }

    public IEnumerable<ValidationFailure> Errors { get; }
}