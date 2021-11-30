using FluentValidation;
using FluentValidation.Results;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.Entities;

public abstract class Entity<TId> : IEntity<TId>
    where TId : struct
{
    [BsonIgnore]
    private ValidationResult _validationResult = new();

    [BsonIgnore]
    public bool IsValid
        => Validate();

    [BsonIgnore]
    public IEnumerable<ValidationFailure> Errors
        => _validationResult.Errors;

    protected void AddErrors(IReadOnlyCollection<ValidationFailure> failures)
        => _validationResult.Errors.AddRange(failures);

    public TId Id { get; protected set; }

    public bool IsDeleted { get; protected set; }

    protected bool OnValidate<TValidator, TEntity>()
        where TValidator : AbstractValidator<TEntity>, new()
        where TEntity : Entity<TId>
    {
        var validationResult = new TValidator().Validate(this as TEntity);
        _validationResult.Errors.AddRange(validationResult.Errors);

        return _validationResult.IsValid;
    }

    protected abstract bool Validate();
}