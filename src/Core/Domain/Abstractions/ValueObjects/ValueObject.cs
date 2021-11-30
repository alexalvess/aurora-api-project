using FluentValidation;
using FluentValidation.Results;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.ValueObjects;

public abstract record ValueObject
{
    [BsonIgnore]
    private ValidationResult ValidationResult { get; set; } = new();

    [BsonIgnore]
    public bool IsValid
        => Validate();

    [BsonIgnore]
    public IEnumerable<ValidationFailure> Errors
        => ValidationResult.Errors;

    protected bool OnValidate<TValidator, TValueObject>()
        where TValidator : AbstractValidator<TValueObject>, new()
        where TValueObject : ValueObject
    {
        ValidationResult = new TValidator().Validate(this as TValueObject);
        return ValidationResult.IsValid;
    }

    protected abstract bool Validate();
}
