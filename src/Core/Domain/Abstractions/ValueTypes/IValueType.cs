using FluentValidation.Results;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.ValueTypes;

public interface IValueType<TValue>
{
    [BsonIgnore]
    public bool IsValid { get; }

    [BsonIgnore]
    public IReadOnlyCollection<ValidationFailure> Errors { get; }

    public TValue Value { get; }

    public void Create(object value);
}
