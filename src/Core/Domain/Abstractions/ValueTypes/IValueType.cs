using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.ValueTypes;

public interface IValueType
{
    public bool IsValid { get; }

    public IReadOnlyCollection<ValidationFailure> Errors { get; }
}
